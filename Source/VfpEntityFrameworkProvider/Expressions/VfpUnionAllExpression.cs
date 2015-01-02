using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpUnionAllExpression : VfpBinaryExpression {
        internal VfpUnionAllExpression(TypeUsage resultType, VfpExpression left, VfpExpression right)
            : base(VfpExpressionKind.UnionAll, resultType, left, right) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}