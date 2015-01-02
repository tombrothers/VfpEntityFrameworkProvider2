using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors {
    internal class VfpExpressionVisitor : IVfpExpressionVisitor<VfpExpression> {
        public virtual VfpExpression Visit(VfpCommandTree expression) {
            switch (expression.CommandTreeKind) {
                case VfpCommandTreeKind.Delete:
                    return Visit((VfpDeleteCommandTree)expression);
                case VfpCommandTreeKind.Function:
                    return Visit((VfpFunctionCommandTree)expression);
                case VfpCommandTreeKind.Insert:
                    return Visit((VfpInsertCommandTree)expression);
                case VfpCommandTreeKind.Query:
                    return Visit((VfpQueryCommandTree)expression);
                case VfpCommandTreeKind.Update:
                    return Visit((VfpUpdateCommandTree)expression);
                default:
                    throw new NotImplementedException(expression.CommandTreeKind.ToString());
            }
        }

        public virtual VfpExpression Visit(VfpFunctionCommandTree expression) {
            return new VfpFunctionCommandTree(expression.EdmFunction, expression.ResultType, expression.Parameters);
        }

        public virtual VfpExpression Visit(VfpQueryCommandTree expression) {
            return new VfpQueryCommandTree(expression.Query.Accept(this),
                                           expression.Parameters);
        }

        public virtual VfpExpression Visit(VfpDeleteCommandTree expression) {
            return new VfpDeleteCommandTree(VisitVfpExpressionBinding(expression.Target),
                                            expression.Predicate.Accept(this),
                                            expression.Parameters);
        }

        public virtual VfpExpression Visit(VfpUpdateCommandTree expression) {
            return new VfpUpdateCommandTree(VisitVfpExpressionBinding(expression.Target),
                                            CreateDbSetClauses(expression.SetClauses),
                                            expression.Predicate,
                                            expression.Parameters,
                                            expression.Returning == null ? null : expression.Returning.Accept(this));
        }

        public virtual VfpExpression Visit(VfpInsertCommandTree expression) {
            return new VfpInsertCommandTree(VisitVfpExpressionBinding(expression.Target),
                                            CreateDbSetClauses(expression.SetClauses),
                                            expression.Parameters,
                                            expression.Returning == null ? null : expression.Returning.Accept(this));
        }

        public virtual VfpExpression Visit(VfpPropertyVariableNameExpression expression) {
            return new VfpPropertyVariableNameExpression(expression.ResultType, expression.Property, expression.VariableName);
        }

        public virtual VfpExpression Visit(VfpParameterExpression expression) {
            return new VfpParameterExpression(expression.ResultType, expression.Name, (VfpConstantExpression)expression.Value.Accept(this));
        }

        public virtual VfpExpression Visit(VfpXmlToCursorExpression expression) {
            return new VfpXmlToCursorExpression(expression.Property.Accept(this),
                                                expression.Parameter.Accept(this),
                                                expression.CursorName,
                                                expression.ItemType);
        }

        public virtual VfpExpression Visit(VfpAndExpression expression) {
            return new VfpAndExpression(expression.ResultType,
                                        expression.Left.Accept(this),
                                        expression.Right.Accept(this));
        }

        public virtual VfpExpression Visit(VfpApplyExpression expression) {
            return new VfpApplyExpression(expression.ExpressionKind,
                                          expression.ResultType,
                                          VisitVfpExpressionBinding(expression.Input),
                                          VisitVfpExpressionBinding(expression.Apply));
        }

        public virtual VfpExpression Visit(VfpArithmeticExpression expression) {
            return new VfpArithmeticExpression(expression.ExpressionKind,
                                               expression.ResultType,
                                               VisitVfpExpressionList(expression.Arguments));

        }

        public virtual VfpExpression Visit(VfpCaseExpression expression) {
            return new VfpCaseExpression(expression.ResultType,
                                         VisitVfpExpressionList(expression.When),
                                         VisitVfpExpressionList(expression.Then),
                                         expression.Else.Accept(this));
        }

        public virtual VfpExpression Visit(VfpCastExpression expression) {
            return new VfpCastExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpComparisonExpression expression) {
            return new VfpComparisonExpression(expression.ExpressionKind,
                                               expression.ResultType,
                                               expression.Left.Accept(this),
                                               expression.Right.Accept(this));
        }

        public virtual VfpExpression Visit(VfpConstantExpression expression) {
            return expression;
        }

        public virtual VfpExpression Visit(VfpCrossJoinExpression expression) {
            return new VfpCrossJoinExpression(expression.ResultType, CreateVfpExpressionBindings(expression.Inputs));
        }

        public virtual VfpExpression Visit(VfpDerefExpression expression) {
            return new VfpDerefExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpDistinctExpression expression) {
            return new VfpDistinctExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpElementExpression expression) {
            return new VfpElementExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpEntityRefExpression expression) {
            return new VfpEntityRefExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpExceptExpression expression) {
            return new VfpExceptExpression(expression.ResultType, expression.Left.Accept(this), expression.Right.Accept(this));
        }

        public virtual VfpExpression Visit(VfpExpression expression) {
            if (expression == null) {
                return null;
            }

            switch (expression.ExpressionKind) {
                case VfpExpressionKind.PropertyVariableName:
                    return Visit((VfpPropertyVariableNameExpression)expression);
                case VfpExpressionKind.Parameter:
                    return Visit((VfpParameterExpression)expression);
                case VfpExpressionKind.And:
                    return Visit((VfpAndExpression)expression);
                //case VfpExpressionKind.Any:
                //    break;
                case VfpExpressionKind.Case:
                    return Visit((VfpCaseExpression)expression);
                case VfpExpressionKind.Cast:
                    return Visit((VfpCastExpression)expression);
                case VfpExpressionKind.Constant:
                    return Visit((VfpConstantExpression)expression);
                case VfpExpressionKind.CrossJoin:
                    return Visit((VfpCrossJoinExpression)expression);
                case VfpExpressionKind.Deref:
                    return Visit((VfpDerefExpression)expression);
                case VfpExpressionKind.Distinct:
                    return Visit((VfpDistinctExpression)expression);
                case VfpExpressionKind.Element:
                    return Visit((VfpElementExpression)expression);
                case VfpExpressionKind.EntityRef:
                    return Visit((VfpEntityRefExpression)expression);
                case VfpExpressionKind.Except:
                    return Visit((VfpExceptExpression)expression);
                case VfpExpressionKind.Filter:
                    return Visit((VfpFilterExpression)expression);
                case VfpExpressionKind.Function:
                    return Visit((VfpFunctionExpression)expression);
                case VfpExpressionKind.GroupBy:
                    return Visit((VfpGroupByExpression)expression);
                case VfpExpressionKind.In:
                    return Visit((VfpInExpression)expression);
                case VfpExpressionKind.Intersect:
                    return Visit((VfpIntersectExpression)expression);
                case VfpExpressionKind.IsEmpty:
                    return Visit((VfpIsEmptyExpression)expression);
                case VfpExpressionKind.IsNull:
                    return Visit((VfpIsNullExpression)expression);
                case VfpExpressionKind.IsOf:
                case VfpExpressionKind.IsOfOnly:
                    return Visit((VfpIsOfExpression)expression);
                case VfpExpressionKind.FullOuterJoin:
                case VfpExpressionKind.InnerJoin:
                case VfpExpressionKind.LeftOuterJoin:
                    return Visit((VfpJoinExpression)expression);
                case VfpExpressionKind.Like:
                    return Visit((VfpLikeExpression)expression);
                case VfpExpressionKind.LikeC:
                    return Visit((VfpLikeCExpression)expression);
                case VfpExpressionKind.Limit:
                    return Visit((VfpLimitExpression)expression);
                case VfpExpressionKind.Divide:
                case VfpExpressionKind.Minus:
                case VfpExpressionKind.Modulo:
                case VfpExpressionKind.Multiply:
                case VfpExpressionKind.Plus:
                    return Visit((VfpArithmeticExpression)expression);
                case VfpExpressionKind.Equals:
                case VfpExpressionKind.GreaterThan:
                case VfpExpressionKind.GreaterThanOrEquals:
                case VfpExpressionKind.LessThan:
                case VfpExpressionKind.LessThanOrEquals:
                case VfpExpressionKind.NotEquals:
                    return Visit((VfpComparisonExpression)expression);
                case VfpExpressionKind.NewInstance:
                    return Visit((VfpNewInstanceExpression)expression);
                case VfpExpressionKind.Not:
                    return Visit((VfpNotExpression)expression);
                case VfpExpressionKind.Null:
                    return Visit((VfpNullExpression)expression);
                case VfpExpressionKind.OfType:
                case VfpExpressionKind.OfTypeOnly:
                    return Visit((VfpOfTypeExpression)expression);
                case VfpExpressionKind.Or:
                    return Visit((VfpOrExpression)expression);
                case VfpExpressionKind.CrossApply:
                case VfpExpressionKind.OuterApply:
                    return Visit((VfpApplyExpression)expression);
                case VfpExpressionKind.ParameterReference:
                    return Visit((VfpParameterReferenceExpression)expression);
                case VfpExpressionKind.Project:
                    return Visit((VfpProjectExpression)expression);
                case VfpExpressionKind.Property:
                    return Visit((VfpPropertyExpression)expression);
                case VfpExpressionKind.Ref:
                    return Visit((VfpRefExpression)expression);
                case VfpExpressionKind.RefKey:
                    return Visit((VfpRefKeyExpression)expression);
                case VfpExpressionKind.RelationshipNavigation:
                    return Visit((VfpRelationshipNavigationExpression)expression);
                case VfpExpressionKind.Scan:
                    return Visit((VfpScanExpression)expression);
                case VfpExpressionKind.Skip:
                    return Visit((VfpSkipExpression)expression);
                case VfpExpressionKind.Sort:
                    return Visit((VfpSortExpression)expression);
                case VfpExpressionKind.Treat:
                    return Visit((VfpTreatExpression)expression);
                case VfpExpressionKind.UnaryMinus:
                    return Visit((VfpUnaryExpression)expression);
                case VfpExpressionKind.UnionAll:
                    return Visit((VfpUnionAllExpression)expression);
                case VfpExpressionKind.VariableReference:
                    return Visit((VfpVariableReferenceExpression)expression);
                case VfpExpressionKind.CommandTree:
                    return Visit((VfpCommandTree)expression);

                case VfpExpressionKind.XmlToCursor:
                    return Visit((VfpXmlToCursorExpression)expression);
                case VfpExpressionKind.XmlToCursorScan:
                    return Visit((VfpXmlToCursorScanExpression)expression);
                case VfpExpressionKind.XmlToCursorProperty:
                    return Visit((VfpXmlToCursorPropertyExpression)expression);
                default:
                    throw new NotImplementedException(expression.ExpressionKind.ToString());
            }
        }

        public virtual VfpExpression Visit(VfpXmlToCursorPropertyExpression expression) {
            return new VfpXmlToCursorPropertyExpression(expression.ResultType, expression.Instance.Accept(this));
        }

        public virtual VfpExpression Visit(VfpXmlToCursorScanExpression expression) {
            return new VfpXmlToCursorScanExpression(expression.Parameter.Accept(this), expression.CursorName);
        }

        public virtual VfpExpression Visit(VfpFilterExpression expression) {
            return new VfpFilterExpression(expression.ResultType, VisitVfpExpressionBinding(expression.Input), expression.Predicate.Accept(this));
        }

        public virtual VfpExpression Visit(VfpFunctionExpression expression) {
            return new VfpFunctionExpression(expression.ResultType, expression.Function, VisitVfpExpressionList(expression.Arguments));
        }

        public virtual VfpExpression Visit(VfpGroupByExpression expression) {
            return new VfpGroupByExpression(expression.ResultType,
                                            CreateDbGroupExpressionBinding(expression.Input),
                                            VisitVfpExpressionList(expression.Keys),
                                            CreateDbAggregates(expression.Aggregates));
        }

        public virtual VfpExpression Visit(VfpInExpression expression) {
            return new VfpInExpression(expression.ResultType,
                                       expression.Item.Accept(this),
                                       VisitVfpExpressionList(expression.List));
        }

        public virtual VfpExpression Visit(VfpIntersectExpression expression) {
            return new VfpIntersectExpression(expression.ResultType,
                                              expression.Left.Accept(this),
                                              expression.Right.Accept(this));
        }

        public virtual VfpExpression Visit(VfpIsEmptyExpression expression) {
            return new VfpIsEmptyExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpIsNullExpression expression) {
            return new VfpIsNullExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpIsOfExpression expression) {
            return new VfpIsOfExpression(expression.ExpressionKind,
                                         expression.ResultType,
                                         expression.Argument.Accept(this),
                                         expression.OfType);
        }

        public virtual VfpExpression Visit(VfpJoinExpression expression) {
            return new VfpJoinExpression(expression.ExpressionKind,
                                         expression.ResultType,
                                         VisitVfpExpressionBinding(expression.Left),
                                         VisitVfpExpressionBinding(expression.Right),
                                         expression.JoinCondition.Accept(this));
        }

        public virtual VfpExpression Visit(VfpLikeExpression expression) {
            return new VfpLikeExpression(expression.ResultType,
                                         expression.Argument.Accept(this),
                                         expression.Pattern.Accept(this),
                                         expression.Escape.Accept(this));
        }

        public virtual VfpExpression Visit(VfpLikeCExpression expression) {
            return new VfpLikeCExpression(expression.ResultType,
                                          expression.Argument.Accept(this),
                                          expression.Pattern.Accept(this));
        }

        public virtual VfpExpression Visit(VfpLimitExpression expression) {
            return new VfpLimitExpression(expression.ResultType,
                                          expression.Argument.Accept(this),
                                          expression.Limit.Accept(this),
                                          expression.WithTies);
        }

        public virtual VfpExpression Visit(VfpNewInstanceExpression expression) {
            return new VfpNewInstanceExpression(expression.ResultType,
                                                VisitVfpExpressionList(expression.Arguments),
                                                CreateVfpRelatedEntityRefList(expression.Relationships));
        }

        public virtual VfpExpression Visit(VfpNotExpression expression) {
            return new VfpNotExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpNullExpression expression) {
            return expression;
        }

        public virtual VfpExpression Visit(VfpOfTypeExpression expression) {
            return new VfpOfTypeExpression(expression.ExpressionKind,
                                           expression.ResultType,
                                           expression.Argument.Accept(this),
                                           expression.OfType);
        }

        public virtual VfpExpression Visit(VfpOrExpression expression) {
            return new VfpOrExpression(expression.ResultType,
                                       expression.Left.Accept(this),
                                       expression.Right.Accept(this));
        }

        public virtual VfpExpression Visit(VfpParameterReferenceExpression expression) {
            return new VfpParameterReferenceExpression(expression.ResultType, expression.ParameterName);
        }

        public virtual VfpExpression Visit(VfpProjectExpression expression) {
            var projection = expression.Projection.Accept(this);
            var input = VisitVfpExpressionBinding(expression.Input);

            return new VfpProjectExpression(expression.ResultType, input, projection);
        }

        public virtual VfpExpression Visit(VfpPropertyExpression expression) {
            return new VfpPropertyExpression(expression.ResultType,
                                             expression.Property,
                                             expression.Instance.Accept(this));
        }

        public virtual VfpExpression Visit(VfpQuantifierExpression expression) {
            return new VfpQuantifierExpression(expression.ExpressionKind,
                                               expression.ResultType,
                                               VisitVfpExpressionBinding(expression.Input),
                                               expression.Predicate.Accept(this));
        }

        public virtual VfpExpression Visit(VfpRefExpression expression) {
            return new VfpRefExpression(expression.ResultType, expression.EntitySet, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpRefKeyExpression expression) {
            return new VfpRefKeyExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpRelationshipNavigationExpression expression) {
            return new VfpRelationshipNavigationExpression(expression.ResultType,
                                                           expression.Relationship,
                                                           expression.NavigateFrom,
                                                           expression.NavigateTo,
                                                           expression.NavigationSource.Accept(this));
        }

        public virtual VfpExpression Visit(VfpScanExpression expression) {
            return expression;
        }

        public virtual VfpExpression Visit(VfpSkipExpression expression) {
            return new VfpSkipExpression(expression.ResultType,
                                         VisitVfpExpressionBinding(expression.Input),
                                         CreateDbSortClauses(expression.SortOrder),
                                         expression.Count.Accept(this));
        }

        public virtual VfpExpression Visit(VfpSortExpression expression) {
            return new VfpSortExpression(expression.ResultType,
                                         VisitVfpExpressionBinding(expression.Input),
                                         CreateDbSortClauses(expression.SortOrder));
        }

        public virtual VfpExpression Visit(VfpTreatExpression expression) {
            return new VfpTreatExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public virtual VfpExpression Visit(VfpUnionAllExpression expression) {
            return new VfpUnionAllExpression(expression.ResultType, expression.Left.Accept(this), expression.Right.Accept(this));
        }

        public virtual VfpExpression Visit(VfpVariableReferenceExpression expression) {
            return new VfpVariableReferenceExpression(expression.ResultType, expression.VariableName);
        }

        protected ReadOnlyCollection<VfpSortClause> VisitNullHandler(ReadOnlyCollection<VfpSortClause> sortClauses) {
            return sortClauses == null ? null : VisitSortClauses(sortClauses);
        }

        protected virtual ReadOnlyCollection<VfpSortClause> VisitSortClauses(ReadOnlyCollection<VfpSortClause> list) {
            var sortClauses = new List<VfpSortClause>();

            for (int index = 0, total = list.Count; index < total; index++) {
                var item = list[index];

                item = new VfpSortClause(item.Expression.Accept(this), item.Ascending, item.Collation);

                sortClauses.Add(item);
            }

            return sortClauses.AsReadOnly();
        }

        protected VfpVariableReferenceExpression VisitNullHandler(VfpVariableReferenceExpression expression) {
            return expression;
        }

        protected VfpExpressionList VisitNullHandler(VfpExpressionList expression) {
            return expression == null ? null : VisitVfpExpressionList(expression);
        }

        protected VfpExpression VisitNullHandler(VfpExpression expression) {
            return expression == null ? null : expression.Accept(this);
        }

        protected ReadOnlyCollection<VfpExpressionBinding> CreateVfpExpressionBindings(IList<VfpExpressionBinding> bindings) {
            return bindings.Select(VisitVfpExpressionBinding).ToList().AsReadOnly();
        }

        protected VfpGroupExpressionBinding CreateDbGroupExpressionBinding(VfpGroupExpressionBinding binding) {
            return new VfpGroupExpressionBinding(binding.Expression.Accept(this),
                                                 (VfpVariableReferenceExpression)binding.Variable.Accept(this),
                                                 (VfpVariableReferenceExpression)binding.GroupVariable.Accept(this));
        }

        protected virtual VfpExpressionBinding VisitVfpExpressionBinding(VfpExpressionBinding binding) {
            return new VfpExpressionBinding(binding.Expression.Accept(this), (VfpVariableReferenceExpression)binding.Variable.Accept(this));
        }

        protected virtual ReadOnlyCollection<VfpAggregate> CreateDbAggregates(IList<VfpAggregate> aggregates) {
            return aggregates.Select(CreateDbAggregate).ToList().AsReadOnly();
        }

        protected VfpAggregate CreateDbAggregate(VfpAggregate aggregate) {
            var functionAggregate = aggregate as VfpFunctionAggregate;

            if (functionAggregate != null) {
                return CreateDbFunctionAggregate(functionAggregate);
            }

            throw new NotImplementedException(aggregate.GetType().ToString());
        }

        protected virtual VfpFunctionAggregate CreateDbFunctionAggregate(VfpFunctionAggregate functionAggregate) {
            return new VfpFunctionAggregate(functionAggregate.ResultType,
                                            VisitVfpExpressionList(functionAggregate.Arguments),
                                            functionAggregate.Function,
                                            functionAggregate.Distinct);
        }

        protected virtual VfpExpressionList VisitVfpExpressionList(IList<VfpExpression> list) {
            var expressions = new List<VfpExpression>();

            for (int index = 0, total = list.Count; index < total; index++) {
                var item = list[index].Accept(this);

                expressions.Add(item);
            }

            return new VfpExpressionList(expressions);
        }

        protected virtual ReadOnlyCollection<VfpSortClause> CreateDbSortClauses(IList<VfpSortClause> sortClause) {
            return sortClause.Select(item => new VfpSortClause(item.Expression.Accept(this), item.Ascending, item.Collation)).ToList().AsReadOnly();
        }

        protected virtual ReadOnlyCollection<VfpRelatedEntityRef> CreateVfpRelatedEntityRefList(ReadOnlyCollection<VfpRelatedEntityRef> relationships) {
            if (relationships == null) {
                return null;
            }

            return relationships.Select(item => new VfpRelatedEntityRef(item.SourceEnd, item.TargetEnd, item.TargetEntityRef.Accept(this))).ToList().AsReadOnly();
        }

        protected ReadOnlyCollection<VfpSetClause> CreateDbSetClauses(IEnumerable<VfpSetClause> setClauses) {
            return setClauses.Select(item => new VfpSetClause(item.Property.Accept(this), item.Value.Accept(this))).ToList().AsReadOnly();
        }

        public VfpExpression Visit(VfpLambdaExpression expression) {
            throw new NotImplementedException();
        }
    }
}