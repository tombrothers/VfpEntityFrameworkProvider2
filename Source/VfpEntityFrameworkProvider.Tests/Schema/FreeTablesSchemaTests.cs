using System.Data;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Schema;

namespace VfpEntityFrameworkProvider.Tests.Schema {
    [TestClass]
    public class FreeTablesSchemaTests : TestBase {
        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_ViewForeignKeys() {
            AssertSchema(SchemaNames.ViewForeignKeys, FreeTablesSchemaExpected.GetViewForeignKeys());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_TableForeignKeys() {
            AssertSchema(SchemaNames.TableForeignKeys, FreeTablesSchemaExpected.GetTableForeignKeys());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_TableForeignKeyConstraints() {
            AssertSchema(SchemaNames.TableForeignKeyConstraints, FreeTablesSchemaExpected.GetTableForeignKeyConstraints());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_ViewConstraintColumns() {
            AssertSchema(SchemaNames.ViewConstraintColumns, FreeTablesSchemaExpected.GetViewConstraintColumns());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_TableConstraintColumns() {
            AssertSchema(SchemaNames.TableConstraintColumns, FreeTablesSchemaExpected.GetTableConstraintColumns());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_ViewConstraints() {
            AssertSchema(SchemaNames.ViewConstraints, FreeTablesSchemaExpected.GetViewConstraints());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_TableConstraints() {
            AssertSchema(SchemaNames.TableConstraints, FreeTablesSchemaExpected.GetTableConstraints());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_ProcedureParameters() {
            AssertSchema(SchemaNames.ProcedureParameters, FreeTablesSchemaExpected.GetProcedureParameters());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_Procedures() {
            AssertSchema(SchemaNames.Procedures, FreeTablesSchemaExpected.GetProcedures());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_FunctionParameters() {
            AssertSchema(SchemaNames.FunctionParameters, FreeTablesSchemaExpected.GetFunctionParameters());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_Functions() {
            AssertSchema(SchemaNames.Functions, FreeTablesSchemaExpected.GetFunctions());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_ViewColumns() {
            AssertSchema(SchemaNames.ViewColumns, FreeTablesSchemaExpected.GetViewColumns());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_Views() {
            AssertSchema(SchemaNames.Views, FreeTablesSchemaExpected.GetViews());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_TableColumns() {
            AssertSchema(SchemaNames.TableColumns, FreeTablesSchemaExpected.GetTableColumns());
        }

        [TestMethod]
        public void FreeTablesSchemaTests_SchemaHelper_Tables() {
            AssertSchema(SchemaNames.Tables, FreeTablesSchemaExpected.GetTables());
        }

        private void AssertSchema(string schemaName, DataTable expected) {
            DataTable actual = null;
            SchemaManager.Using(this.GetConnection().ConnectionString, (manager) => actual = manager.CreateDataTable(this.GetSelectStatement(schemaName)));

            //DataTableHelper.WriteDataTableCode(schemaName, actual);
            DataTableHelper.AssertDataTablesAreEqual(expected, actual);
        }

        private string GetSelectStatement(string schemaName) {
            return "select * from ({{{|VFP:EF:SCHEMAHELPER_" + schemaName + "|}}}) q";
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) {
            File.WriteAllBytes("FreeTables.zip", Properties.Resources.FreeTables);

            var zip = new FastZip();
            zip.ExtractZip("FreeTables.zip", Path.Combine(context.TestDeploymentDir, "FreeTables"), string.Empty);
        }

        protected override VfpConnection GetConnection() {
            var connectionString = Path.Combine(TestContext.TestDeploymentDir, "FreeTables");
            var connection = new VfpConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}