using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;
using VfpEntityFrameworkProvider.Visitors.Replacers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class FlattenFilterRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new FlattenFilterRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpFilterExpression expression) {
            expression = (VfpFilterExpression)base.Visit(expression);

            var innerFilter = expression.Input.Expression as VfpFilterExpression;

            if (innerFilter == null) {
                return expression;
            }

            var variables = VariableReferenceGatherer.Gather(innerFilter.Predicate);

            if (!variables.Any() || variables.Select(x => x.VariableName).Distinct().Count() > 1) {
                return expression;
            }

            var predicate = VariableReferenceReplacer.Replace(variables.First(), expression.Predicate);

            return innerFilter.Input.Filter(predicate.And(innerFilter.Predicate));
        }
    }
}