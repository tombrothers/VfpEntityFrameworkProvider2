using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.SqlGeneration;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class VariableReferenceRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new VariableReferenceRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpVariableReferenceExpression expression) {
            foreach (var shortNames in SqlVisitor.AliasNames) {
                if (expression.VariableName.StartsWith(shortNames)) {
                    return new VfpVariableReferenceExpression(expression.ResultType, expression.VariableName.Replace(shortNames, shortNames.Substring(0, 1)));
                }
            }

            return base.Visit(expression);
        }
    }
}