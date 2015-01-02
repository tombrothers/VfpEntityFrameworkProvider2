using System.Data;

namespace VfpEntityFrameworkProvider.Schema {
    internal interface ISchema {
        void CreateTempTable(VfpConnection connection, DataTableDbcCreator dbcCreator);
        string GetSelectStatement(DataTableDbcCreator dbcCreator);
        bool CanExecute(string key);
        DataTable GetSchema(VfpConnection connection);
    }
}