using System.Data;
using ForeignKeySchema = VfpClient.VfpConnection.SchemaColumnNames.ForeignKey;

namespace VfpEntityFrameworkProvider.Schema {
    internal abstract class TableOrViewForeignKeyConstraintsBase : SchemaBase {
        private bool isView;

        protected TableOrViewForeignKeyConstraintsBase(string key, bool isView)
            : base(key) {
            ArgumentUtility.CheckNotNullOrEmpty("key", key);

            this.isView = isView;
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var foreignKeys = connection.GetSchema(VfpConnection.SchemaNames.ForeignKeys);

            foreignKeys = foreignKeys.DefaultView.ToTable(foreignKeys.TableName, true, ForeignKeySchema.ForeignKeyName);

            foreignKeys.Columns[ForeignKeySchema.ForeignKeyName].ColumnName = "Id";
            foreignKeys.Columns.Add("UpdateRule");
            foreignKeys.Columns.Add("DeleteRule");

            foreach (DataRow row in foreignKeys.Rows) {
                row["UpdateRule"] = "NO ACTION";
                row["DeleteRule"] = "NO ACTION";
            }

            return foreignKeys;
        }
    }
}