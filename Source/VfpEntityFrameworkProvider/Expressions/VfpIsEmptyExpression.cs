using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpIsEmptyExpression : VfpUnaryExpression {
        internal VfpIsEmptyExpression(TypeUsage booleanResultType, VfpExpression argument)
            : base(VfpExpressionKind.IsEmpty, booleanResultType, argument) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}