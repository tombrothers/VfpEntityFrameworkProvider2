using System.Data;
using ForeignKeySchema = VfpClient.VfpConnection.SchemaColumnNames.ForeignKey;

namespace VfpEntityFrameworkProvider.Schema {
    internal abstract class TableOrViewForiegnKeyBase : SchemaBase {
        private readonly bool _isView;

        protected TableOrViewForiegnKeyBase(string key, bool isView)
            : base(key) {
            ArgumentUtility.CheckNotNullOrEmpty("key", key);

            _isView = isView;
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = new DataTable(Key);
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("ToColumnId");
            dataTable.Columns.Add("FromColumnId");
            dataTable.Columns.Add("ConstraintId");
            dataTable.Columns.Add("Ordinal", typeof(int));

            if (connection.IsDbc && !_isView) {
                AddForeignKeys(connection, dataTable);
            }

            return dataTable;
        }

        private static void AddForeignKeys(VfpConnection connection, DataTable dataTable) {
            var foreignKeys = connection.GetSchema(VfpConnection.SchemaNames.ForeignKeys);

            foreach (DataRow dataRow in foreignKeys.Rows) {
                var newRow = dataTable.NewRow();
                newRow["Id"] = dataRow[ForeignKeySchema.ForeignKeyName];
                newRow["ConstraintId"] = dataRow[ForeignKeySchema.ForeignKeyName];
                // TODO: might need to revisit this ... I guess it is possible to have multiple keys 
                newRow["Ordinal"] = 1;

                newRow["ToColumnId"] = string.Format("{0}.{1}",
                                                        dataRow[ForeignKeySchema.PrimaryKeyTableName],
                                                        dataRow[ForeignKeySchema.PrimaryKeyFieldName]);

                newRow["FromColumnId"] = string.Format("{0}.{1}",
                    dataRow[ForeignKeySchema.ForeignKeyTableName],
                    dataRow[ForeignKeySchema.ForeignKeyFieldName]);

                dataTable.Rows.Add(newRow);
            }
        }
    }
}