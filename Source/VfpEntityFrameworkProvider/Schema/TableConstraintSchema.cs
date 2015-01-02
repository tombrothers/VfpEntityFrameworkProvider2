using System.Data;
using System.Linq;
using ForeignKeySchema = VfpClient.VfpConnection.SchemaColumnNames.ForeignKey;
using IndexSchema = VfpClient.VfpConnection.SchemaColumnNames.Index;
using PrimaryKeySchema = VfpClient.VfpConnection.SchemaColumnNames.PrimaryKey;

namespace VfpEntityFrameworkProvider.Schema {
    internal partial class TableConstraintSchema : SchemaBase {
        internal TableConstraintSchema()
            : base(SchemaNames.TableConstraints) {
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = new DataTable(Key);

            dataTable.Columns.Add(Columns.Id);
            dataTable.Columns.Add(Columns.ParentId);
            dataTable.Columns.Add(Columns.Name);
            dataTable.Columns.Add(Columns.ConstraintType);
            dataTable.Columns.Add(Columns.IsDeferrable, typeof(bool), "false");
            dataTable.Columns.Add(Columns.IsInitiallyDeferred, typeof(bool), "false");

            if (connection.IsDbc) {
                AddDbcPrimaryKeys(connection, dataTable);
                AddDbcForeignKeys(connection, dataTable);
            }
            else {
                AddFreeTablePrimaryKeys(connection, dataTable);
            }

            // Call distinct to remove duplicate rows caused by composite primary keys 
            return dataTable.DefaultView.ToTable(true);
        }

        private void AddFreeTablePrimaryKeys(VfpConnection connection, DataTable dataTable) {
            var indexes = connection.GetSchema(VfpConnection.SchemaNames.Indexes);

            if (indexes.Rows.Count == 0) {
                return;
            }

            indexes.DefaultView.RowFilter = IndexSchema.Candidate + " = true";
            indexes = indexes.DefaultView.ToTable();

            var duplicates = indexes.AsEnumerable()
                                    .Select(x => x.Field<string>(IndexSchema.TableName))
                                    .GroupBy(x => x)
                                    .Where(x => x.Count() > 1)
                                    .Select(x => x.Key)
                                    .Distinct()
                                    .ToList();

            var query = indexes.AsEnumerable().Where(x => !duplicates.Contains(x.Field<string>(IndexSchema.TableName)));

            if (!query.Any()) {
                return;
            }

            indexes = query.CopyToDataTable();

            foreach (DataRow dataRow in indexes.Rows) {
                var newRow = dataTable.NewRow();
                newRow[Columns.Id] = "PK_" + dataRow[IndexSchema.TableName];
                newRow[Columns.ParentId] = dataRow[IndexSchema.TableName];
                newRow[Columns.Name] = "PK_" + dataRow[IndexSchema.TableName];
                newRow[Columns.ConstraintType] = ConstraintTypes.PrimaryKey;
                dataTable.Rows.Add(newRow);
            }
        }

        private static void AddDbcForeignKeys(VfpConnection connection, DataTable dataTable) {
            var foreignKeys = connection.GetSchema(VfpConnection.SchemaNames.ForeignKeys);

            foreach (DataRow dataRow in foreignKeys.Rows) {
                var newRow = dataTable.NewRow();
                newRow[Columns.Id] = dataRow[ForeignKeySchema.ForeignKeyName];
                newRow[Columns.ParentId] = dataRow[ForeignKeySchema.ForeignKeyTableName];
                newRow[Columns.Name] = newRow[Columns.Id];
                newRow[Columns.ConstraintType] = ConstraintTypes.ForeignKey;
                dataTable.Rows.Add(newRow);
            }
        }

        private static void AddDbcPrimaryKeys(VfpConnection connection, DataTable dataTable) {
            var primarykeys = connection.GetSchema(VfpConnection.SchemaNames.PrimaryKeys);

            foreach (DataRow dataRow in primarykeys.Rows) {
                DataRow newRow = dataTable.NewRow();
                newRow[Columns.Id] = dataRow[PrimaryKeySchema.IndexName];
                newRow[Columns.ParentId] = dataRow[PrimaryKeySchema.TableName];
                newRow[Columns.Name] = newRow[Columns.Id];
                newRow[Columns.ConstraintType] = ConstraintTypes.PrimaryKey;
                dataTable.Rows.Add(newRow);
            }
        }
    }
}