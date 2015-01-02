using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class PropertyGatherer : VfpExpressionVisitor {
        private readonly List<VfpPropertyExpression> _expressions = new List<VfpPropertyExpression>();

        public static ReadOnlyCollection<VfpPropertyExpression> Gather(VfpExpression expression) {
            var visitor = new PropertyGatherer();

            visitor.Visit(expression);

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpPropertyExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}