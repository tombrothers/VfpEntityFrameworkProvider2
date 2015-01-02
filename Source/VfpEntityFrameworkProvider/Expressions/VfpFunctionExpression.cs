using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpFunctionExpression : VfpExpression {
        public EdmFunction Function { get; private set; }
        public VfpExpressionList Arguments { get; private set; }

        internal VfpFunctionExpression(TypeUsage resultType, EdmFunction function, VfpExpressionList arguments)
            : base(VfpExpressionKind.Function, resultType) {
            Function = ArgumentUtility.CheckNotNull("function", function);
            Arguments = ArgumentUtility.CheckNotNull("arguments", arguments);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}