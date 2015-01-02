using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpUpdateCommandTree : VfpModificationCommandTree {
        public IList<VfpSetClause> SetClauses { get; private set; }
        public VfpExpression Predicate { get; private set; }
        public VfpExpression Returning { get; private set; }

        public override VfpCommandTreeKind CommandTreeKind {
            get {
                return VfpCommandTreeKind.Update;
            }
        }

        public VfpUpdateCommandTree(VfpExpressionBinding target,
                                   ReadOnlyCollection<VfpSetClause> setClauses,
                                   VfpExpression predicate,
                                   IEnumerable<KeyValuePair<string, TypeUsage>> parameters,
                                   VfpExpression returning = null)
            : base(target.Expression.ResultType, target, parameters) {
            SetClauses = ArgumentUtility.CheckNotNull("setClauses", setClauses);
            Predicate = ArgumentUtility.CheckNotNull("predicate", predicate);
            Returning = returning;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}