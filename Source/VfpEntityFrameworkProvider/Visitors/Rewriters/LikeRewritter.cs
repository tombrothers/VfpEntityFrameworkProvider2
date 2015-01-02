using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class LikeRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new LikeRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpLikeExpression expression) {
            // Check the argument expression to see if it contains a property expression.
            // Don't need to rewrite the expression if the left side already contains a property expression.
            if (PropertyGatherer.Gather(expression.Argument).Any()) {
                return expression;
            }

            // Check the pattern expression to see if it has a property expression.
            if (!PropertyGatherer.Gather(expression.Pattern).Any()) {
                return expression;
            }

            return VfpExpressionBuilder.Like(expression.ResultType,
                                             expression.Pattern,
                                             expression.Argument,
                                             expression.Escape);
        }
    }
}