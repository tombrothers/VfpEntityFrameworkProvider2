using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class SingleRowTableRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new SingleRowTableRewritter();

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpProjectExpression expression) {
            VfpProjectExpression innerProjectionExpression;
            bool hasNotExpression;

            if (GetInnerProjectionExpression(expression, out innerProjectionExpression, out hasNotExpression)) {
                var innerNewInstanceExpression = innerProjectionExpression.Projection as VfpNewInstanceExpression;

                if (innerNewInstanceExpression != null) {
                    var innerConstExpression = innerNewInstanceExpression.Arguments[0] as VfpConstantExpression;

                    if (innerConstExpression != null) {
                        var countExpression = new VfpConstantExpression(innerConstExpression.ResultType, "COUNT(*)");
                        innerNewInstanceExpression = new VfpNewInstanceExpression(innerNewInstanceExpression.ResultType, new VfpExpressionList(new List<VfpExpression> { countExpression }));
                        innerProjectionExpression = new VfpProjectExpression(innerProjectionExpression.ResultType, innerProjectionExpression.Input, innerNewInstanceExpression);

                        VfpExpression comparison = new VfpComparisonExpression(VfpExpressionKind.LessThan,
                                                                    PrimitiveTypeKind.Boolean.ToTypeUsage(),
                                                                    new VfpConstantExpression(
                                                                        PrimitiveTypeKind.Int32.ToTypeUsage(), 0),
                                                                    innerProjectionExpression);

                        if (!hasNotExpression) {
                            comparison = new VfpNotExpression(comparison.ResultType, comparison);
                        }

                        innerNewInstanceExpression = new VfpNewInstanceExpression(innerNewInstanceExpression.ResultType, new VfpExpressionList(new List<VfpExpression> { comparison }));
                        innerProjectionExpression = new VfpProjectExpression(expression.ResultType, expression.Input, innerNewInstanceExpression);

                        return innerProjectionExpression;
                    }
                }
            }

            return base.Visit(expression);
        }

        private static bool GetInnerProjectionExpression(VfpProjectExpression expression, out VfpProjectExpression innerProjectionExpression, out bool hasNotExpression) {
            innerProjectionExpression = null;
            hasNotExpression = false;

            if (!expression.Input.VariableName.StartsWith("SingleRowTable")) {
                return false;
            }

            var newInstanceExpression = expression.Projection as VfpNewInstanceExpression;

            if (newInstanceExpression == null) {
                return false;
            }

            var notExpression = newInstanceExpression.Arguments[0] as VfpNotExpression;
            VfpIsEmptyExpression isEmptyExpression = null;

            if (notExpression == null) {
                isEmptyExpression = newInstanceExpression.Arguments[0] as VfpIsEmptyExpression;
            }
            else {
                isEmptyExpression = notExpression.Argument as VfpIsEmptyExpression;
                hasNotExpression = true;
            }

            if (isEmptyExpression == null) {
                return false;
            }

            innerProjectionExpression = isEmptyExpression.Argument as VfpProjectExpression;

            return innerProjectionExpression != null;
        }
    }
}