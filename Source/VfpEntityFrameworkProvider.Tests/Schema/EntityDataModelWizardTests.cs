using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Schema;

namespace VfpEntityFrameworkProvider.Tests.Schema {
    [TestClass]
    public class EntityDataModelWizardTests : TestBase {
        [TestMethod]
        public void CreateDataTable_SchemaHelper_Procedures() {
            #region query

            string query = @"
SELECT 
Project3.C4 C1, 
Project3.C1 C2, 
Project3.C2 C3, 
Project3.C3 C4
FROM ( SELECT 
	UnionAll1.CatalogName C1, 
	UnionAll1.SchemaName C2, 
	UnionAll1.Name C3, 
	1 C4
	FROM  (SELECT 
		Extent1.CatalogName, 
		Extent1.SchemaName, 
		Extent1.Name
		FROM (
        {{{|VFP:EF:SCHEMAHELPER_FUNCTIONS|}}}
      ) Extent1
	UNION ALL
		SELECT 
		Extent2.CatalogName, 
		Extent2.SchemaName, 
		Extent2.Name
		FROM (
        {{{|VFP:EF:SCHEMAHELPER_PROCEDURES|}}}
      ) Extent2) UnionAll1
)  Project3
ORDER BY Project3.C3, Project3.C2";

            #endregion

            DataTable expected = EntityDataModelWizardExpected.GetProcedures();
            DataTable actual = null;
            SchemaManager.Using(this.GetConnection().ConnectionString, (manager) => actual = manager.CreateDataTable(query));

            //DataTableHelper.WriteDataTableCode("Procedures", actual);
            DataTableHelper.AssertDataTablesAreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_Views() {
            #region query 

            string query = @"
SELECT 
Project1.C1, 
Project1.CatalogName, 
Project1.SchemaName, 
Project1.Name
FROM ( SELECT 
	Extent1.CatalogName, 
	Extent1.SchemaName, 
	Extent1.Name, 
	1 C1
	FROM (
        {{{|VFP:EF:SCHEMAHELPER_VIEWS|}}}
      ) Extent1
)  Project1
ORDER BY Project1.Name, Project1.SchemaName";

            #endregion

            DataTable expected = EntityDataModelWizardExpected.GetViews();
            DataTable actual = null;
            SchemaManager.Using(this.GetConnection().ConnectionString, (manager) => actual = manager.CreateDataTable(query));

            //DataTableHelper.WriteDataTableCode("Views", actual);
            DataTableHelper.AssertDataTablesAreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDataTable_SchemaHelper_Tables() {
            #region query 

            string query = @"
SELECT 
Project1.C1, 
Project1.CatalogName, 
Project1.SchemaName, 
Project1.Name
FROM ( SELECT 
	Extent1.CatalogName, 
	Extent1.SchemaName, 
	Extent1.Name, 
	1 C1
	FROM (
        {{{|VFP:EF:SCHEMAHELPER_TABLES|}}}
      ) Extent1
)  Project1
ORDER BY Project1.Name, Project1.SchemaName";

            #endregion

            DataTable expected = EntityDataModelWizardExpected.GetTables();
            DataTable actual = null;
            SchemaManager.Using(this.GetConnection().ConnectionString, (manager) => actual = manager.CreateDataTable(query));

            //DataTableHelper.WriteDataTableCode("Tables", actual);
            DataTableHelper.AssertDataTablesAreEqual(expected, actual);
        }
    }
}
