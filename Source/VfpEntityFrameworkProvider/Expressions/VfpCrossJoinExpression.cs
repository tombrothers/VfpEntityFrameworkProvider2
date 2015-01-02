using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpCrossJoinExpression : VfpExpression {
        public IList<VfpExpressionBinding> Inputs { get; private set; }

        internal VfpCrossJoinExpression(TypeUsage collectionOfRowResultType, ReadOnlyCollection<VfpExpressionBinding> inputs)
            : base(VfpExpressionKind.CrossJoin, collectionOfRowResultType) {
            Inputs = ArgumentUtility.CheckNotNull("inputs", inputs);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}