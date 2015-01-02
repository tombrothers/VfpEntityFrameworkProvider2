using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpDeleteCommandTree : VfpModificationCommandTree {
        public VfpExpression Predicate { get; private set; }

        public override VfpCommandTreeKind CommandTreeKind {
            get {
                return VfpCommandTreeKind.Delete;
            }
        }

        public VfpDeleteCommandTree(VfpExpressionBinding target, VfpExpression predicate, IEnumerable<KeyValuePair<string, TypeUsage>> parameters)
            : base(target.Expression.ResultType, target, parameters) {
                Predicate = ArgumentUtility.CheckNotNull("predicate", predicate);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}