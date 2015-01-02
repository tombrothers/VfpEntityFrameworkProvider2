using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpLimitExpression : VfpExpression {
        public VfpExpression Argument { get; private set; }
        public VfpExpression Limit { get; private set; }
        public bool WithTies { get; private set; }

        internal VfpLimitExpression(TypeUsage resultType, VfpExpression argument, VfpExpression limit, bool withTies)
            : base(VfpExpressionKind.Limit, resultType) {
            Argument = ArgumentUtility.CheckNotNull("argument", argument);
            Limit = ArgumentUtility.CheckNotNull("limit", limit);
            WithTies = withTies;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}