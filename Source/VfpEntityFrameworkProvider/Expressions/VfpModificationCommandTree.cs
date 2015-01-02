using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public abstract class VfpModificationCommandTree : VfpCommandTree {
        public VfpExpressionBinding Target { get; private set; }

        protected VfpModificationCommandTree(TypeUsage resultType, VfpExpressionBinding target, IEnumerable<KeyValuePair<string, TypeUsage>> parameters)
            : base(resultType, parameters) {
            Target = ArgumentUtility.CheckNotNull("target", target);
        }
    }
}