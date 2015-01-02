using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    /// <summary>
    /// The purpose of this rewriter is to ensure that vfp indexes are utilized.  The Entity Framework seems to prefer placing (constant) values on the left side of
    /// a comparison.  This is not ideal for vfp as vfp will only utilize an index if the index expression is on the left side of the comparison.
    /// </summary>
    /// <example>
    /// Before:
    ///         SELECT ;
    ///             Extent1.city;
    ///             FROM Customers Extent1;
    ///             WHERE ('USA' = (ALLTRIM(UPPER(Extent1.country)))) 
    ///             
    /// After:
    ///         SELECT ;
    ///             Extent1.city;
    ///             FROM Customers Extent1;
    ///             WHERE ((ALLTRIM(UPPER(Extent1.country))) = 'USA') 
    /// </example>
    internal class ComparisonRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new ComparisonRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpComparisonExpression expression) {
            // Check the left expression to see if it contains a property expression.
            // Don't need to rewrite the comparison expression of the left side already contains a property expression.
            if (PropertyGatherer.Gather(expression.Left).Any()) {
                return expression;
            }

            // Check the right expression to see if it has a property expression.
            if (!PropertyGatherer.Gather(expression.Right).Any()) {
                return expression;
            }

            return reverseExpression(expression);
        }

        private static VfpComparisonExpression reverseExpression(VfpComparisonExpression expression) {
            switch (expression.ExpressionKind) {
                case VfpExpressionKind.GreaterThan:
                    return expression.Right.LessThan(expression.Left);

                case VfpExpressionKind.GreaterThanOrEquals:
                    return expression.Right.LessThanOrEquals(expression.Left);

                case VfpExpressionKind.LessThan:
                    return expression.Right.GreaterThan(expression.Left);

                case VfpExpressionKind.LessThanOrEquals:
                    return expression.Right.GreaterThanOrEquals(expression.Left);

                default:
                    return expression;
            }
        }
    }
}