using System.Data;

namespace VfpEntityFrameworkProvider.Schema {
    internal class FunctionSchema : SchemaBase {
        internal FunctionSchema()
            : base(SchemaNames.Functions) {
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = new DataTable(this.Key);

            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("CatalogName");
            dataTable.Columns.Add("SchemaName");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("ReturnTypeName");
            dataTable.Columns.Add("ReturnMaxLength");
            dataTable.Columns.Add("ReturnPrecision", typeof(int));
            dataTable.Columns.Add("ReturnDateTimePrecision", typeof(int));
            dataTable.Columns.Add("ReturnScale", typeof(int));
            dataTable.Columns.Add("ReturnCollationCatalog");
            dataTable.Columns.Add("ReturnCollationSchema");
            dataTable.Columns.Add("ReturnCollationName");
            dataTable.Columns.Add("ReturnCharacterSetCatalog");
            dataTable.Columns.Add("ReturnCharacterSetSchema");
            dataTable.Columns.Add("ReturnCharacterSetName");
            dataTable.Columns.Add("ReturnIsMultiSet", typeof(bool));
            dataTable.Columns.Add("IsAggregate", typeof(bool));
            dataTable.Columns.Add("IsBuiltIn", typeof(bool));
            dataTable.Columns.Add("IsNiladic", typeof(bool));
            dataTable.Columns.Add("IsTvf", typeof(bool));

            return dataTable;
        }
    }
}