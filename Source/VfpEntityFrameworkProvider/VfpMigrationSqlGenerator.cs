using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Migrations.Utilities;
using System.Globalization;
using System.IO;
using VfpClient;
using System.Linq;
using VfpEntityFrameworkProvider.SqlGeneration;
using System.Diagnostics;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider {
    public class VfpMigrationSqlGenerator : MigrationSqlGenerator {
        private List<MigrationStatement> _statements;
        private VfpProviderManifest _providerManifest = new VfpProviderManifest();
        private readonly TableIndexService _tableIndexService = new TableIndexService();

        public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken) {
            ArgumentUtility.CheckNotNull("migrationOperations", migrationOperations);

            migrationOperations = OverrideCreateTableOperationForMigrationHistory(migrationOperations).ToArray();

            _tableIndexService.LoadTableIndexes(migrationOperations);

            _statements = new List<MigrationStatement>();

            DetectHistoryRebuild(migrationOperations).Each<dynamic>(o => Generate(o));

#if DEBUG
            _statements.Each(x => Debug.WriteLine(x.Sql));
#endif

            return _statements;
        }

        private IEnumerable<MigrationOperation> OverrideCreateTableOperationForMigrationHistory(IEnumerable<MigrationOperation> migrationOperations) {
            foreach (var operation in migrationOperations) {
                var createTableOperation = operation as CreateTableOperation;

                if (createTableOperation != null && createTableOperation.Name == "dbo.__MigrationHistory") {
                    var primaryKeyColumn = new ColumnModel(PrimitiveTypeKind.Int32) { Name = "MigrationHistoryId", IsIdentity = true, IsNullable = false };

                    createTableOperation.Columns.Insert(0, primaryKeyColumn);
                    createTableOperation.PrimaryKey = new AddPrimaryKeyOperation { Table = createTableOperation.Name, Name = primaryKeyColumn.Name };
                    createTableOperation.PrimaryKey.Columns.Add(primaryKeyColumn.Name);
                }

                yield return operation;
            }
        }

        private static IEnumerable<MigrationOperation> DetectHistoryRebuild(IEnumerable<MigrationOperation> operations) {
            var enumerator = operations.GetEnumerator();

            while (enumerator.MoveNext()) {
                var sequence = HistoryRebuildOperationSequence.Detect(enumerator);
                yield return (sequence == null) ? enumerator.Current : sequence;
            }
        }

        public override string GenerateProcedureBody(ICollection<System.Data.Entity.Core.Common.CommandTrees.DbModificationCommandTree> commandTrees, string rowsAffectedParameter, string providerManifestToken) {
            throw new NotImplementedException();
        }

        protected virtual string Generate(bool defaultValue) {
            return defaultValue ? ".t." : ".f.";
        }

        protected virtual string Generate(DateTime defaultValue) {
            return "CTOT('" + defaultValue.ToString() + "')";
        }

        protected virtual string Generate(Guid defaultValue) {
            return "'" + defaultValue + "'";
        }

        protected virtual string Generate(string defaultValue) {
            ArgumentUtility.CheckNotNullOrEmpty("defaultValue", defaultValue);

            return "'" + defaultValue + "'";
        }

        protected virtual string Generate(TimeSpan defaultValue) {
            return "'" + defaultValue + "'";
        }

        protected virtual string Generate(object defaultValue) {
            ArgumentUtility.CheckNotNull("defaultValue", defaultValue);

            return string.Format(CultureInfo.InvariantCulture, "{0}", defaultValue);
        }

        private void Generate(AddColumnOperation addColumnOperation) {
            ArgumentUtility.CheckNotNull("addColumnOperation", addColumnOperation);

            throw new NotImplementedException("AddColumnOperation");

            //using (var writer = Writer()) {
            //    writer.Write("ALTER TABLE ");
            //    writer.Write(Name(addColumnOperation.Table));
            //    writer.Write(" ADD ");

            //    var column = addColumnOperation.Column;

            //    Generate(column, writer);

            //    if ((column.IsNullable != null)
            //        && !column.IsNullable.Value
            //        && (column.DefaultValue == null)
            //        && (string.IsNullOrWhiteSpace(column.DefaultValueSql))
            //        && !column.IsIdentity
            //        && !column.IsTimestamp
            //        && !column.StoreType.EqualsIgnoreCase("rowversion")
            //        && !column.StoreType.EqualsIgnoreCase("timestamp")) {
            //        writer.Write(" DEFAULT ");

            //        if (column.Type == PrimitiveTypeKind.DateTime) {
            //            writer.Write(Generate(DateTime.Parse("1900-01-01 00:00:00", CultureInfo.InvariantCulture)));
            //        }
            //        else {
            //            writer.Write(Generate((dynamic)column.ClrDefaultValue));
            //        }
            //    }

            //    Statement(writer);
            //}
        }

        private void Generate(AddForeignKeyOperation addForeignKeyOperation) {
            ArgumentUtility.CheckNotNull("addForeignKeyOperation", addForeignKeyOperation);

            using (var writer = Writer()) {
                writer.Write("ALTER TABLE '");
                writer.Write(Name(addForeignKeyOperation.DependentTable));
                writer.Write("' ADD FOREIGN KEY TAG ");
                writer.Write(_tableIndexService.GetIndexName(addForeignKeyOperation.DependentTable, addForeignKeyOperation.DependentColumns));
                writer.Write(" REFERENCES ");
                writer.Write(Name(addForeignKeyOperation.PrincipalTable));
                writer.Write(" TAG ");
                writer.Write(_tableIndexService.GetIndexName(addForeignKeyOperation.PrincipalTable, addForeignKeyOperation.PrincipalColumns));

                Statement(writer, true);
            }
        }

        private void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation) {
            ArgumentUtility.CheckNotNull("addPrimaryKeyOperation", addPrimaryKeyOperation);

            using (var writer = Writer()) {
                var tableIndex = _tableIndexService.GetTableIndex(addPrimaryKeyOperation.Name, addPrimaryKeyOperation.Columns);

                writer.Write("ALTER TABLE ");
                writer.Write(Name(addPrimaryKeyOperation.Table));
                writer.Write(" ADD PRIMARY KEY ");
                writer.Write(tableIndex.IndexExpression);
                writer.Write(" TAG ");
                writer.Write(tableIndex.IndexName);

                Statement(writer, true);
            }
        }

        private void Generate(AlterColumnOperation alterColumnOperation) {
            ArgumentUtility.CheckNotNull("alterColumnOperation", alterColumnOperation);

            throw new NotImplementedException("AlterColumnOperation");

            //var column = alterColumnOperation.Column;

            //if ((column.DefaultValue != null)
            //    || !string.IsNullOrWhiteSpace(column.DefaultValueSql)) {
            //    using (var writer = Writer()) {
            //        DropDefaultConstraint(alterColumnOperation.Table, column.Name, writer);

            //        writer.Write("ALTER TABLE ");
            //        writer.Write(Name(alterColumnOperation.Table));
            //        writer.Write(" ADD CONSTRAINT DF_");
            //        writer.Write(alterColumnOperation.Table);
            //        writer.Write("_");
            //        writer.Write(column.Name);
            //        writer.Write(" DEFAULT ");
            //        writer.Write(
            //            (column.DefaultValue != null)
            //                ? Generate((dynamic)column.DefaultValue)
            //                : column.DefaultValueSql
            //            );
            //        writer.Write(" FOR ");
            //        writer.Write(Quote(column.Name));

            //        Statement(writer);
            //    }
            //}

            //using (var writer = Writer()) {
            //    writer.Write("ALTER TABLE ");
            //    writer.Write(Name(alterColumnOperation.Table));
            //    writer.Write(" ALTER COLUMN ");
            //    writer.Write(Quote(column.Name));
            //    writer.Write(" ");
            //    writer.Write(BuildColumnType(column));

            //    if ((column.IsNullable != null)
            //        && !column.IsNullable.Value) {
            //        writer.Write(" NOT NULL");
            //    }

            //    Statement(writer);
            //}
        }

        private void Generate(CreateIndexOperation createIndexOperation) {
            ArgumentUtility.CheckNotNull("createIndexOperation", createIndexOperation);

            using (var writer = Writer()) {
                var tableIndex = _tableIndexService.GetTableIndex(createIndexOperation.Table, createIndexOperation.Columns);

                writer.Write(string.Format("EXECSCRIPT([USE {0} IN SELECT ('{0}') EXCLUSIVE] + CHR(13) + [INDEX ON {1} TAG {2}{3}] + CHR(13) + [USE IN {0}])",
                                           tableIndex.TableName,
                                           tableIndex.IndexExpression,
                                           tableIndex.IndexName,
                                           createIndexOperation.IsUnique ? " UNIQUE" : string.Empty));

                Statement(writer, true);
            }
        }

        private void Generate(CreateProcedureOperation createProcedureOperation) {
            ArgumentUtility.CheckNotNull("createProcedureOperation", createProcedureOperation);

            throw new NotImplementedException("CreateProcedureOperation");
        }

        private void Generate(CreateTableOperation createTableOperation) {
            ArgumentUtility.CheckNotNull("createTableOperation", createTableOperation);

            Execute(() => {
                using (var writer = Writer()) {
                    WriteCreateTable(createTableOperation, writer);

                    Statement(writer, true);
                }
            });
        }

        private void Execute(Action action) {
            try {
                action();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());

                throw;
            }
        }

        private void WriteCreateTable(CreateTableOperation createTableOperation, IndentedTextWriter writer) {
            ArgumentUtility.CheckNotNull("createTableOperation", createTableOperation);
            ArgumentUtility.CheckNotNull("writer", writer);

            writer.WriteLine("CREATE TABLE " + Name(createTableOperation.Name) + " (");
            writer.Indent++;

            createTableOperation.Columns.Each((c, i) => {
                Generate(c, writer);

                if (i < createTableOperation.Columns.Count - 1) {
                    writer.WriteLine(",");
                }
            });

            if (createTableOperation.PrimaryKey != null) {
                var tableIndex = _tableIndexService.GetTableIndex(createTableOperation.Name, createTableOperation.PrimaryKey.Columns);

                writer.WriteLine(",");
                writer.Write("PRIMARY KEY ");
                writer.Write(tableIndex.IndexExpression);
                writer.Write(" TAG ");
                writer.Write(tableIndex.IndexName);
            }
            else {
                writer.WriteLine();
            }

            writer.Indent--;
            writer.Write(")");
        }

        private void Generate(ColumnModel column, IndentedTextWriter writer) {
            ArgumentUtility.CheckNotNull("column", column);
            ArgumentUtility.CheckNotNull("writer", writer);

            writer.Write(Quote(column.Name));
            writer.Write(" ");
            writer.Write(BuildColumnType(column));

            if ((column.IsNullable != null)
                && !column.IsNullable.Value) {
                writer.Write(" NOT NULL");
            }

            if (column.DefaultValue != null) {
                writer.Write(" DEFAULT ");
                writer.Write(Generate((dynamic)column.DefaultValue));
            }
            else if (!string.IsNullOrWhiteSpace(column.DefaultValueSql)) {
                writer.Write(" DEFAULT ");
                writer.Write(column.DefaultValueSql);
            }
            else if (column.IsIdentity) {
                writer.Write(" AUTOINC");
            }
        }

        private string BuildColumnType(ColumnModel columnModel) {
            ArgumentUtility.CheckNotNull("columnModel", columnModel);

            return BuildPropertyType(columnModel);
        }

        private string BuildPropertyType(PropertyModel propertyModel) {
            ArgumentUtility.CheckNotNull("propertyModel", propertyModel);

            return SqlVisitor.GetSqlPrimitiveType(propertyModel.TypeUsage);
        }

        private void Generate(DropColumnOperation dropColumnOperation) {
            ArgumentUtility.CheckNotNull("dropColumnOperation", dropColumnOperation);

            throw new NotImplementedException("DropColumnOperation");

            //using (var writer = Writer()) {
            //    DropDefaultConstraint(dropColumnOperation.Table, dropColumnOperation.Name, writer);

            //    writer.Write("ALTER TABLE ");
            //    writer.Write(Name(dropColumnOperation.Table));
            //    writer.Write(" DROP COLUMN ");
            //    writer.Write(Quote(dropColumnOperation.Name));

            //    Statement(writer);
            //}
        }

        private void Generate(DropForeignKeyOperation dropForeignKeyOperation) {
            ArgumentUtility.CheckNotNull("dropForeignKeyOperation", dropForeignKeyOperation);

            throw new NotImplementedException("DropForeignKeyOperation");
            //using (var writer = Writer()) {
            //    writer.Write("ALTER TABLE ");
            //    writer.Write(Name(dropForeignKeyOperation.DependentTable));
            //    writer.Write(" DROP CONSTRAINT ");
            //    writer.Write(Quote(dropForeignKeyOperation.Name));

            //    Statement(writer);
            //}
        }

        private void Generate(DropIndexOperation dropIndexOperation) {
            ArgumentUtility.CheckNotNull("dropIndexOperation", dropIndexOperation);

            throw new NotImplementedException("DropIndexOperation");

            //using (var writer = Writer()) {
            //    writer.Write("DROP INDEX ");
            //    writer.Write(Quote(dropIndexOperation.Name));
            //    writer.Write(" ON ");
            //    writer.Write(Name(dropIndexOperation.Table));

            //    Statement(writer);
            //}
        }

        private void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation) {
            ArgumentUtility.CheckNotNull("dropPrimaryKeyOperation", dropPrimaryKeyOperation);

            throw new NotImplementedException("DropPrimaryKeyOperation");

            //using (var writer = Writer()) {
            //    writer.Write("ALTER TABLE ");
            //    writer.Write(Name(dropPrimaryKeyOperation.Table));
            //    writer.Write(" DROP CONSTRAINT ");
            //    writer.Write(Quote(dropPrimaryKeyOperation.Name));

            //    Statement(writer);
            //}
        }

        protected virtual void Generate(DropProcedureOperation dropProcedureOperation) {
            ArgumentUtility.CheckNotNull("dropProcedureOperation", dropProcedureOperation);

            throw new NotImplementedException("DropProcedureOperation");
        }

        private void Generate(DropTableOperation dropTableOperation) {
            ArgumentUtility.CheckNotNull("dropTableOperation", dropTableOperation);

            using (var writer = Writer()) {
                writer.Write("DROP TABLE ");
                writer.Write(Name(dropTableOperation.Name));

                Statement(writer);
            }
        }

        private void Generate(HistoryOperation historyOperation) {
            ArgumentUtility.CheckNotNull("historyOperation", historyOperation);

            //historyOperation.Commands.Each(c => Statement(((VfpCommand)c).ToVfpCode()));

            using (var writer = Writer()) {
                historyOperation.CommandTrees.Each(x => Statement((GetCommandText((VfpCommand)VfpProviderServices.CreateCommand(_providerManifest, x)))));
            }
        }

        private static string GetCommandText(VfpCommand command) {
            var code = command.ToVfpCode()
                              .Replace("\n", string.Empty)
                              .Replace(";\r", " ")
                              .Replace(";", " ");

            var lines = code.Split('\r')
                            .Select(x => x.Trim())
                            .Where(x => !string.IsNullOrEmpty(x));

            return "EXECSCRIPT([" + string.Join(" ] + CHR(13) + [ ", lines) + "])";
        }

        private void Generate(MoveTableOperation moveTableOperation) {
            ArgumentUtility.CheckNotNull("moveTableOperation", moveTableOperation);

            throw new NotImplementedException("MoveTableOperation");
        }

        private void Generate(RenameColumnOperation renameColumnOperation) {
            ArgumentUtility.CheckNotNull("renameColumnOperation", renameColumnOperation);

            using (var writer = Writer()) {
                writer.Write("ALTER TABLE '");
                writer.Write(renameColumnOperation.Table);
                writer.Write("' RENAME COLUMN ");
                writer.Write(renameColumnOperation.Name);
                writer.Write(" TO ");
                writer.Write(renameColumnOperation.NewName);

                Statement(writer, true);
            }
        }

        private void Generate(RenameTableOperation renameTableOperation) {
            ArgumentUtility.CheckNotNull("renameTableOperation", renameTableOperation);

            using (var writer = Writer()) {
                WriteRenameTable(renameTableOperation, writer);

                Statement(writer, true);
            }
        }

        private static void WriteRenameTable(RenameTableOperation renameTableOperation, IndentedTextWriter writer) {
            writer.Write("RENAME TABLE '");
            writer.Write(renameTableOperation.Name);
            writer.Write("' TO '");
            writer.Write(renameTableOperation.NewName);
            writer.Write("'");
        }

        private void Generate(SqlOperation sqlOperation) {
            ArgumentUtility.CheckNotNull("sqlOperation", sqlOperation);

            Statement(sqlOperation.Sql, sqlOperation.SuppressTransaction);
        }

        private void Statement(IndentedTextWriter writer, bool suppressTransaction = true, string batchTerminator = null) {
            ArgumentUtility.CheckNotNull("writer", writer);

            Statement(writer.InnerWriter.ToString(), suppressTransaction, batchTerminator);
        }

        private void Statement(string sql, bool suppressTransaction = true, string batchTerminator = null) {
            ArgumentUtility.CheckNotNullOrEmpty("sql", sql);

            _statements.Add(
                new MigrationStatement {
                    Sql = sql,
                    SuppressTransaction = suppressTransaction,
                    BatchTerminator = batchTerminator
                });
        }

        private static string Quote(string identifier) {
            ArgumentUtility.CheckNotNullOrEmpty("identifier", identifier);

            return identifier;
        }

        private static string Name(string name) {
            ArgumentUtility.CheckNotNullOrEmpty("name", name);

            if (name.StartsWith("dbo.")) {
                name = name.Substring(4);
            }

            return name;
        }

        private static IndentedTextWriter Writer() {
            return new IndentedTextWriter(new StringWriter(CultureInfo.InvariantCulture));
        }

        private class HistoryRebuildOperationSequence : MigrationOperation {
            public readonly AddColumnOperation AddColumnOperation;
            public readonly DropPrimaryKeyOperation DropPrimaryKeyOperation;

            private HistoryRebuildOperationSequence(
                AddColumnOperation addColumnOperation,
                DropPrimaryKeyOperation dropPrimaryKeyOperation)
                : base(null) {
                AddColumnOperation = addColumnOperation;
                DropPrimaryKeyOperation = dropPrimaryKeyOperation;
            }

            public override bool IsDestructiveChange {
                get { return false; }
            }

            public static HistoryRebuildOperationSequence Detect(IEnumerator<MigrationOperation> enumerator) {
                const string HistoryTableName = HistoryContext.DefaultTableName;

                var addColumnOperation = enumerator.Current as AddColumnOperation;
                if (addColumnOperation == null
                    || addColumnOperation.Table != HistoryTableName
                    || addColumnOperation.Column.Name != "ContextKey") {
                    return null;
                }

                enumerator.MoveNext();
                var dropPrimaryKeyOperation = (DropPrimaryKeyOperation)enumerator.Current;

                enumerator.MoveNext();
                var addPrimaryKeyOperation = (AddPrimaryKeyOperation)enumerator.Current;

                return new HistoryRebuildOperationSequence(addColumnOperation, dropPrimaryKeyOperation);
            }
        }
    }
}