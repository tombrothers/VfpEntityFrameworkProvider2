using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public abstract class VfpAggregate {
        public TypeUsage ResultType { get; private set; }
        public VfpExpressionList Arguments { get; private set; }

        internal VfpAggregate(TypeUsage resultType, VfpExpressionList arguments) {
            ResultType = ArgumentUtility.CheckNotNull("resultType", resultType);
            Arguments = ArgumentUtility.CheckNotNull("arguments", arguments);
        }
    }
}