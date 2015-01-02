using System.Data;
using System.Linq;
using ForeignKeySchema = VfpClient.VfpConnection.SchemaColumnNames.ForeignKey;
using IndexSchema = VfpClient.VfpConnection.SchemaColumnNames.Index;
using PrimaryKeySchema = VfpClient.VfpConnection.SchemaColumnNames.PrimaryKey;

namespace VfpEntityFrameworkProvider.Schema {
    internal abstract class TableOrViewConstraintColumnBase : SchemaBase {
        private readonly bool _isView;

        internal TableOrViewConstraintColumnBase(string key, bool isView)
            : base(key) {
            ArgumentUtility.CheckNotNullOrEmpty("key", key);
            
            _isView = isView;
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = new DataTable(Key);

            dataTable.Columns.Add("ConstraintId");
            dataTable.Columns.Add("ColumnId");

            if (!_isView) {
                if (connection.IsDbc) {
                    AddDbcPrimaryKeys(connection, dataTable);
                    AddDbcForeignKeys(connection, dataTable);
                }
                else {
                    AddFreeTablePrimaryKeys(connection, dataTable);
                }
            }

            dataTable.DefaultView.ToTable(dataTable.TableName, true);

            return dataTable;
        }

        private static void AddFreeTablePrimaryKeys(VfpConnection connection, DataTable dataTable) {
            var indexes = connection.GetSchema(VfpConnection.SchemaNames.Indexes);

            if (indexes.Rows.Count == 0) {
                return;
            }

            indexes.DefaultView.RowFilter = IndexSchema.Candidate + " = true";
            indexes = indexes.DefaultView.ToTable();

            if (indexes.Rows.Count == 0) {
                return;
            }

            var duplicates = indexes.AsEnumerable()
                                    .Select(x => x.Field<string>(IndexSchema.TableName))
                                    .GroupBy(x => x)
                                    .Where(x => x.Count() > 1)
                                    .Select(x => x.Key)
                                    .Distinct()
                                    .ToList();

            var query = indexes.AsEnumerable()
                               .Where(x => !duplicates.Contains(x.Field<string>(IndexSchema.TableName)));

            if (!query.Any()) {
                return;    
            }

            indexes = query.CopyToDataTable();

            foreach (DataRow dataRow in indexes.Rows) {
                var newRow = dataTable.NewRow();

                newRow["ConstraintId"] = "PK_" + dataRow[IndexSchema.TableName];
                newRow["ColumnId"] = string.Format("{0}.{1}",
                                                    dataRow[IndexSchema.TableName],
                                                    dataRow[IndexSchema.FieldName]);
                dataTable.Rows.Add(newRow);
            }
        }

        private void AddDbcForeignKeys(VfpConnection connection, DataTable dataTable) {
            var foreignKeys = connection.GetSchema(VfpConnection.SchemaNames.ForeignKeys);

            foreach (DataRow dataRow in foreignKeys.Rows) {
                var newRow = dataTable.NewRow();

                newRow["ConstraintId"] = dataRow[ForeignKeySchema.ForeignKeyName];
                newRow["ColumnId"] = string.Format("{0}.{1}",
                                    dataRow[ForeignKeySchema.ForeignKeyTableName],
                                    dataRow[ForeignKeySchema.ForeignKeyFieldName]);

                dataTable.Rows.Add(newRow);
            }
        }

        private static void AddDbcPrimaryKeys(VfpConnection connection, DataTable dataTable) {
            var primarykeys = connection.GetSchema(VfpConnection.SchemaNames.PrimaryKeys);

            foreach (DataRow dataRow in primarykeys.Rows) {
                var newRow = dataTable.NewRow();

                newRow["ConstraintId"] = dataRow[PrimaryKeySchema.IndexName];
                newRow["ColumnId"] = string.Format("{0}.{1}",
                                                   dataRow[PrimaryKeySchema.TableName],
                                                   dataRow[PrimaryKeySchema.FieldName]);

                dataTable.Rows.Add(newRow);
            }
        }
    }
}