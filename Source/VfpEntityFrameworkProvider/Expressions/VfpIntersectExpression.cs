using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpIntersectExpression : VfpBinaryExpression {
        internal VfpIntersectExpression(TypeUsage resultType, VfpExpression left, VfpExpression right)
            : base(VfpExpressionKind.Intersect, resultType, left, right) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}