using System;
using System.Data;
using System.Linq;

namespace VfpEntityFrameworkProvider.Schema {
    internal abstract class SchemaBase : ISchema {
        internal string Key { get; private set; }
        internal string TempTableFullPath { get; private set; }

        protected SchemaBase(string key) {
            ArgumentUtility.CheckNotNullOrEmpty("key", key);

            Key = key;
        }

        public bool CanExecute(string key) {
            return Key.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public void CreateTempTable(VfpConnection connection, DataTableDbcCreator dbcCreator) {
            ArgumentUtility.CheckNotNull("connection", connection);

            DataTable dataTable = null;

            connection.DoConnected(() => {
                dataTable = this.GetSchema(connection);
            });

            dbcCreator.Add(dataTable);

            TempTableFullPath = dataTable.TableName;
        }

        public string GetSelectStatement(DataTableDbcCreator dbcCreator) {
            return string.Format("SELECT * FROM '{0}!{1}'", dbcCreator.DbcPath, TempTableFullPath);
        }

        public abstract DataTable GetSchema(VfpConnection connection);

        protected void RemoveColumnsWithUpperCaseNames(DataTable dataTable) {
            dataTable.Columns.OfType<DataColumn>()
                     .Where(d => d.ColumnName.Equals(d.ColumnName.ToUpper()))
                     .ToList()
                     .ForEach(column => dataTable.Columns.Remove(column));
        }
    }
}