using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class InvalidWhereExistsRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new InvalidWhereExistsRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpFilterExpression expression) {
            var isEmptyExpression = expression.Predicate as VfpIsEmptyExpression;

            if (isEmptyExpression == null) {
                return base.Visit(expression);
            }

            var projectExpression = isEmptyExpression.Argument as VfpProjectExpression;

            if (projectExpression == null) {
                return base.Visit(expression);
            }

            var filterExpression = projectExpression.Input.Expression as VfpFilterExpression;
            var variables = VariableReferenceGatherer.Gather(filterExpression);

            if (variables.All(x => expression.Input.VariableName != x.VariableName)) {
                return base.Visit(expression);
            }

            // TODO:  verify inner join
            
            //var joinExpression = VfpExpression.Join(VfpExpressionKind.InnerJoin, expression.ResultType, expression.Input, filterExpression.Input, filterExpression.Predicate);
            //var binding = VfpExpression.Binding(joinExpression, VfpExpression.VariableRef(PrimitiveTypeKind.String.ToTypeUsage(), "X" + (++_count)));

            //return base.Visit(binding);

            return base.Visit(expression);
        }
    }
}