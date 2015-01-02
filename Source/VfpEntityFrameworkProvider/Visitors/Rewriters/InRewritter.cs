using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class InRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new InRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpOrExpression expression) {
            var list = OrComparisonGatherer.Gather(expression);

            if (!list.Any()) {
                return base.Visit(expression);
            }

            var comparison = list.Select(x => new {
                                     x.ExpressionKind,
                                     LeftExpression = x.Left,
                                     LeftDbConstantExpression = ConstantGatherer.Gather(x.Left).FirstOrDefault(),
                                     RightExpression = x.Right,
                                     RightDbPropertyExpression = PropertyGatherer.Gather(x.Right).FirstOrDefault(),
                                     RightDbVariableReferenceExpression = VariableReferenceGatherer.Gather(x.Right).FirstOrDefault()
                                 }).First();

            if (comparison.ExpressionKind != VfpExpressionKind.Equals && comparison.ExpressionKind != VfpExpressionKind.NotEquals) {
                return base.Visit(expression);
            }

            if (comparison.LeftDbConstantExpression == null || comparison.RightDbPropertyExpression == null || comparison.RightDbVariableReferenceExpression == null) {
                return base.Visit(expression);
            }

            var expressions = list.Select(x => new {
                                      x.ExpressionKind,
                                      LeftExpression = x.Left,
                                      LeftDbConstantExpression = ConstantGatherer.Gather(x.Left).FirstOrDefault(),
                                      RightExpression = x.Right,
                                      RightDbPropertyExpression = PropertyGatherer.Gather(x.Right).FirstOrDefault(),
                                      RightDbVariableReferenceExpression = VariableReferenceGatherer.Gather(x.Right).FirstOrDefault()
                                  })
                                  .Where(x => x.LeftDbConstantExpression != null)
                                  .Where(x => x.RightDbPropertyExpression != null)
                                  .Where(x => x.RightDbVariableReferenceExpression != null)
                                  .ToList();

            if (list.Count != expressions.Count) {
                return base.Visit(expression);
            }

            if (!expressions.All(x => x.LeftDbConstantExpression.ConstantKind == comparison.LeftDbConstantExpression.ConstantKind &&
                                      x.LeftDbConstantExpression.ResultType == comparison.LeftDbConstantExpression.ResultType &&
                                      x.RightDbPropertyExpression.Property == comparison.RightDbPropertyExpression.Property &&
                                      x.RightDbVariableReferenceExpression.VariableName == comparison.RightDbVariableReferenceExpression.VariableName)) {
                return base.Visit(expression);
            }

            var inExpression = comparison.RightExpression.In(list.Select(x => x.Left).ToList().List());

            return base.Visit(inExpression);
        }
    }
}
