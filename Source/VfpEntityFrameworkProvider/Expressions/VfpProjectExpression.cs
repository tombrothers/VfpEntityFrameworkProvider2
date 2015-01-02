using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpProjectExpression : VfpExpression {
        public VfpExpressionBinding Input { get; private set; }
        public VfpExpression Projection { get; private set; }

        internal VfpProjectExpression(TypeUsage resultType, VfpExpressionBinding input, VfpExpression projection)
            : base(VfpExpressionKind.Project, resultType) {
            Input = ArgumentUtility.CheckNotNull("input", input); ;
            Projection = ArgumentUtility.CheckNotNull("projection", projection); ;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}