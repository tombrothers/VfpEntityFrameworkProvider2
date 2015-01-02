using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpLambda {
        public VfpExpression Body { get; private set; }
        public IList<VfpVariableReferenceExpression> Variables { get; private set; }

        internal VfpLambda(ReadOnlyCollection<VfpVariableReferenceExpression> variables, VfpExpression body) {
            Variables = ArgumentUtility.CheckNotNull("variables", variables);
            Body = ArgumentUtility.CheckNotNull("body", body);
        }
    }
}