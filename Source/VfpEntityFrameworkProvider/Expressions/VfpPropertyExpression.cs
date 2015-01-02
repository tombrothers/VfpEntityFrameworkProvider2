using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public class VfpPropertyExpression : VfpExpression {
        public EdmMember Property { get; private set; }
        public VfpExpression Instance { get; private set; }

        internal VfpPropertyExpression(TypeUsage resultType, EdmMember property, VfpExpression instance)
            : base(VfpExpressionKind.Property, resultType) {
            Property = ArgumentUtility.CheckNotNull("property", property);
            Instance = ArgumentUtility.CheckNotNull("instance", instance);
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