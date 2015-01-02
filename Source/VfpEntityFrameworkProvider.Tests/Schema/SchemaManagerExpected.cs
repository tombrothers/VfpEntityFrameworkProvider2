using System;
using System.Data;

namespace VfpEntityFrameworkProvider.Tests.Schema {
    internal static class SchemaManagerExpected {
        public static DataTable GetViewForeignKeys() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Tocolumnid", typeof(System.String));
            dataTable.Columns.Add("Fromcolumnid", typeof(System.String));
            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableForeignKeys() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Tocolumnid", typeof(System.String));
            dataTable.Columns.Add("Fromcolumnid", typeof(System.String));
            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Id"] = "FK___Employees_reportsto___Employees_employeeid";
            row["Tocolumnid"] = "Employees.Employeeid";
            row["Fromcolumnid"] = "Employees.Reportsto";
            row["Constraintid"] = "FK___Employees_reportsto___Employees_employeeid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Id"] = "FK___EmployeesTerritories_employeeid___Employees_employeeid";
            row["Tocolumnid"] = "Employees.Employeeid";
            row["Fromcolumnid"] = "EmployeesTerritories.Employeeid";
            row["Constraintid"] = "FK___EmployeesTerritories_employeeid___Employees_employeeid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Id"] = "FK___OrderDetails_productid___Products_productid";
            row["Tocolumnid"] = "Products.Productid";
            row["Fromcolumnid"] = "OrderDetails.Productid";
            row["Constraintid"] = "FK___OrderDetails_productid___Products_productid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Id"] = "FK___OrderDetails_orderid___Orders_orderid";
            row["Tocolumnid"] = "Orders.Orderid";
            row["Fromcolumnid"] = "OrderDetails.Orderid";
            row["Constraintid"] = "FK___OrderDetails_orderid___Orders_orderid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Id"] = "FK___Orders_employeeid___Employees_employeeid";
            row["Tocolumnid"] = "Employees.Employeeid";
            row["Fromcolumnid"] = "Orders.Employeeid";
            row["Constraintid"] = "FK___Orders_employeeid___Employees_employeeid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Id"] = "FK___Orders_shipvia___Shippers_shipperid";
            row["Tocolumnid"] = "Shippers.Shipperid";
            row["Fromcolumnid"] = "Orders.Shipvia";
            row["Constraintid"] = "FK___Orders_shipvia___Shippers_shipperid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Id"] = "FK___Products_supplierid___Suppliers_supplierid";
            row["Tocolumnid"] = "Suppliers.Supplierid";
            row["Fromcolumnid"] = "Products.Supplierid";
            row["Constraintid"] = "FK___Products_supplierid___Suppliers_supplierid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Id"] = "FK___Products_categoryid___Categories_categoryid";
            row["Tocolumnid"] = "Categories.Categoryid";
            row["Fromcolumnid"] = "Products.Categoryid";
            row["Constraintid"] = "FK___Products_categoryid___Categories_categoryid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Id"] = "FK___Territories_regionid___Region_regionid";
            row["Tocolumnid"] = "Region.Regionid";
            row["Fromcolumnid"] = "Territories.Regionid";
            row["Constraintid"] = "FK___Territories_regionid___Region_regionid";
            row["Ordinal"] = 1;
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetTableForeignKeyConstraints() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Updaterule", typeof(System.String));
            dataTable.Columns.Add("Deleterule", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Id"] = "FK___Employees_reportsto___Employees_employeeid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Id"] = "FK___EmployeesTerritories_employeeid___Employees_employeeid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Id"] = "FK___OrderDetails_productid___Products_productid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Id"] = "FK___OrderDetails_orderid___Orders_orderid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Id"] = "FK___Orders_employeeid___Employees_employeeid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Id"] = "FK___Orders_shipvia___Shippers_shipperid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Id"] = "FK___Products_supplierid___Suppliers_supplierid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Id"] = "FK___Products_categoryid___Categories_categoryid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Id"] = "FK___Territories_regionid___Region_regionid";
            row["Updaterule"] = "NO ACTION";
            row["Deleterule"] = "NO ACTION";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetViewConstraintColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Columnid", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableConstraintColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Columnid", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Constraintid"] = "Categoryid";
            row["Columnid"] = "Categories.Categoryid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Constraintid"] = "Pk";
            row["Columnid"] = "CustomerCustomerDemo.Customerid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Constraintid"] = "Pk";
            row["Columnid"] = "CustomerCustomerDemo.Customertypeid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Constraintid"] = "Custtypeid";
            row["Columnid"] = "CustomerDemographics.Customertypeid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Constraintid"] = "Customerid";
            row["Columnid"] = "Customers.Customerid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Constraintid"] = "Employeeid";
            row["Columnid"] = "Employees.Employeeid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Constraintid"] = "Pk";
            row["Columnid"] = "EmployeesTerritories.Employeeid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Constraintid"] = "Pk";
            row["Columnid"] = "EmployeesTerritories.Territoryid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Constraintid"] = "Orderid";
            row["Columnid"] = "Internationalorders.Orderid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["Constraintid"] = "Primarykey";
            row["Columnid"] = "OrderDetails.Orderid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["Constraintid"] = "Primarykey";
            row["Columnid"] = "OrderDetails.Productid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["Constraintid"] = "Orderid";
            row["Columnid"] = "Orders.Orderid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["Constraintid"] = "Empid";
            row["Columnid"] = "Previousemployees.Employeeid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["Constraintid"] = "Productid";
            row["Columnid"] = "Products.Productid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["Constraintid"] = "Regionid";
            row["Columnid"] = "Region.Regionid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 15

            row = dataTable.NewRow();
            row["Constraintid"] = "Shipperid";
            row["Columnid"] = "Shippers.Shipperid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 16

            row = dataTable.NewRow();
            row["Constraintid"] = "Supplierid";
            row["Columnid"] = "Suppliers.Supplierid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 17

            row = dataTable.NewRow();
            row["Constraintid"] = "Territoryi";
            row["Columnid"] = "Territories.Territoryid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 18

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___Employees_reportsto___Employees_employeeid";
            row["Columnid"] = "Employees.Reportsto";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 19

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___EmployeesTerritories_employeeid___Employees_employeeid";
            row["Columnid"] = "EmployeesTerritories.Employeeid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 20

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___OrderDetails_productid___Products_productid";
            row["Columnid"] = "OrderDetails.Productid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 21

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___OrderDetails_orderid___Orders_orderid";
            row["Columnid"] = "OrderDetails.Orderid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 22

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___Orders_employeeid___Employees_employeeid";
            row["Columnid"] = "Orders.Employeeid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 23

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___Orders_shipvia___Shippers_shipperid";
            row["Columnid"] = "Orders.Shipvia";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 24

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___Products_supplierid___Suppliers_supplierid";
            row["Columnid"] = "Products.Supplierid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 25

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___Products_categoryid___Categories_categoryid";
            row["Columnid"] = "Products.Categoryid";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 26

            row = dataTable.NewRow();
            row["Constraintid"] = "FK___Territories_regionid___Region_regionid";
            row["Columnid"] = "Territories.Regionid";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetViewConstraints() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Constrainttype", typeof(System.String));
            dataTable.Columns.Add("Isdeferrable", typeof(System.Boolean));
            dataTable.Columns.Add("Isinitiallydeferred", typeof(System.Boolean));
            dataTable.Columns.Add("Expression", typeof(System.String));
            dataTable.Columns.Add("Updaterule", typeof(System.String));
            dataTable.Columns.Add("Deleterule", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableConstraints() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Constrainttype", typeof(System.String));
            dataTable.Columns.Add("Isdeferrable", typeof(System.Boolean));
            dataTable.Columns.Add("Isinitiallydeferred", typeof(System.Boolean));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Id"] = "Categoryid";
            row["Parentid"] = "Categories";
            row["Name"] = "Categoryid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Id"] = "Pk";
            row["Parentid"] = "CustomerCustomerDemo";
            row["Name"] = "Pk";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Id"] = "Custtypeid";
            row["Parentid"] = "CustomerDemographics";
            row["Name"] = "Custtypeid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Id"] = "Customerid";
            row["Parentid"] = "Customers";
            row["Name"] = "Customerid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Id"] = "Employeeid";
            row["Parentid"] = "Employees";
            row["Name"] = "Employeeid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Id"] = "Pk";
            row["Parentid"] = "EmployeesTerritories";
            row["Name"] = "Pk";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Id"] = "Orderid";
            row["Parentid"] = "Internationalorders";
            row["Name"] = "Orderid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Id"] = "Primarykey";
            row["Parentid"] = "OrderDetails";
            row["Name"] = "Primarykey";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Id"] = "Orderid";
            row["Parentid"] = "Orders";
            row["Name"] = "Orderid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["Id"] = "Empid";
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Empid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["Id"] = "Productid";
            row["Parentid"] = "Products";
            row["Name"] = "Productid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["Id"] = "Regionid";
            row["Parentid"] = "Region";
            row["Name"] = "Regionid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["Id"] = "Shipperid";
            row["Parentid"] = "Shippers";
            row["Name"] = "Shipperid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["Id"] = "Supplierid";
            row["Parentid"] = "Suppliers";
            row["Name"] = "Supplierid";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["Id"] = "Territoryi";
            row["Parentid"] = "Territories";
            row["Name"] = "Territoryi";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 15

            row = dataTable.NewRow();
            row["Id"] = "FK___Employees_reportsto___Employees_employeeid";
            row["Parentid"] = "Employees";
            row["Name"] = "FK___Employees_reportsto___Employees_employeeid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 16

            row = dataTable.NewRow();
            row["Id"] = "FK___EmployeesTerritories_employeeid___Employees_employeeid";
            row["Parentid"] = "EmployeesTerritories";
            row["Name"] = "FK___EmployeesTerritories_employeeid___Employees_employeeid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 17

            row = dataTable.NewRow();
            row["Id"] = "FK___OrderDetails_productid___Products_productid";
            row["Parentid"] = "OrderDetails";
            row["Name"] = "FK___OrderDetails_productid___Products_productid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 18

            row = dataTable.NewRow();
            row["Id"] = "FK___OrderDetails_orderid___Orders_orderid";
            row["Parentid"] = "OrderDetails";
            row["Name"] = "FK___OrderDetails_orderid___Orders_orderid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 19

            row = dataTable.NewRow();
            row["Id"] = "FK___Orders_employeeid___Employees_employeeid";
            row["Parentid"] = "Orders";
            row["Name"] = "FK___Orders_employeeid___Employees_employeeid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 20

            row = dataTable.NewRow();
            row["Id"] = "FK___Orders_shipvia___Shippers_shipperid";
            row["Parentid"] = "Orders";
            row["Name"] = "FK___Orders_shipvia___Shippers_shipperid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 21

            row = dataTable.NewRow();
            row["Id"] = "FK___Products_supplierid___Suppliers_supplierid";
            row["Parentid"] = "Products";
            row["Name"] = "FK___Products_supplierid___Suppliers_supplierid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 22

            row = dataTable.NewRow();
            row["Id"] = "FK___Products_categoryid___Categories_categoryid";
            row["Parentid"] = "Products";
            row["Name"] = "FK___Products_categoryid___Categories_categoryid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 23

            row = dataTable.NewRow();
            row["Id"] = "FK___Territories_regionid___Region_regionid";
            row["Parentid"] = "Territories";
            row["Name"] = "FK___Territories_regionid___Region_regionid";
            row["Constrainttype"] = "FOREIGN KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetProcedureParameters() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Typename", typeof(System.String));
            dataTable.Columns.Add("Maxlength", typeof(System.String));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Mode", typeof(System.String));
            dataTable.Columns.Add("Default", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Id"] = "Custorderhist|tcCustomerID";
            row["Parentid"] = "Custorderhist";
            row["Name"] = "tcCustomerID";
            row["Ordinal"] = 1;
            row["Typename"] = "varchar";
            row["Maxlength"] = DBNull.Value;
            row["Precision"] = DBNull.Value;
            row["Datetimeprecision"] = DBNull.Value;
            row["Scale"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Ismultiset"] = false;
            row["Mode"] = "IN";
            row["Default"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Id"] = "Custordersdetail|tiOrderID";
            row["Parentid"] = "Custordersdetail";
            row["Name"] = "tiOrderID";
            row["Ordinal"] = 1;
            row["Typename"] = "int";
            row["Maxlength"] = DBNull.Value;
            row["Precision"] = DBNull.Value;
            row["Datetimeprecision"] = DBNull.Value;
            row["Scale"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Ismultiset"] = false;
            row["Mode"] = "IN";
            row["Default"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Id"] = "Custordersorders|tcCustomerID";
            row["Parentid"] = "Custordersorders";
            row["Name"] = "tcCustomerID";
            row["Ordinal"] = 1;
            row["Typename"] = "varchar";
            row["Maxlength"] = DBNull.Value;
            row["Precision"] = DBNull.Value;
            row["Datetimeprecision"] = DBNull.Value;
            row["Scale"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Ismultiset"] = false;
            row["Mode"] = "IN";
            row["Default"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Id"] = "Salesbyyear|tdBeginning_Date";
            row["Parentid"] = "Salesbyyear";
            row["Name"] = "tdBeginning_Date";
            row["Ordinal"] = 1;
            row["Typename"] = "datetime";
            row["Maxlength"] = DBNull.Value;
            row["Precision"] = DBNull.Value;
            row["Datetimeprecision"] = DBNull.Value;
            row["Scale"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Ismultiset"] = false;
            row["Mode"] = "IN";
            row["Default"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Id"] = "Salesbyyear|tdEnding_Date";
            row["Parentid"] = "Salesbyyear";
            row["Name"] = "tdEnding_Date";
            row["Ordinal"] = 2;
            row["Typename"] = "datetime";
            row["Maxlength"] = DBNull.Value;
            row["Precision"] = DBNull.Value;
            row["Datetimeprecision"] = DBNull.Value;
            row["Scale"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Ismultiset"] = false;
            row["Mode"] = "IN";
            row["Default"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Id"] = "Employeesalesbycountry|tdBeginning_Date";
            row["Parentid"] = "Employeesalesbycountry";
            row["Name"] = "tdBeginning_Date";
            row["Ordinal"] = 1;
            row["Typename"] = "datetime";
            row["Maxlength"] = DBNull.Value;
            row["Precision"] = DBNull.Value;
            row["Datetimeprecision"] = DBNull.Value;
            row["Scale"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Ismultiset"] = false;
            row["Mode"] = "IN";
            row["Default"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Id"] = "Employeesalesbycountry|tdEnding_Date";
            row["Parentid"] = "Employeesalesbycountry";
            row["Name"] = "tdEnding_Date";
            row["Ordinal"] = 2;
            row["Typename"] = "datetime";
            row["Maxlength"] = DBNull.Value;
            row["Precision"] = DBNull.Value;
            row["Datetimeprecision"] = DBNull.Value;
            row["Scale"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Ismultiset"] = false;
            row["Mode"] = "IN";
            row["Default"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetProcedures() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Id"] = "Custorderhist";
            row["Name"] = "Custorderhist";
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Id"] = "Custordersdetail";
            row["Name"] = "Custordersdetail";
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Id"] = "Custordersorders";
            row["Name"] = "Custordersorders";
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Id"] = "Salesbyyear";
            row["Name"] = "Salesbyyear";
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Id"] = "Employeesalesbycountry";
            row["Name"] = "Employeesalesbycountry";
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetFunctionParameters() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Typename", typeof(System.String));
            dataTable.Columns.Add("Maxlength", typeof(System.String));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Mode", typeof(System.String));
            dataTable.Columns.Add("Default", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetFunctions() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Returntypename", typeof(System.String));
            dataTable.Columns.Add("Returnmaxlength", typeof(System.String));
            dataTable.Columns.Add("Returnprecision", typeof(System.Int32));
            dataTable.Columns.Add("Returndatetimeprecision", typeof(System.Int32));
            dataTable.Columns.Add("Returnscale", typeof(System.Int32));
            dataTable.Columns.Add("Returncollationcatalog", typeof(System.String));
            dataTable.Columns.Add("Returncollationschema", typeof(System.String));
            dataTable.Columns.Add("Returncollationname", typeof(System.String));
            dataTable.Columns.Add("Returncharactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Returncharactersetschema", typeof(System.String));
            dataTable.Columns.Add("Returncharactersetname", typeof(System.String));
            dataTable.Columns.Add("Returnismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Isaggregate", typeof(System.Boolean));
            dataTable.Columns.Add("Isbuiltin", typeof(System.Boolean));
            dataTable.Columns.Add("Isniladic", typeof(System.Boolean));
            dataTable.Columns.Add("Istvf", typeof(System.Boolean));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetViewColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Isnullable", typeof(System.Boolean));
            dataTable.Columns.Add("Maxlength", typeof(System.Int32));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Isstoregenerated", typeof(System.Boolean));
            dataTable.Columns.Add("Isidentity", typeof(System.Boolean));
            dataTable.Columns.Add("Default", typeof(System.String));
            dataTable.Columns.Add("Typename", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Parentid"] = "Category_Sales_For_1997";
            row["Name"] = "Categoryname";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Category_Sales_For_1997.Categoryname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Parentid"] = "Category_Sales_For_1997";
            row["Name"] = "Categorysales";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Category_Sales_For_1997.Categorysales";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Parentid"] = "Current_Product_List";
            row["Name"] = "Productid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Current_Product_List.Productid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Parentid"] = "Current_Product_List";
            row["Name"] = "Productname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Current_Product_List.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Parentid"] = "Customer_And_Suppliers_By_City";
            row["Name"] = "City";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customer_And_Suppliers_By_City.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Parentid"] = "Customer_And_Suppliers_By_City";
            row["Name"] = "Companyname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customer_And_Suppliers_By_City.Companyname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Parentid"] = "Customer_And_Suppliers_By_City";
            row["Name"] = "Contactname";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 30;
            row["Precision"] = 30;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customer_And_Suppliers_By_City.Contactname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Parentid"] = "Customer_And_Suppliers_By_City";
            row["Name"] = "Relationship";
            row["Ordinal"] = 4;
            row["Isnullable"] = false;
            row["Maxlength"] = 9;
            row["Precision"] = 9;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customer_And_Suppliers_By_City.Relationship";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shipname";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shipname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shipaddress";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shipaddress";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shipcity";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shipcity";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shipregion";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shipregion";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shippostalcode";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shippostalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shipcountry";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shipcountry";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Customerid";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Customerid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 15

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Customername";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Customername";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 16

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Address";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Address";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 17

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "City";
            row["Ordinal"] = 10;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 18

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Region";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Region";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 19

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Postalcode";
            row["Ordinal"] = 12;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Postalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 20

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Country";
            row["Ordinal"] = 13;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Country";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 21

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Salesperson";
            row["Ordinal"] = 14;
            row["Isnullable"] = true;
            row["Maxlength"] = 31;
            row["Precision"] = 31;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Salesperson";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 22

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Orderid";
            row["Ordinal"] = 15;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 23

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Orderdate";
            row["Ordinal"] = 16;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Orderdate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 24

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Requireddate";
            row["Ordinal"] = 17;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Requireddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 25

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shippeddate";
            row["Ordinal"] = 18;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shippeddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 26

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Shippername";
            row["Ordinal"] = 19;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Shippername";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 27

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Productid";
            row["Ordinal"] = 20;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Productid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 28

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Productname";
            row["Ordinal"] = 21;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 29

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Unitprice";
            row["Ordinal"] = 22;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Unitprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 30

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Quantity";
            row["Ordinal"] = 23;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Quantity";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 31

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Discount";
            row["Ordinal"] = 24;
            row["Isnullable"] = false;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 5;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Discount";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 32

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Extendedprice";
            row["Ordinal"] = 25;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Extendedprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 33

            row = dataTable.NewRow();
            row["Parentid"] = "Invoices";
            row["Name"] = "Freight";
            row["Ordinal"] = 26;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Invoices.Freight";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 34

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Details_Extended";
            row["Name"] = "Orderid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Details_Extended.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 35

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Details_Extended";
            row["Name"] = "Productid";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Details_Extended.Productid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 36

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Details_Extended";
            row["Name"] = "Productname";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Details_Extended.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 37

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Details_Extended";
            row["Name"] = "Unitprice";
            row["Ordinal"] = 4;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Details_Extended.Unitprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 38

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Details_Extended";
            row["Name"] = "Quantity";
            row["Ordinal"] = 5;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Details_Extended.Quantity";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 39

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Details_Extended";
            row["Name"] = "Discount";
            row["Ordinal"] = 6;
            row["Isnullable"] = false;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 5;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Details_Extended.Discount";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 40

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Details_Extended";
            row["Name"] = "Extendedprice";
            row["Ordinal"] = 7;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Details_Extended.Extendedprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 41

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Subtotals";
            row["Name"] = "Orderid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Subtotals.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 42

            row = dataTable.NewRow();
            row["Parentid"] = "Order_Subtotals";
            row["Name"] = "Subtotal";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Order_Subtotals.Subtotal";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 43

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Orderid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 44

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Customerid";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Customerid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 45

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Employeeid";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Employeeid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 46

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Orderdate";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Orderdate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 47

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Requireddate";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Requireddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 48

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shippeddate";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shippeddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 49

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shipvia";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shipvia";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 50

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Freight";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Freight";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 51

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shipname";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shipname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 52

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shipaddress";
            row["Ordinal"] = 10;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shipaddress";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 53

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shipcity";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shipcity";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 54

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shipregion";
            row["Ordinal"] = 12;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shipregion";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 55

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shippostalcode";
            row["Ordinal"] = 13;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shippostalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 56

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Shipcountry";
            row["Ordinal"] = 14;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Shipcountry";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 57

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Companyname";
            row["Ordinal"] = 15;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Companyname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 58

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Address";
            row["Ordinal"] = 16;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Address";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 59

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "City";
            row["Ordinal"] = 17;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 60

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Region";
            row["Ordinal"] = 18;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Region";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 61

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Postalcode";
            row["Ordinal"] = 19;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Postalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 62

            row = dataTable.NewRow();
            row["Parentid"] = "Orders_Qry";
            row["Name"] = "Country";
            row["Ordinal"] = 20;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders_Qry.Country";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 63

            row = dataTable.NewRow();
            row["Parentid"] = "Product_Sales_For_1997";
            row["Name"] = "Categoryname";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Product_Sales_For_1997.Categoryname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 64

            row = dataTable.NewRow();
            row["Parentid"] = "Product_Sales_For_1997";
            row["Name"] = "Productname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Product_Sales_For_1997.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 65

            row = dataTable.NewRow();
            row["Parentid"] = "Product_Sales_For_1997";
            row["Name"] = "Productsales";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Product_Sales_For_1997.Productsales";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 66

            row = dataTable.NewRow();
            row["Parentid"] = "Products_Above_Average_Price";
            row["Name"] = "Productname";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products_Above_Average_Price.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 67

            row = dataTable.NewRow();
            row["Parentid"] = "Products_Above_Average_Price";
            row["Name"] = "Unitprice";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products_Above_Average_Price.Unitprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 68

            row = dataTable.NewRow();
            row["Parentid"] = "Products_By_Category";
            row["Name"] = "Categoryname";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products_By_Category.Categoryname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 69

            row = dataTable.NewRow();
            row["Parentid"] = "Products_By_Category";
            row["Name"] = "Productname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products_By_Category.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 70

            row = dataTable.NewRow();
            row["Parentid"] = "Products_By_Category";
            row["Name"] = "Quantityperunit";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products_By_Category.Quantityperunit";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 71

            row = dataTable.NewRow();
            row["Parentid"] = "Products_By_Category";
            row["Name"] = "Unitsinstock";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products_By_Category.Unitsinstock";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 72

            row = dataTable.NewRow();
            row["Parentid"] = "Products_By_Category";
            row["Name"] = "Discontinued";
            row["Ordinal"] = 5;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products_By_Category.Discontinued";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "logical";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 73

            row = dataTable.NewRow();
            row["Parentid"] = "Quarterly_Orders";
            row["Name"] = "Customerid";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Quarterly_Orders.Customerid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 74

            row = dataTable.NewRow();
            row["Parentid"] = "Quarterly_Orders";
            row["Name"] = "Companyname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Quarterly_Orders.Companyname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 75

            row = dataTable.NewRow();
            row["Parentid"] = "Quarterly_Orders";
            row["Name"] = "City";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Quarterly_Orders.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 76

            row = dataTable.NewRow();
            row["Parentid"] = "Quarterly_Orders";
            row["Name"] = "Country";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Quarterly_Orders.Country";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 77

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_By_Category";
            row["Name"] = "Categoryid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_By_Category.Categoryid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 78

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_By_Category";
            row["Name"] = "Categoryname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_By_Category.Categoryname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 79

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_By_Category";
            row["Name"] = "Productname";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_By_Category.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 80

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_By_Category";
            row["Name"] = "Productsales";
            row["Ordinal"] = 4;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_By_Category.Productsales";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 81

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_Totals_By_Amount";
            row["Name"] = "Saleamount";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_Totals_By_Amount.Saleamount";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 82

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_Totals_By_Amount";
            row["Name"] = "Orderid";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_Totals_By_Amount.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 83

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_Totals_By_Amount";
            row["Name"] = "Companyname";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_Totals_By_Amount.Companyname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 84

            row = dataTable.NewRow();
            row["Parentid"] = "Sales_Totals_By_Amount";
            row["Name"] = "Shippeddate";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sales_Totals_By_Amount.Shippeddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 85

            row = dataTable.NewRow();
            row["Parentid"] = "Summary_Of_Sales_By_Quarter";
            row["Name"] = "Year";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Summary_Of_Sales_By_Quarter.Year";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 86

            row = dataTable.NewRow();
            row["Parentid"] = "Summary_Of_Sales_By_Quarter";
            row["Name"] = "Quarter";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 9;
            row["Precision"] = 9;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Summary_Of_Sales_By_Quarter.Quarter";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 87

            row = dataTable.NewRow();
            row["Parentid"] = "Summary_Of_Sales_By_Quarter";
            row["Name"] = "Total";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Summary_Of_Sales_By_Quarter.Total";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 88

            row = dataTable.NewRow();
            row["Parentid"] = "Summary_Of_Sales_By_Year";
            row["Name"] = "Year";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Summary_Of_Sales_By_Year.Year";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 89

            row = dataTable.NewRow();
            row["Parentid"] = "Summary_Of_Sales_By_Year";
            row["Name"] = "Total";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Summary_Of_Sales_By_Year.Total";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 90

            row = dataTable.NewRow();
            row["Parentid"] = "Ten_Most_Expensive_Products";
            row["Name"] = "Tenmostexpensiveproducts";
            row["Ordinal"] = 1;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Ten_Most_Expensive_Products.Tenmostexpensiveproducts";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 91

            row = dataTable.NewRow();
            row["Parentid"] = "Ten_Most_Expensive_Products";
            row["Name"] = "Unitprice";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Ten_Most_Expensive_Products.Unitprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetViews() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Isupdatable", typeof(System.Boolean));
            dataTable.Columns.Add("Viewdefinition", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Category_Sales_For_1997";
            row["Id"] = "Category_Sales_For_1997";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Current_Product_List";
            row["Id"] = "Current_Product_List";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Customer_And_Suppliers_By_City";
            row["Id"] = "Customer_And_Suppliers_By_City";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Invoices";
            row["Id"] = "Invoices";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Order_Details_Extended";
            row["Id"] = "Order_Details_Extended";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Order_Subtotals";
            row["Id"] = "Order_Subtotals";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Orders_Qry";
            row["Id"] = "Orders_Qry";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Product_Sales_For_1997";
            row["Id"] = "Product_Sales_For_1997";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Products_Above_Average_Price";
            row["Id"] = "Products_Above_Average_Price";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Products_By_Category";
            row["Id"] = "Products_By_Category";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Quarterly_Orders";
            row["Id"] = "Quarterly_Orders";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Sales_By_Category";
            row["Id"] = "Sales_By_Category";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Sales_Totals_By_Amount";
            row["Id"] = "Sales_Totals_By_Amount";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Summary_Of_Sales_By_Quarter";
            row["Id"] = "Summary_Of_Sales_By_Quarter";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Summary_Of_Sales_By_Year";
            row["Id"] = "Summary_Of_Sales_By_Year";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 15

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Ten_Most_Expensive_Products";
            row["Id"] = "Ten_Most_Expensive_Products";
            row["Isupdatable"] = true;
            row["Viewdefinition"] = DBNull.Value;
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetTableColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Isnullable", typeof(System.Boolean));
            dataTable.Columns.Add("Maxlength", typeof(System.Int32));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Isstoregenerated", typeof(System.Boolean));
            dataTable.Columns.Add("Isidentity", typeof(System.Boolean));
            dataTable.Columns.Add("Default", typeof(System.String));
            dataTable.Columns.Add("Typename", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Parentid"] = "Categories";
            row["Name"] = "Categoryid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Categories.Categoryid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = true;
            row["Isidentity"] = true;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Parentid"] = "Categories";
            row["Name"] = "Categoryname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Categories.Categoryname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Parentid"] = "Categories";
            row["Name"] = "Description";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Categories.Description";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Parentid"] = "Categories";
            row["Name"] = "Picture";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Categories.Picture";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "general";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Parentid"] = "CustomerCustomerDemo";
            row["Name"] = "Customerid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "CustomerCustomerDemo.Customerid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Parentid"] = "CustomerCustomerDemo";
            row["Name"] = "Customertypeid";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "CustomerCustomerDemo.Customertypeid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Parentid"] = "CustomerDemographics";
            row["Name"] = "Customertypeid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "CustomerDemographics.Customertypeid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Parentid"] = "CustomerDemographics";
            row["Name"] = "Customerdesc";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "CustomerDemographics.Customerdesc";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Customerid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Customerid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Companyname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Companyname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Contactname";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 30;
            row["Precision"] = 30;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Contactname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Contacttitle";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 30;
            row["Precision"] = 30;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Contacttitle";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Address";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Address";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "City";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Region";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Region";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 15

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Postalcode";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Postalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 16

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Country";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Country";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 17

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Phone";
            row["Ordinal"] = 10;
            row["Isnullable"] = true;
            row["Maxlength"] = 24;
            row["Precision"] = 24;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Phone";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 18

            row = dataTable.NewRow();
            row["Parentid"] = "Customers";
            row["Name"] = "Fax";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = 24;
            row["Precision"] = 24;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Customers.Fax";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 19

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Employeeid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Employeeid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = true;
            row["Isidentity"] = true;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 20

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Lastname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Lastname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 21

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Firstname";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Firstname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 22

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Title";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 30;
            row["Precision"] = 30;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Title";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 23

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Titleofcourtesy";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = 25;
            row["Precision"] = 25;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Titleofcourtesy";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 24

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Birthdate";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Birthdate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 25

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Hiredate";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Hiredate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 26

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Address";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Address";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 27

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "City";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 28

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Region";
            row["Ordinal"] = 10;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Region";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 29

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Postalcode";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Postalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 30

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Country";
            row["Ordinal"] = 12;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Country";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 31

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Homephone";
            row["Ordinal"] = 13;
            row["Isnullable"] = true;
            row["Maxlength"] = 24;
            row["Precision"] = 24;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Homephone";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 32

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Extension";
            row["Ordinal"] = 14;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Extension";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 33

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Photo";
            row["Ordinal"] = 15;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Photo";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 34

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Notes";
            row["Ordinal"] = 16;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Notes";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 35

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Reportsto";
            row["Ordinal"] = 17;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Reportsto";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 36

            row = dataTable.NewRow();
            row["Parentid"] = "Employees";
            row["Name"] = "Photopath";
            row["Ordinal"] = 18;
            row["Isnullable"] = false;
            row["Maxlength"] = 254;
            row["Precision"] = 254;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Employees.Photopath";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 37

            row = dataTable.NewRow();
            row["Parentid"] = "EmployeesTerritories";
            row["Name"] = "Employeeid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "EmployeesTerritories.Employeeid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 38

            row = dataTable.NewRow();
            row["Parentid"] = "EmployeesTerritories";
            row["Name"] = "Territoryid";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "EmployeesTerritories.Territoryid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 39

            row = dataTable.NewRow();
            row["Parentid"] = "Internationalorders";
            row["Name"] = "Orderid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Internationalorders.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 40

            row = dataTable.NewRow();
            row["Parentid"] = "Internationalorders";
            row["Name"] = "Customsdescription";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 100;
            row["Precision"] = 100;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Internationalorders.Customsdescription";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 41

            row = dataTable.NewRow();
            row["Parentid"] = "Internationalorders";
            row["Name"] = "Excisetax";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = 8;
            row["Precision"] = 8;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Internationalorders.Excisetax";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 42

            row = dataTable.NewRow();
            row["Parentid"] = "OrderDetails";
            row["Name"] = "Orderid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "OrderDetails.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 43

            row = dataTable.NewRow();
            row["Parentid"] = "OrderDetails";
            row["Name"] = "Productid";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "OrderDetails.Productid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 44

            row = dataTable.NewRow();
            row["Parentid"] = "OrderDetails";
            row["Name"] = "Unitprice";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = 8;
            row["Precision"] = 8;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "OrderDetails.Unitprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "0";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 45

            row = dataTable.NewRow();
            row["Parentid"] = "OrderDetails";
            row["Name"] = "Quantity";
            row["Ordinal"] = 4;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "OrderDetails.Quantity";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "1";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 46

            row = dataTable.NewRow();
            row["Parentid"] = "OrderDetails";
            row["Name"] = "Discount";
            row["Ordinal"] = 5;
            row["Isnullable"] = false;
            row["Maxlength"] = 19;
            row["Precision"] = 19;
            row["Scale"] = 5;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "OrderDetails.Discount";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "0";
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 47

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Orderid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Orderid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = true;
            row["Isidentity"] = true;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 48

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Customerid";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 5;
            row["Precision"] = 5;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Customerid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 49

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Employeeid";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Employeeid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 50

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Orderdate";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Orderdate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 51

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Requireddate";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Requireddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 52

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shippeddate";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shippeddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 53

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shipvia";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shipvia";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 54

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Freight";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = 8;
            row["Precision"] = 8;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Freight";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "0";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 55

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shipname";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shipname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 56

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shipaddress";
            row["Ordinal"] = 10;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shipaddress";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 57

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shipcity";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shipcity";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 58

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shipregion";
            row["Ordinal"] = 12;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shipregion";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 59

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shippostalcode";
            row["Ordinal"] = 13;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shippostalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 60

            row = dataTable.NewRow();
            row["Parentid"] = "Orders";
            row["Name"] = "Shipcountry";
            row["Ordinal"] = 14;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Orders.Shipcountry";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 61

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Employeeid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Employeeid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 62

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Lastname";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Lastname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 63

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Firstname";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Firstname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 64

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Title";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 30;
            row["Precision"] = 30;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Title";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 65

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Titleofcourtesy";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = 25;
            row["Precision"] = 25;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Titleofcourtesy";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 66

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Birthdate";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Birthdate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "datetime";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 67

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Hiredate";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Hiredate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "datetime";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 68

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Address";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Address";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 69

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "City";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 70

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Region";
            row["Ordinal"] = 10;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Region";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 71

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Postalcode";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Postalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 72

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Country";
            row["Ordinal"] = 12;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Country";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 73

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Homephone";
            row["Ordinal"] = 13;
            row["Isnullable"] = true;
            row["Maxlength"] = 24;
            row["Precision"] = 24;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Homephone";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 74

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Extension";
            row["Ordinal"] = 14;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Extension";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 75

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Photo";
            row["Ordinal"] = 15;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Photo";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 76

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Notes";
            row["Ordinal"] = 16;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Notes";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 77

            row = dataTable.NewRow();
            row["Parentid"] = "Previousemployees";
            row["Name"] = "Photopath";
            row["Ordinal"] = 17;
            row["Isnullable"] = true;
            row["Maxlength"] = 254;
            row["Precision"] = 254;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Previousemployees.Photopath";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 78

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Productid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Productid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = true;
            row["Isidentity"] = true;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 79

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Productname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Productname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 80

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Supplierid";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Supplierid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 81

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Categoryid";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Categoryid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 82

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Quantityperunit";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Quantityperunit";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 83

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Unitprice";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = 8;
            row["Precision"] = 8;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Unitprice";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 84

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Unitsinstock";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Unitsinstock";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "0";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 85

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Unitsonorder";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Unitsonorder";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 86

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Reorderlevel";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Reorderlevel";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "0";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 87

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Discontinued";
            row["Ordinal"] = 10;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Discontinued";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = ".F.";
            row["Typename"] = "logical";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 88

            row = dataTable.NewRow();
            row["Parentid"] = "Products";
            row["Name"] = "Discontinueddate";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Products.Discontinueddate";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "datetime";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 89

            row = dataTable.NewRow();
            row["Parentid"] = "Region";
            row["Name"] = "Regionid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Region.Regionid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = true;
            row["Isidentity"] = true;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 90

            row = dataTable.NewRow();
            row["Parentid"] = "Region";
            row["Name"] = "Regiondescription";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 50;
            row["Precision"] = 50;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Region.Regiondescription";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 91

            row = dataTable.NewRow();
            row["Parentid"] = "Shippers";
            row["Name"] = "Shipperid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Shippers.Shipperid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = true;
            row["Isidentity"] = true;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 92

            row = dataTable.NewRow();
            row["Parentid"] = "Shippers";
            row["Name"] = "Companyname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Shippers.Companyname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 93

            row = dataTable.NewRow();
            row["Parentid"] = "Shippers";
            row["Name"] = "Phone";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 24;
            row["Precision"] = 24;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Shippers.Phone";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 94

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Supplierid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Supplierid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = true;
            row["Isidentity"] = true;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 95

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Companyname";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 40;
            row["Precision"] = 40;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Companyname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 96

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Contactname";
            row["Ordinal"] = 3;
            row["Isnullable"] = true;
            row["Maxlength"] = 30;
            row["Precision"] = 30;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Contactname";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 97

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Contacttitle";
            row["Ordinal"] = 4;
            row["Isnullable"] = true;
            row["Maxlength"] = 30;
            row["Precision"] = 30;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Contacttitle";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 98

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Address";
            row["Ordinal"] = 5;
            row["Isnullable"] = true;
            row["Maxlength"] = 60;
            row["Precision"] = 60;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Address";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 99

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "City";
            row["Ordinal"] = 6;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.City";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 100

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Region";
            row["Ordinal"] = 7;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Region";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 101

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Postalcode";
            row["Ordinal"] = 8;
            row["Isnullable"] = true;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Postalcode";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 102

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Country";
            row["Ordinal"] = 9;
            row["Isnullable"] = true;
            row["Maxlength"] = 15;
            row["Precision"] = 15;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Country";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 103

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Phone";
            row["Ordinal"] = 10;
            row["Isnullable"] = true;
            row["Maxlength"] = 24;
            row["Precision"] = 24;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Phone";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 104

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Fax";
            row["Ordinal"] = 11;
            row["Isnullable"] = true;
            row["Maxlength"] = 24;
            row["Precision"] = 24;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Fax";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 105

            row = dataTable.NewRow();
            row["Parentid"] = "Suppliers";
            row["Name"] = "Homepage";
            row["Ordinal"] = 12;
            row["Isnullable"] = true;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Suppliers.Homepage";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 106

            row = dataTable.NewRow();
            row["Parentid"] = "Territories";
            row["Name"] = "Territoryid";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 20;
            row["Precision"] = 20;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Territories.Territoryid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 107

            row = dataTable.NewRow();
            row["Parentid"] = "Territories";
            row["Name"] = "Territorydescription";
            row["Ordinal"] = 2;
            row["Isnullable"] = true;
            row["Maxlength"] = 50;
            row["Precision"] = 50;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Territories.Territorydescription";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 108

            row = dataTable.NewRow();
            row["Parentid"] = "Territories";
            row["Name"] = "Regionid";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Territories.Regionid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = "";
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetTables() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Categories";
            row["Id"] = "Categories";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "CustomerCustomerDemo";
            row["Id"] = "CustomerCustomerDemo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "CustomerDemographics";
            row["Id"] = "CustomerDemographics";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Customers";
            row["Id"] = "Customers";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Employees";
            row["Id"] = "Employees";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "EmployeesTerritories";
            row["Id"] = "EmployeesTerritories";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Internationalorders";
            row["Id"] = "Internationalorders";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "OrderDetails";
            row["Id"] = "OrderDetails";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Orders";
            row["Id"] = "Orders";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Previousemployees";
            row["Id"] = "Previousemployees";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Products";
            row["Id"] = "Products";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Region";
            row["Id"] = "Region";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Shippers";
            row["Id"] = "Shippers";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Suppliers";
            row["Id"] = "Suppliers";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Territories";
            row["Id"] = "Territories";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }
    }
}
