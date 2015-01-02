using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using VfpEntityFrameworkProvider.SqlGeneration;

namespace VfpEntityFrameworkProvider {
    internal partial class DdlBuilder {
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private readonly HashSet<EntitySet> _ignoredEntitySets = new HashSet<EntitySet>();
        private readonly TableIndexService _tableIndexService = new TableIndexService();

        private readonly bool _execScript;

        public DdlBuilder(bool execScript = false) {
            _execScript = execScript;
        }

        internal static string CreateObjectsScript(StoreItemCollection itemCollection, bool execScript = false) {
            var builder = new DdlBuilder(execScript);

            foreach (var container in itemCollection.GetItems<EntityContainer>()) {
                builder._tableIndexService.LoadTableIndexes(container);

                foreach (var entitySet in container.BaseEntitySets.OfType<EntitySet>().OrderBy(s => s.Name)) {
                    builder.AppendCreateTable(entitySet);
                }

                foreach (var associationSet in container.BaseEntitySets.OfType<AssociationSet>().OrderBy(s => s.Name)) {
                    builder.AppendCreateForeignKeyRelation(associationSet);
                }
            }

            return builder.GetCommandText();
        }

        internal string GetCommandText() {
            return _stringBuilder.ToString();
        }

        private void AppendCreateForeignKeyRelation(AssociationSet associationSet) {
            var constraint = associationSet.ElementType.ReferentialConstraints.Single();
            var principalEnd = associationSet.AssociationSetEnds[constraint.FromRole.Name];
            var dependentEnd = associationSet.AssociationSetEnds[constraint.ToRole.Name];

            // If any of the participating entity sets was skipped, skip the association too
            if (_ignoredEntitySets.Contains(principalEnd.EntitySet) || _ignoredEntitySets.Contains(dependentEnd.EntitySet)) {
                AppendSql("* Ignoring association set with participating entity set with defining query: ");
                AppendIdentifierEscapeNewLine(associationSet.Name);
            }
            else {
                AppendSql("alter table ");
                AppendIdentifier(dependentEnd.EntitySet);
                AppendSql(" add foreign key tag ");
                AppendSql(_tableIndexService.GetIndexName(dependentEnd.EntitySet, constraint.ToProperties));
                AppendSql(" references ");
                AppendIdentifier(principalEnd.EntitySet);
                AppendSql(" tag ");
                AppendSql(_tableIndexService.GetIndexName(principalEnd.EntitySet, constraint.FromProperties));

                // not sure what I should do with this...
                //if (principalEnd.CorrespondingAssociationEndMember.DeleteBehavior == OperationAction.Cascade) {
                //    AppendSql(" on delete cascade");
                //}
            }

            AppendNewLine();
        }

        private void AppendCreateTable(EntitySet entitySet) {
            //If the entity set has defining query, skip it
            if (entitySet.MetadataProperties["DefiningQuery"].Value != null) {
                AppendSql("* Ignoring entity set with defining query: ");
                AppendIdentifier(entitySet, AppendIdentifierEscapeNewLine);
                _ignoredEntitySets.Add(entitySet);
            }
            else {
                var isMigrationHistoryTable = HasMigrationHistoryPrimaryKey(entitySet);
                var formatText = !isMigrationHistoryTable;

                AppendSql("create table ");
                AppendIdentifier(entitySet);
                AppendSql(" (");

                if (formatText) {
                    AppendSql(" ;");
                    AppendNewLine();
                }

                var firstItem = true;
                var tableName = GetTableName(entitySet);

                foreach (var column in entitySet.ElementType.Properties) {
                    if (!firstItem) {
                        AppendSql(", ");

                        if (formatText) {
                            AppendSql(";");
                            AppendNewLine();
                        }
                    }

                    if (formatText) {
                        AppendSql("    ");
                    }

                    AppendIdentifier(column.Name);
                    AppendSql(" ");
                    AppendType(column, IsMigrationHistoryPrimaryKey(tableName, column));

                    firstItem = false;
                }

                if (formatText) {
                    AppendSql(" ;");
                    AppendNewLine();
                }

                AppendSql(")");

                AppendNewLine();
                AppendNewLine();

                if (isMigrationHistoryTable) {
                    AppendSql(VfpOleDb.VfpCommand.SplitCommandsToken);
                }

                AppendPrimaryKey(entitySet);
                AppendTableIndexes(entitySet);
            }

            AppendNewLine();
        }

        private static bool HasMigrationHistoryPrimaryKey(EntitySet entitySet) {
            var tableName = GetTableName(entitySet);

            return entitySet.ElementType.Properties.Any(column => IsMigrationHistoryPrimaryKey(tableName, column));
        }

        private static bool IsMigrationHistoryPrimaryKey(string tableName, EdmMember column) {
            return tableName == "__MigrationHistory" && column.Name == "MigrationId" && column.DeclaringType.Name == "HistoryRow";
        }

        private void AppendTableIndexes(EntitySet entitySet) {
            var tableName = GetTableName(entitySet);

            foreach (var tableIndex in _tableIndexService.TableIndexes.Where(x => x.TableName == tableName && !x.IsPrimaryKey)) {
                if (_execScript) {
                    AppendSql("EXECSCRIPT([USE ");
                    AppendSql(tableName);
                    AppendSql(" IN SELECT(0) EXCLUSIVE] + CHR(13) + [");
                }

                AppendSql("index on ");
                AppendSql(tableIndex.IndexExpression);
                AppendSql(" tag ");
                AppendSql(tableIndex.IndexName);

                if (_execScript) {
                    AppendSql("])");
                }

                AppendNewLine();
            }
        }

        private void AppendPrimaryKey(EntitySet entitySet) {
            if (entitySet.ElementType.KeyMembers.Count > 0) {
                var tablePrimaryKey = _tableIndexService.GetPrimaryKey(entitySet);

                AppendSql("alter table ");
                AppendIdentifier(tablePrimaryKey.TableName);
                AppendSql(" add primary key ");
                AppendSql(tablePrimaryKey.IndexExpression);
                AppendSql(" tag ");
                AppendSql(tablePrimaryKey.IndexName);
            }

            AppendNewLine();
        }

        private void AppendIdentifier(EntitySet table) {
            AppendIdentifier(table, AppendIdentifier);
        }

        private static void AppendIdentifier(EntitySet table, Action<string> AppendIdentifierEscape) {
            var tableName = GetTableName(table);

            AppendIdentifierEscape(tableName);
        }

        private void AppendIdentifier(string identifier) {
            AppendSql(identifier);
        }

        private void AppendIdentifierEscapeNewLine(string identifier) {
            AppendIdentifier(identifier.Replace("\r", "\r&&").Replace("\n", "\n&&"));
        }

        private void AppendType(EdmProperty column, bool isMigrationHistoryPrimaryKey) {
            Facet storeGenFacet;
            var type = column.TypeUsage;

            if (isMigrationHistoryPrimaryKey) {
                type = TypeUsage.CreateStringTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String), false, false, VfpClient.VfpMapping.MaximumCharacterFieldSizeThatCanBeIndex);
            }

            AppendIdentifier(SqlVisitor.GetSqlPrimitiveType(type));
            AppendSql(column.Nullable ? " null" : " not null");

            if (!column.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out storeGenFacet) || storeGenFacet.Value == null) {
                return;
            }

            var storeGenPattern = (StoreGeneratedPattern)storeGenFacet.Value;

            if (storeGenPattern == StoreGeneratedPattern.Identity) {
                AppendSql(" autoinc");
            }
        }

        private void AppendSql(string text) {
            _stringBuilder.Append(text);
        }

        private void AppendNewLine() {
            _stringBuilder.Append("\r\n");
        }

        private static string GetTableName(EntitySet entitySet) {
            var tableName = entitySet.MetadataProperties["Table"].Value as string;

            return tableName ?? entitySet.Name;
        }
    }
}