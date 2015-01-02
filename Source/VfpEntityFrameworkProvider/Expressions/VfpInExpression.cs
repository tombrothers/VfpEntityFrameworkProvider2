using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpInExpression : VfpExpression {
        public VfpExpression Item { get; private set; }
        public IList<VfpExpression> List { get; private set; }

        internal VfpInExpression(TypeUsage booleanResultType, VfpExpression item, VfpExpressionList list)
            : base(VfpExpressionKind.In, booleanResultType) {
            Item = ArgumentUtility.CheckNotNull("item", item);
            List = ArgumentUtility.CheckNotNull("list", list);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}