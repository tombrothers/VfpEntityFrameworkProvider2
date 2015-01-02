using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpGroupExpressionBinding {
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

        public VfpVariableReferenceExpression GroupVariable { get; private set; }

        public string GroupVariableName {
            get {
                return GroupVariable.VariableName;
            }
        }

        public TypeUsage GroupVariableType {
            get {
                return GroupVariable.ResultType;
            }
        }

        internal VfpGroupExpressionBinding(VfpExpression expression, VfpVariableReferenceExpression variable, VfpVariableReferenceExpression groupVariable) {
            Expression = ArgumentUtility.CheckNotNull("expression", expression);
            Variable = ArgumentUtility.CheckNotNull("variable", variable);
            GroupVariable = ArgumentUtility.CheckNotNull("groupVariable", groupVariable);
        }
    }
}