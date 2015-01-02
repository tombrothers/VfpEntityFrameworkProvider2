using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpApplyExpression : VfpExpression {
        public VfpExpressionBinding Apply { get; private set; }
        public VfpExpressionBinding Input { get; private set; }

        internal VfpApplyExpression(VfpExpressionKind applyKind,    
                                    TypeUsage resultRowCollectionTypeUsage, 
                                    VfpExpressionBinding input, 
                                    VfpExpressionBinding apply)
            : base(applyKind, resultRowCollectionTypeUsage) {
            Input = ArgumentUtility.CheckNotNull("input", input);
            Apply = ArgumentUtility.CheckNotNull("apply", apply);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}