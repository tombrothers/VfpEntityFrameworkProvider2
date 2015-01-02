using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Reflection;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors {
    // Converts DbExpression to VfpEntityFrameworkProvider.VfpExpressions for the purpose of rewriting the expression tree.
    internal class ExpressionConverterVisitor : DbExpressionVisitor<VfpExpression> {
        public VfpExpression Visit(DbCommandTree commandTree) {
            var queryCommandTree = commandTree as DbQueryCommandTree;

            if (queryCommandTree != null) {
                return Visit(queryCommandTree);
            }

            var updateCommandTree = commandTree as DbUpdateCommandTree;

            if (updateCommandTree != null) {
                return Visit(updateCommandTree);
            }

            var deleteCommandTree = commandTree as DbDeleteCommandTree;

            if (deleteCommandTree != null) {
                return Visit(deleteCommandTree);
            }

            var insertCommandTree = commandTree as DbInsertCommandTree;

            if (insertCommandTree != null) {
                return Visit(insertCommandTree);
            }

            var functionCommandTree = commandTree as DbFunctionCommandTree;

            if (functionCommandTree != null) {
                return Visit(functionCommandTree);
            }

            throw new NotImplementedException(commandTree.GetType().FullName);
        }

        public VfpExpression Visit(DbFunctionCommandTree commandTree) {
            return new VfpFunctionCommandTree(commandTree.EdmFunction, commandTree.ResultType, commandTree.Parameters);
        }

        public VfpExpression Visit(DbQueryCommandTree commandTree) {
            return new VfpQueryCommandTree(commandTree.Query.Accept(this),
                                          commandTree.Parameters);
        }

        public VfpExpression Visit(DbDeleteCommandTree commandTree) {
            return new VfpDeleteCommandTree(CreateDbExpressionBinding(commandTree.Target),
                                           commandTree.Predicate.Accept(this),
                                           commandTree.Parameters);
        }

        public VfpExpression Visit(DbUpdateCommandTree commandTree) {
            return new VfpUpdateCommandTree(CreateDbExpressionBinding(commandTree.Target),
                                           CreateDbSetClauses(commandTree.SetClauses),
                                           commandTree.Predicate.Accept(this),
                                           commandTree.Parameters,
                                           commandTree.Returning == null ? null : commandTree.Returning.Accept(this));
        }

        public VfpExpression Visit(DbInsertCommandTree commandTree) {
            return new VfpInsertCommandTree(CreateDbExpressionBinding(commandTree.Target),
                                           CreateDbSetClauses(commandTree.SetClauses),
                                           commandTree.Parameters,
                                           commandTree.Returning == null ? null : commandTree.Returning.Accept(this));
        }

        public override VfpExpression Visit(DbAndExpression expression) {
            return new VfpAndExpression(expression.ResultType,
                                       expression.Left.Accept(this),
                                       expression.Right.Accept(this));
        }

        public override VfpExpression Visit(DbApplyExpression expression) {
            return new VfpApplyExpression((VfpExpressionKind)expression.ExpressionKind,
                                          expression.ResultType,
                                          CreateDbExpressionBinding(expression.Input),
                                          CreateDbExpressionBinding(expression.Apply));
        }

        public override VfpExpression Visit(DbArithmeticExpression expression) {
            return new VfpArithmeticExpression((VfpExpressionKind)expression.ExpressionKind,
                                               expression.ResultType,
                                               CreateDbExpressionList(expression.Arguments));
        }

        public override VfpExpression Visit(DbCaseExpression expression) {
            return new VfpCaseExpression(expression.ResultType,
                                         CreateDbExpressionList(expression.When),
                                         CreateDbExpressionList(expression.Then),
                                         expression.Else.Accept(this));
        }

        public override VfpExpression Visit(DbCastExpression expression) {
            return new VfpCastExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbComparisonExpression expression) {
            return new VfpComparisonExpression((VfpExpressionKind)expression.ExpressionKind,
                                               expression.ResultType,
                                               expression.Left.Accept(this),
                                               expression.Right.Accept(this));
        }

        public override VfpExpression Visit(DbConstantExpression expression) {
            return new VfpConstantExpression(expression.ResultType, expression.Value);
        }

        public override VfpExpression Visit(DbCrossJoinExpression expression) {
            return new VfpCrossJoinExpression(expression.ResultType, CreateDbExpressionBindings(expression.Inputs));
        }

        public override VfpExpression Visit(DbDerefExpression expression) {
            return new VfpDerefExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbDistinctExpression expression) {
            return new VfpDistinctExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbElementExpression expression) {
            return new VfpElementExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbEntityRefExpression expression) {
            return new VfpEntityRefExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbExceptExpression expression) {
            return new VfpExceptExpression(expression.ResultType, expression.Left.Accept(this), expression.Right.Accept(this));
        }

        public override VfpExpression Visit(DbExpression expression) {
            switch (expression.ExpressionKind) {
                case DbExpressionKind.And:
                    return Visit((DbAndExpression)expression);
                //case DbExpressionKind.Any:
                //    break;
                case DbExpressionKind.Case:
                    return Visit((DbCaseExpression)expression);
                case DbExpressionKind.Cast:
                    return Visit((DbCastExpression)expression);
                case DbExpressionKind.Constant:
                    return Visit((DbConstantExpression)expression);
                case DbExpressionKind.CrossJoin:
                    return Visit((DbCrossJoinExpression)expression);
                case DbExpressionKind.Deref:
                    return Visit((DbDerefExpression)expression);
                case DbExpressionKind.Distinct:
                    return Visit((DbDistinctExpression)expression);
                case DbExpressionKind.Element:
                    return Visit((DbElementExpression)expression);
                case DbExpressionKind.EntityRef:
                    return Visit((DbEntityRefExpression)expression);
                case DbExpressionKind.Except:
                    return Visit((DbExceptExpression)expression);
                case DbExpressionKind.Filter:
                    return Visit((DbFilterExpression)expression);
                case DbExpressionKind.Function:
                    return Visit((DbFunctionExpression)expression);
                case DbExpressionKind.GroupBy:
                    return Visit((DbGroupByExpression)expression);
                case DbExpressionKind.In:
                    return Visit((DbInExpression)expression);
                case DbExpressionKind.Intersect:
                    return Visit((DbIntersectExpression)expression);
                case DbExpressionKind.IsEmpty:
                    return Visit((DbIsEmptyExpression)expression);
                case DbExpressionKind.IsNull:
                    return Visit((DbIsNullExpression)expression);
                case DbExpressionKind.IsOf:
                case DbExpressionKind.IsOfOnly:
                    return Visit((DbIsOfExpression)expression);
                case DbExpressionKind.FullOuterJoin:
                case DbExpressionKind.InnerJoin:
                case DbExpressionKind.LeftOuterJoin:
                    return Visit((DbJoinExpression)expression);
                case DbExpressionKind.Like:
                    return Visit((DbLikeExpression)expression);
                case DbExpressionKind.Limit:
                    return Visit((DbLimitExpression)expression);
                case DbExpressionKind.Divide:
                case DbExpressionKind.Minus:
                case DbExpressionKind.Modulo:
                case DbExpressionKind.Multiply:
                case DbExpressionKind.Plus:
                    return Visit((DbArithmeticExpression)expression);
                case DbExpressionKind.Equals:
                case DbExpressionKind.GreaterThan:
                case DbExpressionKind.GreaterThanOrEquals:
                case DbExpressionKind.LessThan:
                case DbExpressionKind.LessThanOrEquals:
                case DbExpressionKind.NotEquals:
                    return Visit((DbComparisonExpression)expression);
                case DbExpressionKind.NewInstance:
                    return Visit((DbNewInstanceExpression)expression);
                case DbExpressionKind.Not:
                    return Visit((DbNotExpression)expression);
                case DbExpressionKind.Null:
                    return Visit((DbNullExpression)expression);
                case DbExpressionKind.OfType:
                case DbExpressionKind.OfTypeOnly:
                    return Visit((DbOfTypeExpression)expression);
                case DbExpressionKind.Or:
                    return Visit((DbOrExpression)expression);
                case DbExpressionKind.CrossApply:
                case DbExpressionKind.OuterApply:
                    return Visit((DbApplyExpression)expression);
                case DbExpressionKind.ParameterReference:
                    return Visit((DbParameterReferenceExpression)expression);
                case DbExpressionKind.Project:
                    return Visit((DbProjectExpression)expression);
                case DbExpressionKind.Property:
                    return Visit((DbPropertyExpression)expression);
                case DbExpressionKind.Ref:
                    return Visit((DbRefExpression)expression);
                case DbExpressionKind.RefKey:
                    return Visit((DbRefKeyExpression)expression);
                case DbExpressionKind.RelationshipNavigation:
                    return Visit((DbRelationshipNavigationExpression)expression);
                case DbExpressionKind.Scan:
                    return Visit((DbScanExpression)expression);
                case DbExpressionKind.Skip:
                    return Visit((DbSkipExpression)expression);
                case DbExpressionKind.Sort:
                    return Visit((DbSortExpression)expression);
                case DbExpressionKind.Treat:
                    return Visit((DbTreatExpression)expression);
                case DbExpressionKind.UnaryMinus:
                    return Visit((DbUnaryExpression)expression);
                case DbExpressionKind.UnionAll:
                    return Visit((DbUnionAllExpression)expression);
                case DbExpressionKind.VariableReference:
                    return Visit((DbVariableReferenceExpression)expression);
                default:
                    throw new NotImplementedException(expression.ExpressionKind.ToString());
            }
        }

        public override VfpExpression Visit(DbFilterExpression expression) {
            return new VfpFilterExpression(expression.ResultType, CreateDbExpressionBinding(expression.Input), expression.Predicate.Accept(this));
        }

        public override VfpExpression Visit(DbFunctionExpression expression) {
            return new VfpFunctionExpression(expression.ResultType, expression.Function, CreateDbExpressionList(expression.Arguments));
        }

        public override VfpExpression Visit(DbGroupByExpression expression) {
            return new VfpGroupByExpression(expression.ResultType,
                                            CreateDbGroupExpressionBinding(expression.Input),
                                            CreateDbExpressionList(expression.Keys),
                                            CreateDbAggregates(expression.Aggregates));
        }

        public override VfpExpression Visit(DbInExpression expression) {
            return new VfpInExpression(expression.ResultType,
                                       expression.Item.Accept(this),
                                       CreateDbExpressionList(expression.List));
        }

        public override VfpExpression Visit(DbIntersectExpression expression) {
            return new VfpIntersectExpression(expression.ResultType,
                                              expression.Left.Accept(this),
                                              expression.Right.Accept(this));
        }

        public override VfpExpression Visit(DbIsEmptyExpression expression) {
            return new VfpIsEmptyExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbIsNullExpression expression) {
            return new VfpIsNullExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbIsOfExpression expression) {
            return new VfpIsOfExpression((VfpExpressionKind)expression.ExpressionKind,
                                         expression.ResultType,
                                         expression.Argument.Accept(this),
                                         expression.OfType);
        }

        public override VfpExpression Visit(DbJoinExpression expression) {
            return new VfpJoinExpression((VfpExpressionKind)expression.ExpressionKind,
                                         expression.ResultType,
                                         CreateDbExpressionBinding(expression.Left),
                                         CreateDbExpressionBinding(expression.Right),
                                         expression.JoinCondition.Accept(this));
        }

        public override VfpExpression Visit(DbLikeExpression expression) {
            return new VfpLikeExpression(expression.ResultType,
                                         expression.Argument.Accept(this),
                                         expression.Pattern.Accept(this),
                                         expression.Escape.Accept(this));
        }

        public override VfpExpression Visit(DbLimitExpression expression) {
            return new VfpLimitExpression(expression.ResultType,
                                          expression.Argument.Accept(this),
                                          expression.Limit.Accept(this),
                                          expression.WithTies);
        }

        public override VfpExpression Visit(DbNewInstanceExpression expression) {
            ReadOnlyCollection<VfpRelatedEntityRef> readOnlyRelationships = null;

            dynamic list = expression.GetType()
                                     .GetProperty("RelatedEntityReferences", BindingFlags.NonPublic | BindingFlags.Instance)
                                     .GetValue(expression, null);

            if (list != null) {
                var relationships = new List<VfpRelatedEntityRef>();

                foreach (dynamic item in list) {
                    relationships.Add(new VfpRelatedEntityRef(item.SourceEnd, item.TargetEnd, item.TargetEntityRef));
                }

                readOnlyRelationships = new ReadOnlyCollection<VfpRelatedEntityRef>(relationships);
            }

            return new VfpNewInstanceExpression(expression.ResultType,
                                                CreateDbExpressionList(expression.Arguments),
                                                readOnlyRelationships);
        }

        public override VfpExpression Visit(DbNotExpression expression) {
            return new VfpNotExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbNullExpression expression) {
            return new VfpNullExpression(expression.ResultType);
        }

        public override VfpExpression Visit(DbOfTypeExpression expression) {
            return new VfpOfTypeExpression((VfpExpressionKind)expression.ExpressionKind,
                                           expression.ResultType,
                                           expression.Argument.Accept(this),
                                           expression.OfType);
        }

        public override VfpExpression Visit(DbOrExpression expression) {
            return new VfpOrExpression(expression.ResultType,
                                       expression.Left.Accept(this),
                                       expression.Right.Accept(this));
        }

        public override VfpExpression Visit(DbParameterReferenceExpression expression) {
            return new VfpParameterReferenceExpression(expression.ResultType, expression.ParameterName);
        }

        public override VfpExpression Visit(DbProjectExpression expression) {
            return new VfpProjectExpression(expression.ResultType,
                                            CreateDbExpressionBinding(expression.Input),
                                            expression.Projection.Accept(this));
        }

        public override VfpExpression Visit(DbPropertyExpression expression) {
            return new VfpPropertyExpression(expression.ResultType,
                                             expression.Property,
                                             expression.Instance.Accept(this));
        }

        public override VfpExpression Visit(DbQuantifierExpression expression) {
            return new VfpQuantifierExpression((VfpExpressionKind)expression.ExpressionKind,
                                               expression.ResultType,
                                               CreateDbExpressionBinding(expression.Input),
                                               expression.Predicate.Accept(this));
        }

        public override VfpExpression Visit(DbRefExpression expression) {
            return new VfpRefExpression(expression.ResultType, expression.EntitySet, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbRefKeyExpression expression) {
            return new VfpRefKeyExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbRelationshipNavigationExpression expression) {
            return new VfpRelationshipNavigationExpression(expression.ResultType,
                                                           expression.Relationship,
                                                           expression.NavigateFrom,
                                                           expression.NavigateTo,
                                                           expression.NavigationSource.Accept(this));
        }

        public override VfpExpression Visit(DbScanExpression expression) {
            return new VfpScanExpression(expression.ResultType, expression.Target);
        }

        public override VfpExpression Visit(DbSkipExpression expression) {
            return new VfpSkipExpression(expression.ResultType,
                                         CreateDbExpressionBinding(expression.Input),
                                         CreateDbSortClauses(expression.SortOrder),
                                         expression.Count.Accept(this));
        }

        public override VfpExpression Visit(DbSortExpression expression) {
            return new VfpSortExpression(expression.ResultType,
                                         CreateDbExpressionBinding(expression.Input),
                                         CreateDbSortClauses(expression.SortOrder));
        }

        public override VfpExpression Visit(DbTreatExpression expression) {
            return new VfpTreatExpression(expression.ResultType, expression.Argument.Accept(this));
        }

        public override VfpExpression Visit(DbUnionAllExpression expression) {
            return new VfpUnionAllExpression(expression.ResultType, expression.Left.Accept(this), expression.Right.Accept(this));
        }

        public override VfpExpression Visit(DbVariableReferenceExpression expression) {
            return new VfpVariableReferenceExpression(expression.ResultType, expression.VariableName);
        }

        private ReadOnlyCollection<VfpExpressionBinding> CreateDbExpressionBindings(IEnumerable<DbExpressionBinding> bindings) {
            return bindings.Select(CreateDbExpressionBinding).ToList().AsReadOnly();
        }

        private VfpGroupExpressionBinding CreateDbGroupExpressionBinding(DbGroupExpressionBinding binding) {
            return new VfpGroupExpressionBinding(binding.Expression.Accept(this),
                                                 (VfpVariableReferenceExpression)binding.Variable.Accept(this),
                                                 (VfpVariableReferenceExpression)binding.GroupVariable.Accept(this));
        }

        private VfpExpressionBinding CreateDbExpressionBinding(DbExpressionBinding binding) {
            return new VfpExpressionBinding(binding.Expression.Accept(this), (VfpVariableReferenceExpression)binding.Variable.Accept(this));
        }

        private ReadOnlyCollection<VfpAggregate> CreateDbAggregates(IEnumerable<DbAggregate> aggregates) {
            var list = aggregates.Select(CreateDbAggregate).ToList();

            return new ReadOnlyCollection<VfpAggregate>(list);
        }

        private VfpAggregate CreateDbAggregate(DbAggregate aggregate) {
            var functionAggregate = aggregate as DbFunctionAggregate;

            if (functionAggregate != null) {
                return new VfpFunctionAggregate(functionAggregate.ResultType,
                                                CreateDbExpressionList(functionAggregate.Arguments),
                                                functionAggregate.Function,
                                                functionAggregate.Distinct);
            }

            throw new NotImplementedException(aggregate.GetType().ToString());
        }

        private VfpExpressionList CreateDbExpressionList(IEnumerable<DbExpression> list) {
            var expressions = list.Select(argument => argument.Accept(this)).ToList();

            return new VfpExpressionList(expressions);
        }

        private ReadOnlyCollection<VfpSortClause> CreateDbSortClauses(IEnumerable<DbSortClause> sortClause) {
            return sortClause.Select(item => new VfpSortClause(item.Expression.Accept(this), item.Ascending, item.Collation)).ToList().AsReadOnly();
        }

        private ReadOnlyCollection<VfpSetClause> CreateDbSetClauses(IEnumerable<DbModificationClause> setClauses) {
            return CreateDbSetClauses(setClauses.Cast<DbSetClause>());
        }

        private ReadOnlyCollection<VfpSetClause> CreateDbSetClauses(IEnumerable<DbSetClause> setClauses) {
            return setClauses.Select(item => new VfpSetClause(item.Property.Accept(this), item.Value.Accept(this))).ToList().AsReadOnly();
        }
    }
}