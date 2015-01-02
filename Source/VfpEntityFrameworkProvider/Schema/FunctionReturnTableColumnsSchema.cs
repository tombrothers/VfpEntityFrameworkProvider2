using System.Data;

namespace VfpEntityFrameworkProvider.Schema {
    internal class FunctionReturnTableColumnsSchema : SchemaBase {
        internal FunctionReturnTableColumnsSchema()
            : base(SchemaNames.FunctionReturnTableColumns) {
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = new DataTable(Key);

            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("ParentId");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Ordinal", typeof(int));
            dataTable.Columns.Add("TypeName");
            dataTable.Columns.Add("MaxLength", typeof(int));
            dataTable.Columns.Add("Precision", typeof(int));
            dataTable.Columns.Add("DateTimePrecision", typeof(int));
            dataTable.Columns.Add("Scale", typeof(int));
            dataTable.Columns.Add("CollationCatalog");
            dataTable.Columns.Add("CollationSchema");
            dataTable.Columns.Add("CollationName");
            dataTable.Columns.Add("CharacterSetCatalog");
            dataTable.Columns.Add("CharacterSetSchema");
            dataTable.Columns.Add("CharacterSetName");
            dataTable.Columns.Add("IsMultiSet", typeof(bool));
            dataTable.Columns.Add("IsIdentity", typeof(bool));
            dataTable.Columns.Add("IsStoreGenerated", typeof(bool));
            dataTable.Columns.Add("IsNullable", typeof(bool));
            dataTable.Columns.Add("Default");

            return dataTable;
        }
    }
}