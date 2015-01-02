using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpEntityRefExpression : VfpUnaryExpression {
        internal VfpEntityRefExpression(TypeUsage refResultType, VfpExpression entity)
            : base(VfpExpressionKind.EntityRef, refResultType, entity) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}