using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpLambdaExpression : VfpExpression {
        public VfpLambda Lambda { get; private set; }
        public IList<VfpExpression> Arguments { get; private set; }

        internal VfpLambdaExpression(TypeUsage resultType, VfpLambda lambda, VfpExpressionList arguments)
            : base(VfpExpressionKind.Lambda, resultType) {
            Lambda = ArgumentUtility.CheckNotNull("lambda", lambda);
            Arguments = ArgumentUtility.CheckNotNull("arguments", arguments);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}