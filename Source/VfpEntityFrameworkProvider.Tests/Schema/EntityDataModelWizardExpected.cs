using System;
using System.Data;

namespace VfpEntityFrameworkProvider.Tests.Schema {
    internal static class EntityDataModelWizardExpected {
        public static DataTable GetTables() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("C1", typeof(System.Decimal));
            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Categories";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "CustomerCustomerDemo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "CustomerDemographics";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Customers";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Employees";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "EmployeesTerritories";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Internationalorders";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "OrderDetails";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Orders";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Previousemployees";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Products";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Region";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Shippers";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Suppliers";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Territories";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetViews() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("C1", typeof(System.Decimal));
            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Category_Sales_For_1997";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Current_Product_List";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Customer_And_Suppliers_By_City";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Invoices";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Order_Details_Extended";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Order_Subtotals";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Orders_Qry";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Product_Sales_For_1997";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Products_Above_Average_Price";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Products_By_Category";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Quarterly_Orders";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Sales_By_Category";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Sales_Totals_By_Amount";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Summary_Of_Sales_By_Quarter";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Summary_Of_Sales_By_Year";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 15

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Ten_Most_Expensive_Products";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetProcedures() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("C1", typeof(System.Decimal));
            dataTable.Columns.Add("C2", typeof(System.String));
            dataTable.Columns.Add("C3", typeof(System.String));
            dataTable.Columns.Add("C4", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["C2"] = DBNull.Value;
            row["C3"] = DBNull.Value;
            row["C4"] = "Custorderhist";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["C2"] = DBNull.Value;
            row["C3"] = DBNull.Value;
            row["C4"] = "Custordersdetail";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["C2"] = DBNull.Value;
            row["C3"] = DBNull.Value;
            row["C4"] = "Custordersorders";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["C2"] = DBNull.Value;
            row["C3"] = DBNull.Value;
            row["C4"] = "Employeesalesbycountry";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["C1"] = 1;
            row["C2"] = DBNull.Value;
            row["C3"] = DBNull.Value;
            row["C4"] = "Salesbyyear";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }
    }
}
