using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Schema;

namespace VfpEntityFrameworkProvider.Tests.Schema {
    [TestClass]
    public class SchemaManagerTests : TestBase {
        [TestMethod]
        public void CreateDataTable_SchemaHelper_ViewForeignKeys() {
            AssertSchema(SchemaNames.ViewForeignKeys, SchemaManagerExpected.GetViewForeignKeys());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_TableForeignKeys() {
            AssertSchema(SchemaNames.TableForeignKeys, SchemaManagerExpected.GetTableForeignKeys());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_TableForeignKeyConstraints() {
            AssertSchema(SchemaNames.TableForeignKeyConstraints, SchemaManagerExpected.GetTableForeignKeyConstraints());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_ViewConstraintColumns() {
            AssertSchema(SchemaNames.ViewConstraintColumns, SchemaManagerExpected.GetViewConstraintColumns());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_TableConstraintColumns() {
            AssertSchema(SchemaNames.TableConstraintColumns, SchemaManagerExpected.GetTableConstraintColumns());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_ViewConstraints() {
            AssertSchema(SchemaNames.ViewConstraints, SchemaManagerExpected.GetViewConstraints());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_TableConstraints() {
            AssertSchema(SchemaNames.TableConstraints, SchemaManagerExpected.GetTableConstraints());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_ProcedureParameters() {
            AssertSchema(SchemaNames.ProcedureParameters, SchemaManagerExpected.GetProcedureParameters());
        }
        
        [TestMethod]
        public void CreateDataTable_SchemaHelper_Procedures() {
            AssertSchema(SchemaNames.Procedures, SchemaManagerExpected.GetProcedures());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_FunctionParameters() {
            AssertSchema(SchemaNames.FunctionParameters, SchemaManagerExpected.GetFunctionParameters());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_Functions() {
            AssertSchema(SchemaNames.Functions, SchemaManagerExpected.GetFunctions());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_ViewColumns() {
            AssertSchema(SchemaNames.ViewColumns, SchemaManagerExpected.GetViewColumns());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_Views() {
            AssertSchema(SchemaNames.Views, SchemaManagerExpected.GetViews());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_TableColumns() {
            AssertSchema(SchemaNames.TableColumns, SchemaManagerExpected.GetTableColumns());
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_Tables() {
            AssertSchema(SchemaNames.Tables, SchemaManagerExpected.GetTables());
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
    }
}