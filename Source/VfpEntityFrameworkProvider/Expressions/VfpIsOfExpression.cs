using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpIsOfExpression : VfpUnaryExpression {
        public TypeUsage OfType { get; private set; }

        internal VfpIsOfExpression(VfpExpressionKind isOfKind, TypeUsage booleanResultType, VfpExpression argument, TypeUsage isOfType)
            : base(isOfKind, booleanResultType, argument) {
            OfType = ArgumentUtility.CheckNotNull("isOfType", isOfType);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}