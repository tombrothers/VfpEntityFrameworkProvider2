using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class ConstantGatherer : VfpExpressionVisitor {
        private readonly List<VfpConstantExpression> _expressions = new List<VfpConstantExpression>();

        public static ReadOnlyCollection<VfpConstantExpression> Gather(VfpExpression expression) {
            var visitor = new ConstantGatherer();

            visitor.Visit(expression);

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpConstantExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}