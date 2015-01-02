using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpLikeExpression : VfpExpression {
        public VfpExpression Argument { get; private set; }
        public VfpExpression Pattern { get; private set; }
        public VfpExpression Escape { get; private set; }

        internal VfpLikeExpression(TypeUsage resultType, VfpExpression argument, VfpExpression pattern, VfpExpression escape)
            : base(VfpExpressionKind.Like, resultType) {
            Argument = ArgumentUtility.CheckNotNull("argument", argument);
            Pattern = ArgumentUtility.CheckNotNull("pattern", pattern);
            Escape = ArgumentUtility.CheckNotNull("escape", escape);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}