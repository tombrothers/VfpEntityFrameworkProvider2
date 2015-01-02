using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Linq;

namespace VfpEntityFrameworkProvider {
    internal class TableIndexService {
        private readonly List<TableIndex> _tableIndexes = new List<TableIndex>();

        public IEnumerable<TableIndex> TableIndexes { get { return _tableIndexes; } }

        public TableIndex GetPrimaryKey(EntitySet entitySet) {
            ArgumentUtility.CheckNotNull("entitySet", entitySet);

            return GetTableIndex(GetTableName(entitySet), entitySet.ElementType.KeyMembers.Select(x => x.Name));
        }

        public TableIndex GetTableIndex(string tableName, IEnumerable<string> columnNames, string indexName = null) {
            ArgumentUtility.CheckNotNullOrEmpty("tableName", tableName);
            ArgumentUtility.CheckNotNull("columnNames", columnNames);

            tableName = GetTableName(tableName);
            var indexExpression = GetIndexExpression(columnNames);

            var query = _tableIndexes.Where(x => x.TableName == tableName)
                                     .Where(x => x.IndexExpression == indexExpression);

            if (string.IsNullOrWhiteSpace(indexName)) {
                return query.Single();    
            }

            return query.SingleOrDefault(x => x.IndexName == indexName);
        }

        public void LoadTableIndexes(IEnumerable<MigrationOperation> migrationOperations) {
            ArgumentUtility.CheckNotNull("migrationOperations", migrationOperations);

            foreach (var operation in migrationOperations) {
                AddTablePrimaryIndex(operation as CreateTableOperation);
                AddTablePrimaryIndex(operation as PrimaryKeyOperation);
                AddTableIndex(operation as IndexOperation);
                AddTableForeignKeyIndex(operation as ForeignKeyOperation);
            }

            SetIndexNames();
        }

        private void AddTableForeignKeyIndex(ForeignKeyOperation operation) {
            if (operation == null) {
                return;
            }

            AddTableIndex(operation.DependentTable, operation.DependentColumns);

            var addOperation = operation as AddForeignKeyOperation;

            if (addOperation != null) {
                AddTableIndex(addOperation.PrincipalTable, addOperation.PrincipalColumns);
            }
        }

        public void LoadTableIndexes(EntityContainer container) {
            ArgumentUtility.CheckNotNull("container", container);

            // add primary keys
            foreach (var entitySet in container.BaseEntitySets.OfType<EntitySet>().OrderBy(s => s.Name)) {
                AddTablePrimaryIndex(GetTableName(entitySet), entitySet.ElementType.KeyMembers);
            }

            // add indexes based on foreign keys
            foreach (var associationSet in container.BaseEntitySets.OfType<AssociationSet>().OrderBy(s => s.Name)) {
                var constraint = associationSet.ElementType.ReferentialConstraints.Single();
                var principalEnd = associationSet.AssociationSetEnds[constraint.FromRole.Name];
                var dependentEnd = associationSet.AssociationSetEnds[constraint.ToRole.Name];

                AddTableIndex(GetTableName(dependentEnd.EntitySet), constraint.ToProperties);
                AddTableIndex(GetTableName(principalEnd.EntitySet), constraint.FromProperties);
            }

            SetIndexNames();
        }

        private void SetIndexNames() {
            // shorten the index names to 10 characters to adhere to vfp naming restrictions
            _tableIndexes.Where(x => x.IndexName.Length > VfpClient.VfpMapping.MaximumIndexNameLength)
                         .ToList()
                         .ForEach(x => x.IndexName = x.IndexName.Substring(0, VfpClient.VfpMapping.MaximumIndexNameLength));

            var tableNames = _tableIndexes.Select(x => x.TableName).Distinct().ToArray();

            foreach (var tableName in tableNames) {
                var tableIndexes = _tableIndexes.Where(x => x.TableName == tableName);
                var indexQuery = tableIndexes.GroupBy(x => x.IndexName, x => x);

                while (indexQuery.Any(x => x.Count() > 1)) {
                    foreach (var indexGroup in indexQuery.Where(x => x.Count() > 1)) {
                        var counter = -1;
                        var newName = string.Empty;

                        foreach (var indexItem in indexGroup) {
                            counter++;

                            // Don't rename the first item.
                            if (counter == 0) {
                                continue;
                            }

                            while (true) {
                                var number = counter.ToString();

                                newName = indexItem.IndexName;

                                if (newName.Length + number.Length > VfpClient.VfpMapping.MaximumIndexNameLength) {
                                    newName = newName.Substring(0, VfpClient.VfpMapping.MaximumIndexNameLength - number.Length);
                                }

                                newName = newName + number;

                                if (!tableIndexes.Any(x => x.IndexName == newName)) {
                                    indexItem.IndexName = newName;

                                    break;
                                }

                                counter++;
                            }
                        }
                    }
                }
            }
        }

        public string GetIndexName(EntitySet entitySet, IEnumerable<EdmMember> members) {
            ArgumentUtility.CheckNotNull("entitySet", entitySet);
            ArgumentUtility.CheckNotNull("members", members);

            return GetIndexName(GetTableName(entitySet), members.Select(x => x.Name));
        }

        public string GetIndexName(string tableName, IEnumerable<string> columnNames) {
            var indexExpression = GetIndexExpression(columnNames);

            return _tableIndexes.Single(x => x.TableName == GetTableName(tableName) && x.IndexExpression == indexExpression).IndexName;
        }

        private static string GetTableName(EntitySet entitySet) {
            var tableName = entitySet.MetadataProperties["Table"].Value as string;

            return tableName ?? entitySet.Name;
        }

        private void AddTablePrimaryIndex(CreateTableOperation operation) {
            if (operation == null) {
                return;
            }

            AddTablePrimaryIndex(operation.PrimaryKey);
        }

        private void AddTablePrimaryIndex(PrimaryKeyOperation operation) {
            if (operation == null) {
                return;
            }

            AddTableIndex(operation.Table, operation.Columns, true);
        }

        private void AddTablePrimaryIndex(string tableName, IEnumerable<EdmMember> properties) {
            if (properties == null || !properties.Any()) {
                return;
            }

            AddTableIndex(tableName, properties.Select(x => x.Name), true);
        }

        private void AddTableIndex(IndexOperation operation) {
            if (operation == null) {
                return;
            }

            AddTableIndex(operation.Table, operation.Columns, false, operation.Name);
        }

        private void AddTableIndex(string tableName, IEnumerable<EdmMember> properties) {
            if (properties == null) {
                return;
            }

            foreach (EdmProperty property in properties) {
                AddTableIndex(tableName, property.Name);
            }
        }

        private void AddTableIndex(string tableName, string columnName) {
            AddTableIndex(tableName, new[] { columnName }, false);
        }

        private void AddTableIndex(string tableName, IEnumerable<string> columnNames, bool isPrimaryKey = false, string indexName = null) {
            if (columnNames == null || !columnNames.Any()) {
                return;
            }

            var indexExpression = GetIndexExpression(columnNames);

            var tableIndex = new TableIndex {
                TableName = GetTableName(tableName),
                IndexExpression = indexExpression,
                IndexName = indexName ?? GetIndexName(indexExpression),
                IsPrimaryKey = isPrimaryKey
            };

            if (!TableIndexExists(tableIndex)) {
                _tableIndexes.Add(tableIndex);
            }
        }

        private static string GetTableName(string tableName) {
            ArgumentUtility.CheckNotNullOrEmpty("tableName", tableName);

            if (tableName.Contains(".")) {
                tableName = tableName.Substring(tableName.IndexOf(".") + 1);
            }

            return tableName;
        }

        private bool TableIndexExists(TableIndex tableIndex) {
            return _tableIndexes.FirstOrDefault(x => x.TableName == tableIndex.TableName && x.IndexExpression == tableIndex.IndexExpression) != null;
        }

        private static string GetIndexName(string indexExpression) {
            return indexExpression.Replace("TRANS(", string.Empty)
                                  .Replace(")", string.Empty)
                                  .Replace("+", "_");
        }

        private static string GetIndexExpression(IEnumerable<EdmMember> properties) {
            if (properties == null || !properties.Any()) {
                return null;
            }

            return GetIndexExpression(properties.Select(x => x.Name));
        }

        private static string GetIndexExpression(IEnumerable<string> columnNames) {
            var expression = string.Empty;
            var columnCount = columnNames.Count();

            if (columnCount > 1) {
                var firstItem = true;
                foreach (var column in columnNames) {
                    if (!firstItem) {
                        expression += "+";
                    }

                    expression += "TRANS(" + column + ")";
                    firstItem = false;
                }
            }
            else {
                expression += columnNames.Single();
            }

            return expression;
        }
    }
}