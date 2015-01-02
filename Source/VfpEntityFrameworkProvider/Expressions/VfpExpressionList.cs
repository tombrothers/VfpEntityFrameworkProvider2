using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpExpressionList : ReadOnlyCollection<VfpExpression> {
        internal VfpExpressionList(IList<VfpExpression> elements)
            : base(elements) {
        }
    }
}