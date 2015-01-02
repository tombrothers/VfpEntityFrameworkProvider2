using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpComparisonExpression : VfpBinaryExpression {
        internal VfpComparisonExpression(VfpExpressionKind kind, TypeUsage resultType, VfpExpression left, VfpExpression right)
            : base(kind, resultType, left, right) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}