using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpGroupByExpression : VfpExpression {
        public VfpGroupExpressionBinding Input { get; private set; }
        public VfpExpressionList Keys { get; private set; }
        public IList<VfpAggregate> Aggregates { get; private set; }

        internal VfpGroupByExpression(TypeUsage collectionOfRowResultType, VfpGroupExpressionBinding input, VfpExpressionList groupKeys, ReadOnlyCollection<VfpAggregate> aggregates)
            : base(VfpExpressionKind.GroupBy, collectionOfRowResultType) {
            Input = ArgumentUtility.CheckNotNull("input", input);
            Keys = ArgumentUtility.CheckNotNull("groupKeys", groupKeys);
            Aggregates = ArgumentUtility.CheckNotNull("aggregates", aggregates);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}