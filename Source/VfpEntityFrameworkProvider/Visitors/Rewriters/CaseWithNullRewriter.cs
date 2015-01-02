using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class CaseWithNullRewriter : VfpExpressionVisitor {
        private bool _canRewrite;

        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new CaseWithNullRewriter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpNewInstanceExpression expression) {
            _canRewrite = true;

            var result = base.Visit(expression);

            _canRewrite = false;

            return result;
        }

        public override VfpExpression Visit(VfpCaseExpression expression) {
            if (!_canRewrite || !NullGatherer.Gather(expression).Any()) {
                return expression;
            }

            return expression.Cast(expression.ResultType);
        }
    }
}