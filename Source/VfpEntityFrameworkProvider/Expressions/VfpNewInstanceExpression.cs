using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpNewInstanceExpression : VfpExpression {
        public VfpExpressionList Arguments { get; private set; }
        public ReadOnlyCollection<VfpRelatedEntityRef> Relationships { get; private set; }

        internal VfpNewInstanceExpression(TypeUsage type, VfpExpressionList arguments, ReadOnlyCollection<VfpRelatedEntityRef> relationships = null)
            : base(VfpExpressionKind.NewInstance, type) {
            Arguments = ArgumentUtility.CheckNotNull("arguments", arguments);

            if (relationships != null && relationships.Count > 0) {
                Relationships = relationships;
            }
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}