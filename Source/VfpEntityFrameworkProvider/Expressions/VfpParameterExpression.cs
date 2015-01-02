using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpParameterExpression : VfpExpression {
        public VfpConstantExpression Value { get; private set; }
        public string Name { get; private set; }

        internal VfpParameterExpression(TypeUsage type, string name, VfpConstantExpression value)
            : base(VfpExpressionKind.Parameter, type) {
            Name = ArgumentUtility.CheckNotNullOrEmpty("name", name);
            Value = ArgumentUtility.CheckNotNull("value", value);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}