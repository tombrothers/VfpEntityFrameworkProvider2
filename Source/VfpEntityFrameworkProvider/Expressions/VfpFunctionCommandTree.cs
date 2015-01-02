using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpFunctionCommandTree : VfpCommandTree {
        public EdmFunction EdmFunction { get; private set; }

        public override VfpCommandTreeKind CommandTreeKind {
            get {
                return VfpCommandTreeKind.Function;
            }
        }

        public VfpFunctionCommandTree(EdmFunction edmFunction, TypeUsage resultType, IEnumerable<KeyValuePair<string, TypeUsage>> parameters)
            : base(resultType, parameters) {
            EdmFunction = ArgumentUtility.CheckNotNull("edmFunction", edmFunction);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}