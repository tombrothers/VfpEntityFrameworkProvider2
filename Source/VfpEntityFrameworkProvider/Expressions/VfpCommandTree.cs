using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public abstract class VfpCommandTree : VfpExpression {
        public IEnumerable<KeyValuePair<string, TypeUsage>> Parameters { get; private set; }
        public abstract VfpCommandTreeKind CommandTreeKind { get; }

        internal VfpCommandTree(TypeUsage resultType, IEnumerable<KeyValuePair<string, TypeUsage>> parameters)
            : base(VfpExpressionKind.CommandTree, resultType) {
            Parameters = ArgumentUtility.CheckNotNull("parameters", parameters);
        }
    }
}