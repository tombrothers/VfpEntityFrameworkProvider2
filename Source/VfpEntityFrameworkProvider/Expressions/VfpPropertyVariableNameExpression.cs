using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public class VfpPropertyVariableNameExpression : VfpExpression {
        public EdmMember Property { get; private set; }
        public string VariableName { get; private set; }

        internal VfpPropertyVariableNameExpression(TypeUsage resultType, EdmMember property, string variableName)
            : base(VfpExpressionKind.PropertyVariableName, resultType) {
            Property = ArgumentUtility.CheckNotNull("property", property);
            VariableName = ArgumentUtility.CheckNotNullOrEmpty("variableName", variableName);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }

        [ExcludeFromCodeCoverage]
        private string DebuggerDisplay() {
            return String.Format("PropertyName={0} | DeclaringTypeName={1}", Property.Name, Property.DeclaringType.Name);
        }
    }
}