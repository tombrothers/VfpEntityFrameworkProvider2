using System.Collections.Generic;
using System.Collections.ObjectModel;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class XmlToCursorExpressionGatherer : VfpExpressionVisitor {
        private bool _canRewrite = true;

        private readonly List<VfpXmlToCursorExpression> _expressions = new List<VfpXmlToCursorExpression>();

        public static ReadOnlyCollection<VfpXmlToCursorExpression> Gather(VfpExpression expression) {
            var visitor = new XmlToCursorExpressionGatherer();

            visitor.Visit(expression);

            if (!visitor._canRewrite) {
                visitor._expressions.Clear();
            }

            return visitor._expressions.AsReadOnly();
        }

        public override VfpExpression Visit(VfpNotExpression expression) {
            if (expression.Argument is VfpXmlToCursorExpression) {
                _canRewrite = false;
            }

            return base.Visit(expression);
        }

        public override VfpExpression Visit(VfpOrExpression expression) {
            _canRewrite = false;

            return base.Visit(expression);
        }

        public override VfpExpression Visit(VfpXmlToCursorExpression expression) {
            _expressions.Add(expression);

            return base.Visit(expression);
        }
    }
}