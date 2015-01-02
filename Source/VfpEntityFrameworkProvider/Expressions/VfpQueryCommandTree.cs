using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpQueryCommandTree : VfpCommandTree {
        public VfpExpression Query { get; private set; }

        public override VfpCommandTreeKind CommandTreeKind {
            get {
                return VfpCommandTreeKind.Query;
            }
        }

        public VfpQueryCommandTree(VfpExpression query, IEnumerable<KeyValuePair<string, TypeUsage>> parameters)
            : base(query.ResultType, parameters) {
                Query = ArgumentUtility.CheckNotNull("query", query);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}