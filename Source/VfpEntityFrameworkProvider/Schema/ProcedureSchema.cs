using System.Data;
using System.Linq;

namespace VfpEntityFrameworkProvider.Schema {
    internal partial class ProcedureSchema : SchemaBase {
        internal ProcedureSchema()
            : base(SchemaNames.Procedures) {
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = connection.GetSchema(Key);

            if (dataTable.Rows.Count > 0) {
                // remove referential integrity procedures
                var rows = dataTable.AsEnumerable()
                                    .Where(row => !row.Field<bool>(VfpConnection.SchemaColumnNames.Procedure.ReferentialIntegrity))
                                    .ToArray();

                if (rows.Any()) {
                    dataTable = rows.CopyToDataTable();
                }
                else {
                    dataTable.Clear();
                }
            }

            dataTable.Columns[VfpConnection.SchemaColumnNames.Procedure.ProcedureName].ColumnName = Columns.Id;
            dataTable.Columns.Add(Columns.Name, typeof(string), Columns.Id);
            dataTable.Columns.Add(Columns.CatalogName);
            dataTable.Columns.Add(Columns.SchemaName);

            RemoveColumnsWithUpperCaseNames(dataTable);

            dataTable = dataTable.DefaultView.ToTable(dataTable.TableName, false, new[] { "Id", "Name", "CatalogName", "SchemaName" });
            dataTable.TableName = Key;

            return dataTable;
        }
    }
}