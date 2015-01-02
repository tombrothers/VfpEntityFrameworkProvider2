using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class OrComparisonGatherer : VfpExpressionVisitor {
        private readonly List<VfpComparisonExpression> _expressions = new List<VfpComparisonExpression>();
        private bool _invalid;

        public static ReadOnlyCollection<VfpComparisonExpression> Gather(VfpOrExpression expression) {
            var visitor = new OrComparisonGatherer();

            visitor.Visit(expression);

            if (visitor._invalid) {
                visitor._expressions.Clear();
            }

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpAndExpression expression) {
            _invalid = true;

            return base.Visit(expression);
        }

        public override VfpExpression Visit(VfpComparisonExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}