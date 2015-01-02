using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Replacers {
    internal class VariableReferenceReplacer : VfpExpressionVisitor {
        private readonly VfpVariableReferenceExpression _variableReferenceExpression;

        public static VfpExpression Replace(VfpVariableReferenceExpression variableReferenceExpression, VfpExpression expression) {
            var rewriter = new VariableReferenceReplacer(variableReferenceExpression);

            return rewriter.Visit(expression);
        }

        public VariableReferenceReplacer(VfpVariableReferenceExpression variableReferenceExpression) {
            _variableReferenceExpression = variableReferenceExpression;
        }


        public override VfpExpression Visit(VfpVariableReferenceExpression expression) {
            return _variableReferenceExpression;
        }
    }
}