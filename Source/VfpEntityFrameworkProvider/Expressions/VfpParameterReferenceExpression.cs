using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpParameterReferenceExpression : VfpExpression {
        public string ParameterName { get; private set; }

        internal VfpParameterReferenceExpression(TypeUsage type, string name)
            : base(VfpExpressionKind.ParameterReference, type) {
            ParameterName = name;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}