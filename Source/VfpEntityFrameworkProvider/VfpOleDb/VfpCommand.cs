using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using VfpEntityFrameworkProvider.Schema;
using VfpTransaction = VfpClient.VfpTransaction;

namespace VfpEntityFrameworkProvider.VfpOleDb {
    public class VfpCommand : VfpClient.VfpCommand, ICloneable {
        internal const string SplitCommandsToken = "{{{|VFP:EF:NEWCOMMAND|}}}";
        internal const string SingleRowTempTableName = "_VFP_EF_SINGLEROWTEMPTABLE";
        internal const string SingleRowTempTableRequiredToken = "{{{|VFP:EF:" + SingleRowTempTableName + "|}}}";
        internal const string ExecuteScalarBeginDelimiter = "{{{|VFP:EF:EXECUTESCALAR|";
        internal const string ExecuteScalarEndDelimiter = "|}}}";

        protected new internal OleDbCommand OleDbCommand { get { return base.OleDbCommand; } }

        public VfpCommand(string commandText = null, VfpConnection connection = null, VfpTransaction transaction = null) {
            CommandText = commandText;

            if (connection != null) {
                Connection = connection;
            }

            if (transaction != null) {
                Transaction = transaction;
            }
        }

        protected internal VfpCommand(VfpCommand vfpCommand)
            : base(vfpCommand) {
        }

        protected internal VfpCommand(OleDbCommand oleDbCommand, VfpConnection vfpConnection)
            : base(oleDbCommand, vfpConnection) {
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) {
            /*
             * SchemaManager (and why here) explained:
             * 
             * From what I can tell, the EF designer is only able to get schema information from querying data using the connection.  This seemed to be
             * a problem because I couldn't figure a way to get all the required information from a DBC... plus, I really didn't want to require a DBC to use this
             * EF Provider.  What I figured I could do instead was hook into the query process and use a combination of techniques to get the schema information
             * and write it to a temp table.  Then rewrite the command text to include the temp table.  
             * 
             * The part of the query that needs to be rewritten comes from the DefiningQuery in StoreSchemaDefinition.ssdl.  The DefiningQuery will have a token
             * that identifies the requested schema information.
             * 
             * This was the only place that I found where I had an instance of the connection and the command...
             * 
             */
            if (SchemaManager.IsSchemaQuery(this.CommandText)) {
                DataTable dataTable = null;

                SchemaManager.Using(Connection.ConnectionString, (schemaManager) => dataTable = schemaManager.CreateDataTable(CommandText, Parameters));

                return dataTable.CreateDataReader();
            }

            var hasExecuteScalar = CommandText.Contains(ExecuteScalarBeginDelimiter);

            HandleTempTable();
            SplitMultipleCommands();
            HandleExecuteScalar();

            var reader = (VfpClient.VfpDataReader)base.ExecuteDbDataReader(behavior);

            if (hasExecuteScalar) {
                return new VfpAutoIncDataReader(reader, CommandText);
            }

            return reader;
        }

        public override int ExecuteNonQuery() {
            SplitMultipleCommands();

            return base.ExecuteNonQuery();
        }

        private void HandleTempTable() {
            if (!CommandText.Contains(SingleRowTempTableRequiredToken)) {
                return;
            }

            var commandText = CommandText;
            var tempFile = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(Connection.DataSource)), SingleRowTempTableName + ".dbf");

            if (!File.Exists(tempFile)) {
                CommandText = "CREATE TABLE '" + tempFile + "' FREE (PK i)";
                ExecuteNonQuery();
                CommandText = "INSERT INTO '" + tempFile + "' VALUE(1)";
                ExecuteNonQuery();
            }

            CommandText = commandText.Replace(SingleRowTempTableRequiredToken, "'" + tempFile + "'");
        }

        private void SplitMultipleCommands() {
            var commands = CommandText.Split(new[] { SplitCommandsToken }, StringSplitOptions.RemoveEmptyEntries);

            if (commands.Length <= 0) {
                return;
            }

            for (int index = 0, total = commands.Length - 1; index < total; index++) {
                CommandText = commands[index];
                HandleExecuteScalar();
                ExecuteNonQuery();
            }

            CommandText = commands[commands.Length - 1];
        }

        private void HandleExecuteScalar() {
            var startIndex = CommandText.IndexOf(ExecuteScalarBeginDelimiter);

            if (startIndex == -1) {
                return;
            }

            var endIndex = CommandText.IndexOf(ExecuteScalarEndDelimiter, startIndex);

            if (endIndex == -1) {
                return;
            }

            var commandText = CommandText;
            var fullText = CommandText.Substring(startIndex, (endIndex + ExecuteScalarEndDelimiter.Length) - startIndex);

            CommandText = fullText.Substring(ExecuteScalarBeginDelimiter.Length, fullText.Length - (ExecuteScalarEndDelimiter.Length + ExecuteScalarBeginDelimiter.Length));

            var value = ExecuteScalar();

            CommandText = commandText.Replace(fullText, GetFormattedValue(value));
        }

        private static string GetFormattedValue(object value) {
            if (value == null) {
                return " null ";
            }

            var stringValue = value as string;

            if (stringValue != null) {
                return "'" + stringValue.Replace("'", "' + chr(39) + '") + "'";
            }

            return value.ToString();
        }

        object ICloneable.Clone() {
            return new VfpCommand(this);
        }
    }
}