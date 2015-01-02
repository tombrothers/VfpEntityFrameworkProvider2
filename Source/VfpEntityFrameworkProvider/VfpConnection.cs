using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Transactions;
using VfpEntityFrameworkProvider.VfpOleDb;
using IsolationLevel = System.Data.IsolationLevel;
using VfpException = VfpClient.VfpException;

namespace VfpEntityFrameworkProvider {
    public class VfpConnection : VfpClient.VfpConnection {
        public VfpConnection() {
        }

        public VfpConnection(string connectionString)
            : base(connectionString) {
        }

        protected override DbProviderFactory DbProviderFactory {
            get {
                return VfpProviderFactory.Instance;
            }
        }

        //private Transaction _transaction;

        //public override void EnlistTransaction(Transaction transaction) {
        //    if (State != ConnectionState.Open) {
        //        Open();
        //    }

        //    if (_transaction != null) {
        //        throw new VfpException("Cannot nest EnlistTransaction");
        //    }

        //    _transaction = transaction;

        //    _transaction.TransactionCompleted += _transaction_TransactionCompleted;
        //}

        //private void _transaction_TransactionCompleted(object sender, TransactionEventArgs e) {

        //}

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) {
            if (isolationLevel > IsolationLevel.ReadUncommitted) {
                isolationLevel = IsolationLevel.ReadUncommitted;
            }

            return base.BeginDbTransaction(isolationLevel);
        }

        public new VfpCommand CreateCommand() {
            return (VfpCommand)CreateDbCommand();
        }

        protected override DbCommand CreateDbCommand() {
            return new VfpCommand(OleDbConnection.CreateCommand(), this);
        }

        public override DataTable GetSchema(string collectionName) {
            return FixViewSchema(collectionName, base.GetSchema(collectionName));
        }

        public override DataTable GetSchema(string collectionName, string[] restrictionValues) {
            return FixViewSchema(collectionName, base.GetSchema(collectionName, restrictionValues));
        }

        private static DataTable FixViewSchema(string collectionName, DataTable schema) {
            if (!SchemaNames.Views.Equals(collectionName, StringComparison.InvariantCultureIgnoreCase) || schema.Rows.Count == 0) {
                return schema;
            }

            var rows = schema.AsEnumerable().Where(x => !x.Field<string>(SchemaColumnNames.View.Sql).Contains("?")).ToArray();

            if (!rows.Any()) {
                schema.Clear();

                return schema;
            }

            var newSchema = rows.CopyToDataTable();

            newSchema.TableName = schema.TableName;

            return newSchema;
        }
    }
}