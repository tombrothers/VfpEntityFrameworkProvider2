using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class ScanGatherer : VfpExpressionVisitor {
        private readonly List<VfpScanExpression> _expressions = new List<VfpScanExpression>();

        public static ReadOnlyCollection<VfpScanExpression> Gather(VfpExpression expression) {
            var visitor = new ScanGatherer();

            visitor.Visit(expression);

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpScanExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}