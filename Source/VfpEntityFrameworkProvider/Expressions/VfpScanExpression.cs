using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpScanExpression : VfpExpression {
        public EntitySetBase Target { get; private set; }

        internal VfpScanExpression(TypeUsage collectionOfEntityType, EntitySetBase entitySet)
            : base(VfpExpressionKind.Scan, collectionOfEntityType) {
            Target = entitySet;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}