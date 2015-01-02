using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpSkipExpression : VfpExpression {
        public VfpExpression Count { get; private set; }
        public VfpExpressionBinding Input { get; private set; }
        public ReadOnlyCollection<VfpSortClause> SortOrder { get; private set; }

        internal VfpSkipExpression(TypeUsage resultType, VfpExpressionBinding input, ReadOnlyCollection<VfpSortClause> sortOrder, VfpExpression count)
            : base(VfpExpressionKind.Skip, resultType) {
            Input = ArgumentUtility.CheckNotNull("input", input);
            SortOrder = ArgumentUtility.CheckNotNull("sortOrder", sortOrder);
            Count = ArgumentUtility.CheckNotNull("count", count);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}