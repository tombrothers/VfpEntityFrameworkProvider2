using System.Collections.Generic;
using System.Data;
using System.Linq;
using IndexSchema = VfpClient.VfpConnection.SchemaColumnNames.Index;
using TableFieldSchema = VfpClient.VfpConnection.SchemaColumnNames.TableField;
using VfpClientSchemaNames = VfpClient.VfpConnection.SchemaNames;
using ViewFieldSchema = VfpClient.VfpConnection.SchemaColumnNames.ViewField;

namespace VfpEntityFrameworkProvider.Schema {
    internal abstract class TableOrViewColumnBase : SchemaBase {
        private readonly bool _isView;

        protected TableOrViewColumnBase(string key, bool isView)
            : base(key) {
            ArgumentUtility.CheckNotNull("key", key);

            _isView = isView;
        }

        public override DataTable GetSchema(VfpConnection connection) {
            ArgumentUtility.CheckNotNull("connection", connection);

            var dataTable = connection.GetSchema((_isView ? VfpClientSchemaNames.ViewFields : VfpClientSchemaNames.TableFields));

            RelabelColumns(dataTable);
            AddColumns(dataTable);
            SetValues(dataTable);
            RemoveColumnsWithUpperCaseNames(dataTable);
            SetIdentity(connection, dataTable);

            dataTable = dataTable.DefaultView.ToTable(dataTable.TableName, false, new[] { 
                "ParentId",
                "Name",
                "Ordinal",
                "IsNullable",
                "MaxLength",
                "Precision",
                "Scale",
                "DateTimePrecision",
                "CharacterSetCatalog",
                "CharacterSetSchema",
                "CharacterSetName",
                "CollationCatalog",
                "CollationSchema",
                "CollationName",
                "Id",
                "IsMultiset",
                "IsStoreGenerated",
                "IsIdentity",
                "Default",
                "TypeName"
            });

            return dataTable;
        }

        private static void SetIdentity(VfpConnection connection, DataTable dataTable) {
            var indexes = connection.GetSchema(VfpConnection.SchemaNames.Indexes);

            if (indexes.Rows.Count == 0) {
                return;
            }

            var primaryKeylist = (from columnRow in dataTable.AsEnumerable()
                                  from indexRow in indexes.AsEnumerable()
                                  where columnRow.Field<string>("ParentId") == indexRow.Field<string>(IndexSchema.TableName)
                                     && columnRow.Field<string>("Name") == indexRow.Field<string>(IndexSchema.FieldName)
                                     && indexRow.Field<bool>(IndexSchema.PrimaryKey)
                                  select new {
                                      ColumnRow = columnRow,
                                      PrimaryKey = indexRow.Field<bool>(IndexSchema.PrimaryKey),
                                      AutoIncrement = indexRow.Field<bool>(IndexSchema.AutoInc)
                                  }).ToList();

            var tableNames = new HashSet<string>();

            foreach (var item in primaryKeylist) {
                tableNames.Add(item.ColumnRow.Field<string>("ParentId"));

                item.ColumnRow["IsIdentity"] = item.AutoIncrement;
                item.ColumnRow["IsStoreGenerated"] = item.AutoIncrement;

                if (item.PrimaryKey) {
                    item.ColumnRow["IsNullable"] = false;
                }
            }

            var candidateKeyList = (from columnRow in dataTable.AsEnumerable()
                                    from indexRow in indexes.AsEnumerable()
                                    where columnRow.Field<string>("ParentId") == indexRow.Field<string>(IndexSchema.TableName)
                                       && columnRow.Field<string>("Name") == indexRow.Field<string>(IndexSchema.FieldName)
                                       && indexRow.Field<bool>(IndexSchema.Candidate)
                                       && !tableNames.Contains(columnRow.Field<string>("ParentId"))
                                    select columnRow).ToList();

            if (candidateKeyList.Any()) {
                // filter out tables that have multiple candidate key columns
                var duplicates = candidateKeyList.Select(x => x.Field<string>("ParentId"))
                                                 .GroupBy(x => x)
                                                 .Where(x => x.Count() > 1)
                                                 .Select(x => x.Key)
                                                 .Distinct()
                                                 .ToList();

                candidateKeyList = candidateKeyList.Where(x => !duplicates.Contains(x.Field<string>("ParentId"))).ToList();

                foreach (var item in candidateKeyList) {
                    item["IsIdentity"] = true;
                    item["IsNullable"] = false;
                }
            }
        }

        private static void SetValues(DataTable dataTable) {
            dataTable.Columns["TypeName"].ReadOnly = false;
            foreach (DataRow dataRow in dataTable.Rows) {
                if (dataRow.IsNull("MaxLength")) {
                    dataRow["MaxLength"] = -1;
                }

                if (dataRow.IsNull("Scale")) {
                    dataRow["Scale"] = 0;
                }

                dataRow["IsIdentity"] = false;
                dataRow["IsStoreGenerated"] = false;
                dataRow["TypeName"] = dataRow["TypeName"].ToString().ToLower();
            }
            dataTable.Columns["TypeName"].ReadOnly = true;
        }

        private static void AddColumns(DataTable dataTable) {
            dataTable.Columns.Add("Id", typeof(string), "ParentId + '.' + Name");
            dataTable.Columns.Add("IsMultiSet", typeof(bool), "False");
            dataTable.Columns.Add("IsIdentity", typeof(bool));
            dataTable.Columns.Add("IsStoreGenerated", typeof(bool));
            dataTable.Columns.Add("CollationCatalog");
            dataTable.Columns.Add("CollationSchema");
            dataTable.Columns.Add("CollationName");
            dataTable.Columns.Add("CharacterSetCatalog");
            dataTable.Columns.Add("CharacterSetSchema");
            dataTable.Columns.Add("CharacterSetName");
            dataTable.Columns.Add("DateTimePrecision");
            dataTable.Columns.Add("Precision", typeof(int), "MaxLength");
        }

        private void RelabelColumns(DataTable dataTable) {
            dataTable.Columns[_isView ? ViewFieldSchema.ViewName : TableFieldSchema.TableName].ColumnName = "ParentId";
            dataTable.Columns[TableFieldSchema.FieldName].ColumnName = "Name";
            dataTable.Columns[TableFieldSchema.Ordinal].ColumnName = "Ordinal";
            dataTable.Columns[TableFieldSchema.Nullable].ColumnName = "IsNullable";
            dataTable.Columns[TableFieldSchema.Width].ColumnName = "MaxLength";
            dataTable.Columns[TableFieldSchema.Decimal].ColumnName = "Scale";
            dataTable.Columns[TableFieldSchema.DefaultValue].ColumnName = "Default";
            dataTable.Columns[TableFieldSchema.VfpTypeName].ColumnName = "TypeName";

            dataTable.Columns["MaxLength"].ReadOnly = false;
            dataTable.Columns["Scale"].ReadOnly = false;
            dataTable.Columns["IsNullable"].ReadOnly = false;
        }
    }
}