using System.Data;

namespace VfpEntityFrameworkProvider.Schema {
    internal partial class ViewConstraintSchema : SchemaBase {
        internal ViewConstraintSchema()
            : base(SchemaNames.ViewConstraints) {
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = new DataTable(Key);
            dataTable.Columns.Add(Columns.Id);
            dataTable.Columns.Add(Columns.ParentId);
            dataTable.Columns.Add(Columns.Name);
            dataTable.Columns.Add(Columns.ConstraintType);
            dataTable.Columns.Add(Columns.IsDeferrable, typeof(bool));
            dataTable.Columns.Add(Columns.IsInitiallyDeferred, typeof(bool));
            dataTable.Columns.Add(Columns.Expression);
            dataTable.Columns.Add(Columns.UpdateRule);
            dataTable.Columns.Add(Columns.DeleteRule);

            return dataTable;
        }
    }
}