using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpQuantifierExpression : VfpExpression {
        public VfpExpressionBinding Input { get; private set; }
        public VfpExpression Predicate { get; private set; }

        internal VfpQuantifierExpression(VfpExpressionKind kind, TypeUsage booleanResultType, VfpExpressionBinding input, VfpExpression predicate)
            : base(kind, booleanResultType) {
            Input = ArgumentUtility.CheckNotNull("input", input);
            Predicate = ArgumentUtility.CheckNotNull("predicate", predicate);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}