using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class ApplyRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new ApplyRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpApplyExpression expression) {
            if (expression.ExpressionKind != VfpExpressionKind.OuterApply) {
                return expression;
            }

            if (IsSingleRowTable(expression.Input)) {
                return CreateJoin(expression.Apply, expression.Input);
            }

            return expression;
        }

        private static VfpJoinExpression CreateJoin(VfpExpressionBinding left, VfpExpressionBinding right) {
            var comparison = VfpExpressionBuilder.Constant(1).ExpressionEquals(VfpExpressionBuilder.Constant(1));

            return left.LeftJoin(right, comparison, right.VariableType);
        }

        private static bool IsSingleRowTable(VfpExpressionBinding expression) {
            return expression.VariableName.StartsWith("SingleRowTable");
        }
    }
}