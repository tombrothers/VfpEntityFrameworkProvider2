using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpSortExpression : VfpExpression {
        public VfpExpressionBinding Input { get; private set; }
        public ReadOnlyCollection<VfpSortClause> SortOrder { get; private set; }

        internal VfpSortExpression(TypeUsage resultType, VfpExpressionBinding input, ReadOnlyCollection<VfpSortClause> sortOrder)
            : base(VfpExpressionKind.Sort, resultType) {
            Input = ArgumentUtility.CheckNotNull("input", input);
            SortOrder = ArgumentUtility.CheckNotNull("sortOrder", sortOrder);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}