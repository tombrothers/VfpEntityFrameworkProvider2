using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public class VfpVariableReferenceExpression : VfpExpression {
        public string VariableName { get; private set; }

        internal VfpVariableReferenceExpression(TypeUsage type, string name)
            : base(VfpExpressionKind.VariableReference, type) {
            VariableName = name;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }

        [ExcludeFromCodeCoverage]
        private string DebuggerDisplay() {
            return string.Format("VariableName={0} | EdmType={1}", VariableName, ResultType.EdmType);
        }
    }
}