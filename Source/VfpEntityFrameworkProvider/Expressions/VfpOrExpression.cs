using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpOrExpression : VfpBinaryExpression {
        internal VfpOrExpression(TypeUsage booleanResultType, VfpExpression left, VfpExpression right)
            : base(VfpExpressionKind.Or, booleanResultType, left, right) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}