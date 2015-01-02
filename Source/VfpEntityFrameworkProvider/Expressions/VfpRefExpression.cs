using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpRefExpression : VfpUnaryExpression {
        public EntitySet EntitySet { get; private set; }

        internal VfpRefExpression(TypeUsage refResultType, EntitySet entitySet, VfpExpression refKeys)
            : base(VfpExpressionKind.Ref, refResultType, refKeys) {
            EntitySet = ArgumentUtility.CheckNotNull("entitySet", entitySet);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}