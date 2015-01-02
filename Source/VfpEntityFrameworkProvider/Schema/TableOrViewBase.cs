using System.Collections.Generic;
using System.Data;
using System.Linq;
using VfpClientTableSchema = VfpClient.VfpConnection.SchemaColumnNames.Table;
using VfpClientViewSchema = VfpClient.VfpConnection.SchemaColumnNames.View;

namespace VfpEntityFrameworkProvider.Schema {
    internal abstract class TableOrViewBase : SchemaBase {
        private readonly bool _isView;

        public TableOrViewBase(string key, bool isView)
            : base(key) {
            ArgumentUtility.CheckNotNullOrEmpty("key", key);

            _isView = isView;
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = connection.GetSchema(this.GetSchemaName());

            RelabelColumns(dataTable);

            dataTable = dataTable.DefaultView.ToTable(dataTable.TableName, false, new[] { "Name" });

            AddColumns(dataTable);

            dataTable = dataTable.DefaultView.ToTable(dataTable.TableName, false, GetColumnNames().ToArray());

            return dataTable;
        }

        private IEnumerable<string> GetColumnNames() {
            yield return "CatalogName";
            yield return "SchemaName";
            yield return "Name";
            yield return "Id";

            if (!_isView) {
                yield break;
            }

            yield return "IsUpdatable";
            yield return "ViewDefinition";
        }

        protected virtual void AddColumns(DataTable dataTable) {
            dataTable.Columns.Add("Id", typeof(string), "Name");
            dataTable.Columns.Add("SchemaName");
            dataTable.Columns.Add("CatalogName");
        }

        private void RelabelColumns(DataTable dataTable) {
            dataTable.Columns[GetColumnName()].ColumnName = "Name";
        }

        private string GetColumnName() {
            return _isView ? VfpClientViewSchema.ViewName : VfpClientTableSchema.TableName;
        }

        private string GetSchemaName() {
            return _isView ? VfpConnection.SchemaNames.Views : VfpConnection.SchemaNames.Tables;
        }
    }
}