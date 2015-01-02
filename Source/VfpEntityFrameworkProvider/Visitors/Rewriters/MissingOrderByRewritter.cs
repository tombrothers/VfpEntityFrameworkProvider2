using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    /// <summary>
    /// The purpose of this class is to ensure that a "Select Top X" statement includes an "Order By" clause.
    /// </summary>
    internal class MissingOrderByRewritter : VfpExpressionVisitor {
        private VfpExpression _rootExpression;

        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new MissingOrderByRewritter { _rootExpression = expression };

            return rewriter.Visit(expression);
        }

        public override VfpExpression Visit(VfpLimitExpression expression) {
            var sortExpression = expression.Argument as VfpSortExpression;

            if (sortExpression == null) {
                var newLimitExpression = GetVfpLimitExpression(expression, expression.Argument as VfpProjectExpression) ??
                                         GetVfpLimitExpression(expression, expression.Argument as VfpFilterExpression) ??
                                         GetVfpLimitExpression(expression, expression.Argument as VfpScanExpression);

                if (newLimitExpression != null) {
                    return base.Visit(newLimitExpression);
                }
            }

            return base.Visit(expression);
        }

        private VfpLimitExpression GetVfpLimitExpression(VfpLimitExpression limitExpression, VfpScanExpression scanExpression) {
            if (scanExpression == null) {
                return null;
            }

            var keyMembers = GetKeyMembers(scanExpression.Target.ElementType.KeyMembers);

            if (!keyMembers.Any()) {
                return null;
            }

            var variableReference = new VfpVariableReferenceExpression(scanExpression.ResultType, GetUniqueVariableName());
            var expressionBinding = new VfpExpressionBinding(scanExpression, variableReference);
            var sortClauses = GetSortClauses(keyMembers, expressionBinding).ToList().AsReadOnly();
            var sortExpression = new VfpSortExpression(scanExpression.ResultType, expressionBinding, sortClauses);

            return new VfpLimitExpression(limitExpression.ResultType,
                                          sortExpression,
                                          limitExpression.Limit,
                                          limitExpression.WithTies);
        }

        private static VfpLimitExpression GetVfpLimitExpression(VfpLimitExpression limitExpression, VfpFilterExpression filterExpression) {
            var keyMembers = GetKeyMembers(filterExpression);

            if (!keyMembers.Any()) {
                return null;
            }

            var sortBinding = GetSortBinding(keyMembers, filterExpression.Input, filterExpression);
            var newFilter = sortBinding.Filter(filterExpression.Predicate);

            return new VfpLimitExpression(limitExpression.ResultType,
                                          newFilter,
                                          limitExpression.Limit,
                                          limitExpression.WithTies);
        }

        private static IEnumerable<EdmMember> GetKeyMembers(VfpFilterExpression expression) {
            return expression == null ? new EdmMember[] { } : GetKeyMembers(expression.Input, expression);
        }

        private static VfpLimitExpression GetVfpLimitExpression(VfpLimitExpression limitExpression, VfpProjectExpression projectExpression) {
            var keyMembers = GetKeyMembers(projectExpression);

            if (!keyMembers.Any()) {
                return null;
            }

            var sortBinding = GetSortBinding(keyMembers, projectExpression.Input, projectExpression);
            var newProject = sortBinding.Project(projectExpression.Projection);

            return new VfpLimitExpression(limitExpression.ResultType,
                                          newProject,
                                          limitExpression.Limit,
                                          limitExpression.WithTies);
        }

        private static IEnumerable<EdmMember> GetKeyMembers(VfpProjectExpression expression) {
            return expression == null ? new EdmMember[] { } : GetKeyMembers(expression.Input, expression);
        }

        private static VfpExpressionBinding GetSortBinding(IEnumerable<EdmMember> keyMembers, VfpExpressionBinding binding, VfpExpression expression) {
            var allVarialbes = VariableReferenceGatherer.Gather(expression).ToList();
            var entityTypeVariables = allVarialbes.Where(x => x.ResultType.EdmType is EntityType).ToList().GroupBy(x => x.VariableName).Select(x => x.First()).ToList();
            var keyMembersWithVariables = keyMembers.Join(entityTypeVariables, x => x.DeclaringType, x => x.ResultType.EdmType, (k, v) => new { KeyMember = k, Variable = v }).ToList();
            var sortClauses = keyMembersWithVariables.Select(x => new VfpSortClause(new VfpPropertyVariableNameExpression(x.KeyMember.TypeUsage, x.KeyMember, x.Variable.VariableName), true, string.Empty)).ToList();
            var sortExpression = binding.Sort(sortClauses);
            
            return sortExpression.BindAs(binding.Variable);
        }

        private string GetUniqueVariableName() {
            var list = GetVariableNames();
            var attempt = list.Count(x => x.StartsWith("t"));
            var variableName = "t" + attempt;

            while (list.Contains(variableName)) {
                attempt += 1;
                variableName = "t" + attempt;
            }

            return variableName;
        }

        private IList<string> GetVariableNames() {
            var list = VariableReferenceGatherer.Gather(_rootExpression);

            return list.Select(x => x.VariableName).ToList();
        }

        private static IEnumerable<VfpSortClause> GetSortClauses(IEnumerable<EdmMember> keyMembers, VfpExpressionBinding expressionBinding) {
            return keyMembers.Select(x => new VfpSortClause(new VfpPropertyExpression(x.TypeUsage, x, expressionBinding.Variable), true, string.Empty));
        }
        
        private static IEnumerable<EdmMember> GetKeyMembers(VfpExpressionBinding expressionBinding, VfpExpression expression) {
            var keyMembers = GetKeyMembers(expressionBinding.VariableType.EdmType);

            if (keyMembers.Any()) {
                return keyMembers;
            }

            var entityTypes = EntityTypeGatherer.Gather(expression);

            return GetKeyMembers(entityTypes);
        }

        private static IEnumerable<EdmMember> GetKeyMembers(EdmType edmType) {
            var entityType = edmType as EntityType;

            if (entityType == null) {
                var collectionType = edmType as CollectionType;

                if (collectionType != null) {
                    entityType = collectionType.TypeUsage.EdmType as EntityType;

                    if (entityType == null) {
                        var rowType = collectionType.TypeUsage.EdmType as RowType;

                        if (rowType != null) {
                            return GetKeyMembers(rowType.DeclaredProperties.Select(x => x.TypeUsage.EdmType).OfType<EntityType>().SelectMany(x => x.MetadataProperties));
                        }
                    }

                }
            }

            if (entityType == null) {
                return new EdmMember[] { };
            }

            return GetKeyMembers(entityType.MetadataProperties);
        }

        private static IEnumerable<EdmMember> GetKeyMembers(IEnumerable<EntityType> entityTypes) {
            if (entityTypes == null) {
                return new EdmMember[] { };
            }

            return entityTypes.SelectMany(x => GetKeyMembers(x.MetadataProperties));
        }

        private static IEnumerable<EdmMember> GetKeyMembers(IEnumerable<MetadataProperty> properties) {
            if (properties == null) {
                return new EdmMember[] { };
            }

            return properties.Where(x => x.Name == "KeyMembers").SelectMany(x => GetKeyMembers(x.Value as IEnumerable<EdmMember>));
        }

        private static IEnumerable<EdmMember> GetKeyMembers(IEnumerable<EdmMember> properties) {
            return properties ?? new EdmMember[] { };
        }
    }
}