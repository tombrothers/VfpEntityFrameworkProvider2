using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Removers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class ExpressionRewritter {
        public static VfpExpression Rewrite(VfpProviderManifest vfpManifest, VfpExpression expression) {
            expression = RedundantCaseExpressionRemover.Remove(expression);

            expression = FlattenFilterRewritter.Rewrite(expression);
            expression = ApplyRewritter.Rewrite(expression);
            
            expression = InRewritter.Rewrite(expression);
            expression = XmlToCursorRewritter.Rewrite(expression);
            //expression = XmlToCursorMoveToInnerExpressionRewriter.Rewrite(expression);
            expression = XmlToCursorJoinRewriter.Rewrite(expression);

            expression = ComparisonRewritter.Rewrite(expression);
            expression = LikeRewritter.Rewrite(expression);
            expression = LikeCRewritter.Rewrite(expression);
            expression = CaseWithNullRewriter.Rewrite(expression);
            
            expression = SingleRowTableRewritter.Rewrite(expression);
            expression = MissingOrderByRewritter.Rewrite(expression);
            expression = VariableReferenceRewritter.Rewrite(expression);
            expression = ConstantToParameterRewritter.Rewrite(expression);
            expression = FilterProjectRewritter.Rewrite(expression);

            expression = DecimalPropertyRewritter.Rewrite(vfpManifest, expression);

            return expression;
        }
    }
}