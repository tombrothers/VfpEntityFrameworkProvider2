using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider {
    internal static class VfpExpressionExtensions {
        public static bool IsPropertyOverVarRef(this VfpExpression expression) {
            var propertyExpression = expression as VfpPropertyExpression;

            if (propertyExpression == null) {
                return false;
            }

            var varRefExpression = propertyExpression.Instance as VfpVariableReferenceExpression;

            return varRefExpression != null;
        }

        public static bool IsJoinExpression(this VfpExpression e) {
            return VfpExpressionKind.CrossJoin == e.ExpressionKind ||
                   VfpExpressionKind.FullOuterJoin == e.ExpressionKind ||
                   VfpExpressionKind.InnerJoin == e.ExpressionKind ||
                   VfpExpressionKind.LeftOuterJoin == e.ExpressionKind;
        }

        public static bool IsApplyExpression(this VfpExpression expression) {
            return expression.ExpressionKind == VfpExpressionKind.CrossApply || expression.ExpressionKind == VfpExpressionKind.OuterApply;
        }

        public static bool IsComplexExpression(this VfpExpression expression) {
            return !(expression.ExpressionKind == VfpExpressionKind.Constant || 
                     expression.ExpressionKind == VfpExpressionKind.ParameterReference || 
                     expression.ExpressionKind == VfpExpressionKind.Property);
        }
    }
}