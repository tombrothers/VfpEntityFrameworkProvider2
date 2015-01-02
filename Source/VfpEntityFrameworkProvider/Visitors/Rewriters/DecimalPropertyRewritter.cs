using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class DecimalPropertyRewritter : VfpExpressionVisitor {
        private readonly VfpProviderManifest _vfpManifest;

        public DecimalPropertyRewritter(VfpProviderManifest vfpManifest) {
            _vfpManifest = vfpManifest;
        }

        public static VfpExpression Rewrite(VfpProviderManifest vfpManifest, VfpExpression expression) {
            var rewriter = new DecimalPropertyRewritter(vfpManifest);

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpNewInstanceExpression expression) {
            var arguments = expression.Arguments.Select(GetVfpExpression).ToList();

            return new VfpNewInstanceExpression(expression.ResultType,
                                               VisitVfpExpressionList(arguments),
                                               CreateVfpRelatedEntityRefList(expression.Relationships));
        }

        private VfpExpression GetVfpExpression(VfpExpression expression) {
            if (!IsDecimalPropertyExpression(expression)) {
                return expression;
            }

            var scale = GetScale(expression.ResultType);

            if (scale == 0) {
                return expression;
            }

            var castTypeUsage = _vfpManifest.GetDecimalTypeUsage(20, scale);

            return expression.Cast(castTypeUsage);
        }

        private static bool IsDecimalPropertyExpression(VfpExpression expression) {
            if (expression == null || expression.ExpressionKind != VfpExpressionKind.Property) {
                return false;
            }

            return expression.ResultType.IsPrimitiveType() && expression.ResultType.ToPrimitiveTypeKind() == PrimitiveTypeKind.Decimal;
        }

        private static byte GetScale(TypeUsage typeUsage) {
            byte scale;

            if (!typeUsage.TryGetScale(out scale)) {
                scale = 0;
            }

            return scale;
        }
    }
}