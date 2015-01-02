using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;
using VfpEntityFrameworkProvider.Visitors.Replacers;

namespace VfpEntityFrameworkProvider.Visitors.Removers {
    internal class XmlToCursorExpressionRemover : VfpExpressionVisitor {
        private readonly string _cursorName;

        public static VfpExpression Remove(VfpExpression expression, string cursorName) {
            ArgumentUtility.CheckNotNullOrEmpty("cursorName", cursorName);

            var visitor = new XmlToCursorExpressionRemover(cursorName);

            return visitor.Visit(expression);
        }

        private XmlToCursorExpressionRemover(string cursorName) {
            _cursorName = cursorName;
        }

        public override VfpExpression Visit(VfpJoinExpression expression) {
            var result = base.Visit(expression);

            expression = result as VfpJoinExpression;

            if (expression == null) {
                return result;
            }

            var filterExpression = expression.Left.Expression as VfpFilterExpression;

            if (IsZeroEqualsZero(filterExpression)) {
                expression = VfpExpressionBuilder.Join(expression.ExpressionKind, expression.ResultType, filterExpression.Input, expression.Right, expression.JoinCondition);
            }

            filterExpression = expression.Right.Expression as VfpFilterExpression;

            if (IsZeroEqualsZero(filterExpression)) {
                expression = VfpExpressionBuilder.Join(expression.ExpressionKind, expression.ResultType, expression.Left, filterExpression.Input, expression.JoinCondition);
            }

            return expression;
        }

        public override VfpExpression Visit(VfpProjectExpression expression) {
            var result = base.Visit(expression);

            expression = result as VfpProjectExpression;

            if (expression == null) {
                return result;
            }

            var filterExpression = expression.Input.Expression as VfpFilterExpression;

            if (IsZeroEqualsZero(filterExpression)) {
                var projectionExpression = GetProjection(filterExpression, expression.Input.VariableName, expression.Projection);

                return filterExpression.Input.Project(projectionExpression);
            }

            return result;
        }

        private static VfpExpression GetProjection(VfpFilterExpression filterExpression, string filterVariableName, VfpExpression projectionExpression) {
            var newInstanceExpression = projectionExpression as VfpNewInstanceExpression;

            if (newInstanceExpression == null) {
                return projectionExpression;
            }

            var newInstanceVariableNames = VariableReferenceGatherer.Gather(newInstanceExpression)
                                                                    .Select(x => x.VariableName)
                                                                    .Distinct()
                                                                    .ToArray();

            if (newInstanceVariableNames.Length != 1) {
                return projectionExpression;
            }

            if (filterVariableName != newInstanceVariableNames.First()) {
                return projectionExpression;
            }

            return VariableReferenceReplacer.Replace(filterExpression.Input.Variable, newInstanceExpression);
        }

        private static bool IsZeroEqualsZero(VfpFilterExpression expression) {
            return expression != null && IsZeroEqualsZero(expression.Predicate as VfpComparisonExpression);
        }

        private static bool IsZeroEqualsZero(VfpComparisonExpression expression) {
            return expression != null && expression.ExpressionKind == VfpExpressionKind.Equals && IsZero(expression.Left as VfpConstantExpression) && IsZero(expression.Right as VfpConstantExpression);
        }

        private static bool IsZero(VfpConstantExpression expression) {
            return expression != null && expression.Value != null && expression.Value.ToString() == "0";
        }

        public override VfpExpression Visit(VfpFilterExpression expression) {
            var result = base.Visit(expression);

            expression = result as VfpFilterExpression;

            if (expression == null) {
                return result;
            }

            if (IsValidXmlToCursorExpression(expression.Predicate as VfpXmlToCursorExpression)) {
                var predicate = VfpExpressionBuilder.Constant(0).ExpressionEquals(VfpExpressionBuilder.Constant(0));

                return new VfpFilterExpression(expression.ResultType, expression.Input, predicate);
            }

            return result;
        }

        public override VfpExpression Visit(VfpAndExpression expression) {
            var result = base.Visit(expression);

            expression = result as VfpAndExpression;

            if (expression == null) {
                return result;
            }

            if (IsValidXmlToCursorExpression(expression.Left as VfpXmlToCursorExpression)) {
                return expression.Right;
            }

            if (IsValidXmlToCursorExpression(expression.Right as VfpXmlToCursorExpression)) {
                return expression.Left;
            }

            return result;
        }

        private bool IsValidXmlToCursorExpression(VfpXmlToCursorExpression expression) {
            return expression != null && expression.CursorName == _cursorName;
        }
    }
}