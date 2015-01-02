using System.Data.Entity.Core.Metadata.Edm;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class ConstantToParameterRewritter : VfpExpressionVisitor {
        private int _count;

        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new ConstantToParameterRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpParameterExpression expression) {
            return expression;
        }

        public override VfpExpression Visit(VfpConstantExpression expression) {
            switch (expression.ResultType.ToPrimitiveTypeKind()) {
                case PrimitiveTypeKind.Binary:
                case PrimitiveTypeKind.Decimal:
                case PrimitiveTypeKind.Double:
                case PrimitiveTypeKind.Single:
                case PrimitiveTypeKind.String:
                    return new VfpParameterExpression(expression.ResultType, GetParameterName(), expression);
                default:
                    return base.Visit(expression);
            }
        }

        private string GetParameterName() {
            return "@__C2P__" + (++_count);
        }
    }
}