using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class NullGatherer : VfpExpressionVisitor {
        private readonly List<VfpNullExpression> _expressions = new List<VfpNullExpression>();

        public static ReadOnlyCollection<VfpNullExpression> Gather(VfpExpression expression) {
            var visitor = new NullGatherer();

            visitor.Visit(expression);

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpNullExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}