using System.Data;

namespace VfpEntityFrameworkProvider.Schema {
    internal class ViewSchema : TableOrViewBase {
        internal ViewSchema()
            : base(SchemaNames.Views, true) {
        }

        protected override void AddColumns(DataTable dataTable) {
            base.AddColumns(dataTable);

            dataTable.Columns.Add("IsUpdatable", typeof(bool), "true");
            dataTable.Columns.Add("ViewDefinition");
        }
    }
}