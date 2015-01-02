using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpDerefExpression : VfpUnaryExpression {
        internal VfpDerefExpression(TypeUsage entityResultType, VfpExpression refExpr)
            : base(VfpExpressionKind.Deref, entityResultType, refExpr) {
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}