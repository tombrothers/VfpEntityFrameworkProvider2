using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class VariableReferenceGatherer : VfpExpressionVisitor {
        private readonly List<VfpVariableReferenceExpression> _expressions = new List<VfpVariableReferenceExpression>();

        public static ReadOnlyCollection<VfpVariableReferenceExpression> Gather(VfpExpression expression) {
            var visitor = new VariableReferenceGatherer();

            visitor.Visit(expression);

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpVariableReferenceExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}