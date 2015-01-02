using System.Data;

namespace VfpEntityFrameworkProvider.Schema {
    internal class CheckConstraintSchema : SchemaBase {
        internal CheckConstraintSchema()
            : base(SchemaNames.CheckConstraints) {
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);
            //// TODO: consider getting this information later... by using AFields()

            var dataTable = new DataTable(this.Key);

            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Expression");

            return dataTable;
        }
    }
}