using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpInsertCommandTree : VfpModificationCommandTree {
        public IList<VfpSetClause> SetClauses { get; private set; }
        public VfpExpression Returning { get; private set; }

        public override VfpCommandTreeKind CommandTreeKind {
            get {
                return VfpCommandTreeKind.Insert;
            }
        }

        public VfpInsertCommandTree(VfpExpressionBinding target, 
                                    ReadOnlyCollection<VfpSetClause> setClauses, 
                                    IEnumerable<KeyValuePair<string, TypeUsage>> parameters, 
                                    VfpExpression returning = null)
            : base(target.Expression.ResultType, target, parameters) {
            SetClauses = ArgumentUtility.CheckNotNull("setClauses", setClauses);
            Returning = returning;
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}