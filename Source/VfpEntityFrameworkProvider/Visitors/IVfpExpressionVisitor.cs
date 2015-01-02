using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors {
    public interface IVfpExpressionVisitor<out TResultType>  {
        TResultType Visit(VfpExpression expression);
        TResultType Visit(VfpLikeCExpression expression);
        TResultType Visit(VfpParameterExpression expression);
        TResultType Visit(VfpXmlToCursorExpression expression);
        TResultType Visit(VfpXmlToCursorPropertyExpression expression);
        TResultType Visit(VfpXmlToCursorScanExpression expression);
        TResultType Visit(VfpPropertyVariableNameExpression expression);

        TResultType Visit(VfpAndExpression expression);
        TResultType Visit(VfpApplyExpression expression);
        TResultType Visit(VfpArithmeticExpression expression);
        TResultType Visit(VfpCaseExpression expression);
        TResultType Visit(VfpCastExpression expression);
        TResultType Visit(VfpComparisonExpression expression);
        TResultType Visit(VfpConstantExpression expression);
        TResultType Visit(VfpCrossJoinExpression expression);
        TResultType Visit(VfpDerefExpression expression);
        TResultType Visit(VfpDistinctExpression expression);
        TResultType Visit(VfpElementExpression expression);
        TResultType Visit(VfpEntityRefExpression expression);
        TResultType Visit(VfpExceptExpression expression);
        TResultType Visit(VfpFilterExpression expression);
        TResultType Visit(VfpFunctionExpression expression);
        TResultType Visit(VfpGroupByExpression expression);
        TResultType Visit(VfpInExpression expression);
        TResultType Visit(VfpIntersectExpression expression);
        TResultType Visit(VfpIsEmptyExpression expression);
        TResultType Visit(VfpIsNullExpression expression);
        TResultType Visit(VfpIsOfExpression expression);
        TResultType Visit(VfpJoinExpression expression);
        TResultType Visit(VfpLambdaExpression expression);
        TResultType Visit(VfpLikeExpression expression);
        TResultType Visit(VfpLimitExpression expression);
        TResultType Visit(VfpNewInstanceExpression expression);
        TResultType Visit(VfpNotExpression expression);
        TResultType Visit(VfpNullExpression expression);
        TResultType Visit(VfpOfTypeExpression expression);
        TResultType Visit(VfpOrExpression expression);
        TResultType Visit(VfpParameterReferenceExpression expression);
        TResultType Visit(VfpProjectExpression expression);
        TResultType Visit(VfpPropertyExpression expression);
        TResultType Visit(VfpQuantifierExpression expression);
        TResultType Visit(VfpRefExpression expression);
        TResultType Visit(VfpRefKeyExpression expression);
        TResultType Visit(VfpRelationshipNavigationExpression expression);
        TResultType Visit(VfpScanExpression expression);
        TResultType Visit(VfpSkipExpression expression);
        TResultType Visit(VfpSortExpression expression);
        TResultType Visit(VfpTreatExpression expression);
        TResultType Visit(VfpUnionAllExpression expression);
        TResultType Visit(VfpVariableReferenceExpression expression);
    }
}