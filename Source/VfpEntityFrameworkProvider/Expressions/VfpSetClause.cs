namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpSetClause {
        public VfpExpression Property { get; private set; }
        public VfpExpression Value { get; private set; }

        internal VfpSetClause(VfpExpression property, VfpExpression value) {
            Property = ArgumentUtility.CheckNotNull("property", property);
            Value = ArgumentUtility.CheckNotNull("value", value);
        }
    }
}