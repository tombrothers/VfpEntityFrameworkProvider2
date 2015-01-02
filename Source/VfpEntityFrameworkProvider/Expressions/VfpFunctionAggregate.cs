using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpFunctionAggregate : VfpAggregate {
        public EdmFunction Function { get; private set; }
        public bool Distinct { get; private set; }

        internal VfpFunctionAggregate(TypeUsage resultType, VfpExpressionList arguments, EdmFunction function, bool isDistinct)
            : base(resultType, arguments) {
            Function = ArgumentUtility.CheckNotNull("function", function);
            Distinct = isDistinct;
        }
    }
}