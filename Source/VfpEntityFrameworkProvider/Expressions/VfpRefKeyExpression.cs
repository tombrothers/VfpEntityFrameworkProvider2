using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpRefKeyExpression : VfpUnaryExpression {
        internal VfpRefKeyExpression(TypeUsage rowResultType, VfpExpression reference)
            : base(VfpExpressionKind.RefKey, rowResultType, reference) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}