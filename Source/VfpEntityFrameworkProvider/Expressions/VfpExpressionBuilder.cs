using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace VfpEntityFrameworkProvider.Expressions {
    public static class VfpExpressionBuilder {
        public static VfpSortExpression Sort(this VfpExpressionBinding input, IEnumerable<VfpSortClause> sortOrder) {
            return new VfpSortExpression(input.Expression.ResultType, input, sortOrder.ToList().AsReadOnly());
        }

        public static VfpConstantExpression Constant(object value) {
            ArgumentUtility.CheckNotNull("value", value);

            return Constant(value, value.GetType());
        }

        public static VfpInExpression In(this VfpExpression expression, VfpExpressionList list) {
            return new VfpInExpression(expression.ResultType, expression, list);
        }

        public static VfpExpressionList List(this IList<VfpExpression> list) {
            ArgumentUtility.CheckNotNull("list", list);

            return new VfpExpressionList(list);
        }

        public static VfpLikeExpression Like(TypeUsage resultType, VfpExpression argument, VfpExpression pattern, VfpExpression escape) {
            return new VfpLikeExpression(resultType, argument, pattern, escape);
        }

        public static VfpLikeCExpression LikeC(this VfpLikeExpression expression) {
            ArgumentUtility.CheckNotNull("expression", expression);

            return new VfpLikeCExpression(expression.ResultType, expression.Argument, expression.Pattern);
        }

        public static VfpPropertyExpression Property(this VfpExpression expression, EdmMember property) {
            return new VfpPropertyExpression(expression.ResultType, property, expression);
        }
        
        public static VfpPropertyExpression Property(this VfpExpression expression, TypeUsage resultType, EdmMember property) {
            return new VfpPropertyExpression(resultType, property, expression);
        }

        public static VfpConstantExpression Constant(object value, Type type) {
            return new VfpConstantExpression(type.ToTypeUsage(), value);
        }

        public static VfpNotExpression Not(this VfpExpression argument, TypeUsage resultType) {
            return new VfpNotExpression(resultType, argument);
        }

        public static VfpCastExpression Cast(this VfpExpression expression, TypeUsage type) {
            return new VfpCastExpression(type, expression);
        }

        public static VfpComparisonExpression GreaterThanOrEquals(this VfpExpression left, VfpExpression right) {
            return new VfpComparisonExpression(VfpExpressionKind.GreaterThanOrEquals, left.ResultType, left, right);
        }

        public static VfpComparisonExpression GreaterThan(this VfpExpression left, VfpExpression right) {
            return new VfpComparisonExpression(VfpExpressionKind.GreaterThan, left.ResultType, left, right);
        }

        public static VfpComparisonExpression LessThanOrEquals(this VfpExpression left, VfpExpression right) {
            return new VfpComparisonExpression(VfpExpressionKind.LessThanOrEquals, left.ResultType, left, right);
        }

        public static VfpComparisonExpression LessThan(this VfpExpression left, VfpExpression right) {
            return new VfpComparisonExpression(VfpExpressionKind.LessThan, left.ResultType, left, right);
        }

        public static VfpComparisonExpression ExpressionEquals(this VfpExpression left, VfpExpression right) {
            return new VfpComparisonExpression(VfpExpressionKind.Equals, left.ResultType, left, right);
        }

        public static VfpExpressionBinding BindAs(this VfpExpression input, string variableName) {
            var variable = input.ResultType.Variable(variableName);

            return BindAs(input, variable);
        }

        public static VfpExpressionBinding BindAs(this VfpExpression input, VfpVariableReferenceExpression variable) {
            return new VfpExpressionBinding(input, variable);
        }

        public static VfpVariableReferenceExpression Variable(this TypeUsage type, string name) {
            return new VfpVariableReferenceExpression(type, name);
        }

        public static VfpJoinExpression LeftJoin(this VfpExpressionBinding left, VfpExpressionBinding right, VfpExpression joinCondition, TypeUsage collectionOfRowResultType) {
            return Join(VfpExpressionKind.LeftOuterJoin, collectionOfRowResultType, left, right, joinCondition);
        }
        
        public static VfpJoinExpression InnerJoin(this VfpExpressionBinding left, VfpExpressionBinding right, VfpExpression joinCondition, TypeUsage collectionOfRowResultType) {
            return Join(VfpExpressionKind.InnerJoin, collectionOfRowResultType, left, right, joinCondition);
        }

        public static VfpJoinExpression Join(VfpExpressionKind kind, TypeUsage collectionOfRowResultType, VfpExpressionBinding left, VfpExpressionBinding right, VfpExpression joinCondition) {
            return new VfpJoinExpression(kind, collectionOfRowResultType, left, right, joinCondition);
        }

        public static VfpProjectExpression Project(this VfpExpressionBinding input, VfpExpression projection) {
            return new VfpProjectExpression(input.Expression.ResultType, input, projection);
        }

        public static VfpFilterExpression Filter(this VfpExpressionBinding input, VfpExpression predicate) {
            return new VfpFilterExpression(input.Expression.ResultType, input, predicate);
        }

        public static VfpAndExpression And(this VfpExpression left, VfpExpression right) {
            return new VfpAndExpression(left.ResultType, left, right);
        }

        //public static VfpAndExpression And(TypeUsage type, VfpExpression left, VfpExpression right) {
        //    return new VfpAndExpression(type, left, right);
        //}

        public static VfpLikeCExpression LikeC(TypeUsage type, VfpExpression argument, VfpExpression pattern) {
            return new VfpLikeCExpression(type, argument, pattern);
        }

        public static VfpParameterExpression Parameter(TypeUsage type, string name, VfpConstantExpression value) {
            return new VfpParameterExpression(type, name, value);
        }

        public static VfpXmlToCursorPropertyExpression XmlToCursorProperty(TypeUsage type, VfpExpression instance) {
            return new VfpXmlToCursorPropertyExpression(type, instance);
        }

        public static VfpXmlToCursorScanExpression XmlToCursorScan(VfpExpression parameter, string cursorName) {
            return new VfpXmlToCursorScanExpression(parameter, cursorName);
        }

        public static VfpXmlToCursorExpression XmlToCursor(VfpExpression property, VfpExpression parameter, string cursorName, Type itemType) {
            return new VfpXmlToCursorExpression(property, parameter, cursorName, itemType);
        }
    }
}


