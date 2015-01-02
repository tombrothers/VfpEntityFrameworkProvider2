using System.Data;
using System.Linq;
using VfpClientProcedureParameterSchema = VfpClient.VfpConnection.SchemaColumnNames.ProcedureParameter;

namespace VfpEntityFrameworkProvider.Schema {
    internal partial class ProcedureParameterSchema : SchemaBase {
        internal ProcedureParameterSchema()
            : base(SchemaNames.ProcedureParameters) {
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = connection.GetSchema(Key);

            dataTable = RemoveReferentialIntegrityProcedures(connection, dataTable);
            RelabelColumns(dataTable);
            AddColumns(dataTable);
            FixDataTypes(dataTable);

            dataTable = dataTable.DefaultView.ToTable(dataTable.TableName, false, new[] { 
                Columns.Id,
                Columns.ParentId,
                Columns.Name,
                Columns.Ordinal,
                Columns.TypeName,
                Columns.MaxLength,
                Columns.Precision,
                Columns.DateTimePrecision,
                Columns.Scale,
                Columns.CollationCatalog,
                Columns.CollationSchema,
                Columns.CollationName,
                Columns.CharacterSetCatalog,
                Columns.CharacterSetSchema,
                Columns.CharacterSetName,
                Columns.IsMultiSet,
                Columns.Mode,
                Columns.Default
            });

            dataTable.TableName = Key;

            return dataTable;
        }

        private static void FixDataTypes(DataTable dataTable) {
            foreach (DataRow row in dataTable.Rows) {
                switch (row[Columns.TypeName].ToString().ToLower()) {
                    case "logical":
                    case "decimal":
                        break;
                    case "integer":
                        row[Columns.TypeName] = "int";
                        break;
                    case "date":
                        row[Columns.TypeName] = "datetime";
                        break;
                    default:
                        row[Columns.TypeName] = "varchar";
                        break;
                }
            }
        }

        private void AddColumns(DataTable dataTable) {
            dataTable.Columns.Add(Columns.Id, typeof(string), "ParentId + '|' + Name");
            dataTable.Columns.Add(Columns.MaxLength);
            dataTable.Columns.Add(Columns.Precision, typeof(int));
            dataTable.Columns.Add(Columns.DateTimePrecision, typeof(int));
            dataTable.Columns.Add(Columns.Scale, typeof(int));
            dataTable.Columns.Add(Columns.CollationCatalog);
            dataTable.Columns.Add(Columns.CollationSchema);
            dataTable.Columns.Add(Columns.CollationName);
            dataTable.Columns.Add(Columns.CharacterSetCatalog);
            dataTable.Columns.Add(Columns.CharacterSetSchema);
            dataTable.Columns.Add(Columns.CharacterSetName);
            dataTable.Columns.Add(Columns.IsMultiSet, typeof(bool), "false");
            dataTable.Columns.Add(Columns.Mode, typeof(string), "'IN'");
            dataTable.Columns.Add(Columns.Default);
        }

        private void RelabelColumns(DataTable dataTable) {
            dataTable.Columns[VfpClientProcedureParameterSchema.ProcedureName].ColumnName = Columns.ParentId;
            dataTable.Columns[VfpClientProcedureParameterSchema.ParameterName].ColumnName = Columns.Name;
            dataTable.Columns[VfpClientProcedureParameterSchema.Ordinal].ColumnName = Columns.Ordinal;
            dataTable.Columns[VfpClientProcedureParameterSchema.VfpTypeName].ColumnName = Columns.TypeName;
        }

        private DataTable RemoveReferentialIntegrityProcedures(VfpConnection connection, DataTable dataTable) {
            if (dataTable == null || dataTable.Rows.Count == 0) {
                return dataTable;
            }

            var procedureSchema = new ProcedureSchema();
            var procedures = procedureSchema.GetSchema(connection);
            var procedureNames = procedures.AsEnumerable()
                                           .Select(row => row.Field<string>(ProcedureSchema.Columns.Name))
                                           .ToList();

            var query = dataTable.AsEnumerable()
                                 .Where(row => procedureNames.Contains(row.Field<string>(VfpClientProcedureParameterSchema.ProcedureName)));

            if (query.Any()) {
                dataTable = query.CopyToDataTable();
            }
            else {
                dataTable.Clear();
            }

            return dataTable;
        }
    }
}