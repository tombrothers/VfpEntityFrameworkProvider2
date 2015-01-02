using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class ParameterGatherer : VfpExpressionVisitor {
        private readonly List<VfpParameterExpression> _expressions = new List<VfpParameterExpression>();

        public static ReadOnlyCollection<VfpParameterExpression> Gather(VfpExpression expression) {
            var visitor = new ParameterGatherer();

            visitor.Visit(expression);

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpParameterExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}