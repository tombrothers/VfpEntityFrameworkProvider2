using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpArithmeticExpression : VfpExpression {
        public VfpExpressionList Arguments { get; private set; }

        internal VfpArithmeticExpression(VfpExpressionKind kind, TypeUsage numericResultType, VfpExpressionList arguments)
            : base(kind, numericResultType) {
            Arguments = arguments;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}