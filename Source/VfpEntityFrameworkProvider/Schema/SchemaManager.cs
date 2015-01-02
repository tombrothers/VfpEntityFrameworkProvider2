using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VfpClient;

namespace VfpEntityFrameworkProvider.Schema {
    public class SchemaManager : IDisposable {
        private const string BeginDelimiter = "{{{|VFP:EF:SCHEMAHELPER_";
        private const string EndDelimiter = "|}}}";
        private bool _disposed;
        private readonly VfpConnection _connection;
        private readonly IEnumerable<ISchema> _schemas;
        private readonly DataTableDbcCreator _dbcCreator;
        private static readonly Regex RegExForInnerJoinSubSelect = new Regex(@"(INNER JOIN)\W*\(\W*SELECT\W*\*\W*FROM\W*('.*')\W*\)\W*(\w*)", RegexOptions.Compiled);
        private static readonly Regex RegExForFromSubSelect = new Regex(@"(FROM)\W*\(\W*SELECT\W*\*\W*FROM\W*('.*')\W*\)\W*(\w*)", RegexOptions.Compiled);

        private SchemaManager(VfpConnection connection, DataTableDbcCreator dbcCreator) {
            _connection = connection;
            _schemas = GetSchemas();
            _dbcCreator = dbcCreator;
        }

        public static void Using(string connectionString, Action<SchemaManager> action) {
            var builder = new VfpConnectionStringBuilder(connectionString);

            builder.Ansi = true;

            var connection = new VfpConnection(builder.ConnectionString);

            using (var manager = new SchemaManager(connection, new DataTableDbcCreator())) {
                action(manager);
            };
        }

        public DataTable CreateDataTable(string commandText, VfpParameterCollection parameters = null) {
            Rewrite(ref commandText);
            var dataTable = new DataTable("SchemaManagerResults");

            using (var cmd = _connection.CreateCommand()) {
                cmd.CommandText = commandText;

                if (parameters != null) {
                    foreach (VfpParameter parameter in parameters) {
                        cmd.Parameters.Add(new VfpParameter(parameter.ParameterName, parameter.Value));
                    }
                }

                using (var da = new VfpDataAdapter(cmd)) {
                    try {
                        da.Fill(dataTable);

                        if (dataTable.Rows.Count > 0) {
                            dataTable = dataTable.DefaultView.ToTable(dataTable.TableName, true);
                        }
#if DEBUG
                        _dbcCreator.Add(dataTable);
#endif
                    }
                    catch (Exception ex) {
                        VfpClientTracing.Tracer.TraceError(ex);
                        throw;
                    }
                }
            }

            return dataTable;
        }

        private void Rewrite(ref string commandText) {
            while (IsSchemaQuery(commandText)) {
                var key = ExtractKey(commandText);
                var schema = GetSchema(key);

                schema.CreateTempTable(_connection, _dbcCreator);
                commandText = commandText.Replace(BeginDelimiter + key + EndDelimiter, schema.GetSelectStatement(_dbcCreator));
            }

            RewriteTempTableQueries(ref commandText);
        }

        private static void RewriteTempTableQueries(ref string commandText) {
            foreach (var regex in GetSubselectRegexItems()) {
                foreach (var match in regex.Matches(commandText).Cast<Match>().OrderByDescending(x => x.Index)) {
                    if (match.Groups.Count < 4) {
                        continue;
                    }

                    var newValue = string.Join(" ", match.Groups.Cast<Group>().Skip(1).Select(x => x.Value));

                    commandText = commandText.Replace(match.Value, newValue);
                }
            }
        }

        private static IEnumerable<Regex> GetSubselectRegexItems() {
            yield return RegExForFromSubSelect;
            yield return RegExForInnerJoinSubSelect;
        }

        private ISchema GetSchema(string key) {
            foreach (var schema in _schemas.Where(x => x.CanExecute(key))) {
                return schema;
            }

            throw new NotImplementedException("Key:  " + key);
        }

        internal static bool IsSchemaQuery(string commandText) {
            return !string.IsNullOrEmpty(commandText) && commandText.Contains(BeginDelimiter);
        }

        private static string ExtractKey(string commandText) {
            commandText = commandText.Substring(commandText.IndexOf(BeginDelimiter) + BeginDelimiter.Length);
            commandText = commandText.Substring(0, commandText.IndexOf(EndDelimiter));

            return commandText;
        }

        void IDisposable.Dispose() {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing) {
            if (_disposed || !disposing) {
                return;
            }

            _connection.Close();
            _connection.Dispose();
            ((IDisposable)_dbcCreator).Dispose();

            try {
                if (!string.IsNullOrEmpty(_dbcCreator.DbcPath) && File.Exists(_dbcCreator.DbcPath)) {
#if !DEBUG
                    Directory.Delete(Path.GetDirectoryName(_dbcCreator.DbcPath), true);
#endif
                }
            }
            catch (IOException ex) {
                VfpClientTracing.Tracer.TraceError(ex);
            }

            _disposed = true;
        }

        private static IEnumerable<ISchema> GetSchemas() {
            yield return new CheckConstraintSchema();
            yield return new FunctionParameterSchema();
            yield return new FunctionReturnTableColumnsSchema();
            yield return new FunctionSchema();
            yield return new ProcedureParameterSchema();
            yield return new ProcedureSchema();
            yield return new TableColumnSchema();
            yield return new TableConstraintColumnSchema();
            yield return new TableConstraintSchema();
            yield return new TableForeignKeyConstraintsSchema();
            yield return new TableForeignKeySchema();
            yield return new TableSchema();
            yield return new ViewColumnSchema();
            yield return new ViewConstraintColumnSchema();
            yield return new ViewConstraintSchema();
            yield return new ViewForeignKeySchema();
            yield return new ViewSchema();
        }
    }
}