using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpLikeCExpression : VfpExpression {
        public VfpExpression Argument { get; private set; }
        public VfpExpression Pattern { get; private set; }

        internal VfpLikeCExpression(TypeUsage resultType, VfpExpression argument, VfpExpression pattern)
            : base(VfpExpressionKind.LikeC, resultType) {
            Argument = ArgumentUtility.CheckNotNull("argument", argument);
            Pattern = ArgumentUtility.CheckNotNull("pattern", pattern);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}