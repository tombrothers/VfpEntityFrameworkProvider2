using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpBinaryExpression : VfpExpression {
        public VfpExpression Left { get; private set; }
        public VfpExpression Right { get; private set; }

        internal VfpBinaryExpression(VfpExpressionKind kind, TypeUsage type, VfpExpression left, VfpExpression right)
            : base(kind, type) {
            Left = ArgumentUtility.CheckNotNull("left", left);
            Right = ArgumentUtility.CheckNotNull("right", right);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}