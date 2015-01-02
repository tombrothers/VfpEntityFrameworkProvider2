using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VfpEntityFrameworkProvider.Tests {
    internal static class DataTableHelper {
        public static void AssertDataTablesAreEqual(DataTable expected, DataTable actual) {
            if (expected == null && actual == null) {
                return;
            }

            if (expected == null && actual != null) {
                Assert.Fail("expected is null");
            }

            if (expected != null && actual == null) {
                Assert.Fail("actual is null");
            }

            if (expected.Rows.Count > actual.Rows.Count) {
                Assert.Fail("expected has more rows than actual");
            }

            if (actual.Rows.Count > expected.Rows.Count) {
                Assert.Fail("actual has more rows than expected");
            }

            if (expected.Columns.Count > actual.Columns.Count) {
                Assert.Fail("expected has more columns than actual");
            }

            if (actual.Columns.Count > expected.Columns.Count) {
                Assert.Fail("actual has more columns than expected");
            }

            for (int index = 0, total = expected.Columns.Count; index < total; index++) {
                DataColumn expectedColumn = expected.Columns[index];
                DataColumn actualColumn = actual.Columns[index];

                if (expectedColumn.ColumnName != actualColumn.ColumnName) {
                    Assert.Fail(string.Format("Column names don't match.  [index:{0}] [expected:{1}] [actual:{2}]", index, expectedColumn.ColumnName, actualColumn.ColumnName));
                }

                if (expectedColumn.DataType.Name != actualColumn.DataType.Name) {
                    Assert.Fail(string.Format("Column types don't match.  [index:{0}] [expected:{1}] [actual:{2}]", index, expectedColumn.DataType.Name, actualColumn.DataType.Name));
                }
            }

            for (int index = 0, total = expected.Rows.Count; index < total; index++) {
                DataRow expectedRow = expected.Rows[index];
                DataRow actualRow = actual.Rows[index];

                foreach (DataColumn column in expected.Columns) {
                    if (expectedRow.IsNull(column.ColumnName) && !actualRow.IsNull(column.ColumnName)) {
                        Assert.Fail(string.Format("Column values don't match.   [column:{0}] [expected:{1}] [actual:{2}]", column.ColumnName, "(IsNull)", "(IsNotNull)"));
                    }

                    if (!expectedRow.IsNull(column.ColumnName) && actualRow.IsNull(column.ColumnName)) {
                        Assert.Fail(string.Format("Column values don't match.  [column:{0}] [expected:{1}] [actual:{2}]", column.ColumnName, "(IsNotNull)", "(IsNull)"));
                    }

                    if (expectedRow[column.ColumnName].ToString() != actualRow[column.ColumnName].ToString()) {
                        Assert.Fail(string.Format("Column values don't match.  [column:{0}] [expected:{1}] [actual:{2}]", column.ColumnName, expectedRow[column.ColumnName], actualRow[column.ColumnName]));
                    }
                }
            }
        }

        /// <summary>
        /// Used to create methods for the ExpectedSchema class
        /// </summary>
        public static void WriteDataTableCode(string schemaName, DataTable dataTable) {
            StringBuilder sb = new StringBuilder();
            sb.Append("public static DataTable Get");
            sb.Append(schemaName);
            sb.Append("() {");
            sb.Append(Environment.NewLine);
            sb.Append("DataTable dataTable = new DataTable();");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("#region columns");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            foreach (DataColumn column in dataTable.Columns) {
                sb.Append("dataTable.Columns.Add(\"");
                sb.Append(column.ColumnName);
                sb.Append("\", typeof(");
                sb.Append(column.DataType.FullName);
                sb.Append("));");
                sb.Append(Environment.NewLine);
            }

            sb.Append(Environment.NewLine);
            sb.Append("#endregion columns");
            sb.Append(Environment.NewLine);

            if (dataTable.Rows.Count > 0) {

                sb.Append(Environment.NewLine);
                sb.Append("#region rows");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("DataRow row;");
                sb.Append(Environment.NewLine);
                for (int index = 0, total = dataTable.Rows.Count; index < total; index++) {
                    DataRow row = dataTable.Rows[index];

                    sb.Append(Environment.NewLine);
                    sb.Append("#region row ");
                    sb.Append(index);
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("row = dataTable.NewRow();");
                    sb.Append(Environment.NewLine);
                    foreach (DataColumn column in dataTable.Columns) {
                        sb.Append("row[\"");
                        sb.Append(column.ColumnName);
                        sb.Append("\"] = ");

                        if (row.IsNull(column)) {
                            sb.Append("DBNull.Value");
                        }
                        else {
                            switch (column.DataType.Name) {
                                case "Object":
                                    sb.Append("@\"");
                                    sb.Append(row[column].ToString().Replace("\"", "\" + (char)34 + @\""));
                                    sb.Append("\"");
                                    break;
                                case "String":
                                    sb.Append("\"");
                                    sb.Append(row[column].ToString().Replace(@"\", @"\\").Replace("\"", @"\"""));
                                    sb.Append("\"");
                                    break;
                                case "DateTime":
                                    sb.Append("DateTime.Parse(\"");
                                    sb.Append(row[column].ToString());
                                    sb.Append("\")");
                                    break;
                                case "Guid":
                                    sb.Append("new Guid(\"");
                                    sb.Append(row[column].ToString());
                                    sb.Append("\")");
                                    break;
                                case "Boolean":
                                    sb.Append(row[column].ToString().ToLower());
                                    break;
                                default:
                                    Debug.WriteLine(column.DataType.Name);
                                    sb.Append(row[column].ToString());
                                    break;
                            }
                        }

                        sb.Append(";");
                        sb.Append(Environment.NewLine);
                    }

                    sb.Append("dataTable.Rows.Add(row);");
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("#endregion row");
                    sb.Append(Environment.NewLine);
                }

                sb.Append(Environment.NewLine);
                sb.Append("#endregion rows");
                sb.Append(Environment.NewLine);
            }

            sb.Append(Environment.NewLine);
            sb.Append("return dataTable;");
            sb.Append(Environment.NewLine);
            sb.Append("}");

            Clipboard.SetText(sb.ToString());
        }
    }
}
