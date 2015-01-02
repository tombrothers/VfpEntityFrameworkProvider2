using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpIsNullExpression : VfpUnaryExpression {
        internal VfpIsNullExpression(TypeUsage booleanResultType, VfpExpression argument)
            : base(VfpExpressionKind.IsNull, booleanResultType, argument) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}