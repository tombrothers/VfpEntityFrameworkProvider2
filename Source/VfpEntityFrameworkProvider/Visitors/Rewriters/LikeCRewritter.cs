using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class LikeCRewritter : VfpExpressionVisitor {
        private bool _canRewrite;

        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new LikeCRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpNewInstanceExpression expression) {
            _canRewrite = true;

            var result =   base.Visit(expression);

            _canRewrite = false;

            return result;
        }

        public override VfpExpression Visit(VfpLikeExpression expression) {
            if (!_canRewrite) {
                return expression;
            }

            return expression.LikeC();
        }
    }
}