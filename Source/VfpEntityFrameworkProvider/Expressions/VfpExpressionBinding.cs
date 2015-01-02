using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpExpressionBinding {
        public VfpExpression Expression { get; private set; }
        public VfpVariableReferenceExpression Variable { get; private set; }

        public string VariableName {
            get {
                return Variable.VariableName;
            }
        }

        public TypeUsage VariableType {
            get {
                return Variable.ResultType;
            }
        }

        internal VfpExpressionBinding(VfpExpression expression, VfpVariableReferenceExpression variableReference) {
            Expression = ArgumentUtility.CheckNotNull("expression", expression);
            Variable = ArgumentUtility.CheckNotNull("variableReference", variableReference);
        }
    }
}