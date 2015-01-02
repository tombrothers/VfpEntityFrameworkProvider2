using System;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using NorthwindEFModel;
using SampleQueries.Harness;

namespace SampleQueries.Samples {
    [Title("Entity SQL over ObjectQuery<T>")]
    [Prefix("EntitySQL")]
    class EntitySQLSamples : NorthwindBasedSample {
        #region Restriction Operators
        [Category("Restriction Operators")]
        [Title("Where - Entity SQL Literal")]
        [Description("This sample finds all customers in Seattle.")]
        public void EntitySQLR1() {
            string entitySQL = "SELECT VALUE c FROM Customers AS c WHERE c.Address.City = 'Seattle';";
            ObjectQuery<Customer> query = context.CreateQuery<Customer>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Restriction Operators")]
        [Title("Where - DateTime Parameter")]
        [Description("This sample finds all orders placed before 1997.")]
        public void EntitySQLR2() {
            DateTime dt = new DateTime(1997, 1, 1);

            string entitySQL = "SELECT VALUE o FROM Orders AS o WHERE o.OrderDate < @orderDate;";
            ObjectParameter[] queryParams = { new ObjectParameter("orderDate", dt) };
            ObjectQuery<Order> query = context.CreateQuery<Order>(entitySQL, queryParams);

            // show top level objects only
            ObjectDumper.Write(query, 1, 5);
        }

        [Category("Restriction Operators")]
        [Title("Where - Composite predicate")]
        [Description("This sample find Products that have stock below their reorder level and have a units on order of zero.")]
        public void EntitySQLR3() {
            string entitySQL = "SELECT VALUE p FROM Products AS p WHERE p.UnitsInStock < p.ReorderLevel AND p.UnitsOnOrder = 0;";
            ObjectQuery<Product> query = context.CreateQuery<Product>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        [Category("Restriction Operators")]
        [Title("Where - Decimal Parameter")]
        [Description("This sample finds Products that have a UnitPrice less than 10.")]
        public void EntitySQLR4() {
            decimal price = 10;

            string entitySQL = "SELECT VALUE p FROM Products AS p WHERE p.UnitPrice < @price;";
            ObjectParameter[] queryParams = { new ObjectParameter("price", price) };
            ObjectQuery<Product> query = context.CreateQuery<Product>(entitySQL, queryParams);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        [Category("Restriction Operators")]
        [Title("Where - Related Entities 1")]
        [Description("This sample finds Orders for Customers in Mexico.")]
        public void EntitySQLR5() {
            string entitySQL = "SELECT VALUE o FROM Orders AS o WHERE o.Customer.Address.Country = 'Mexico';";
            ObjectQuery<Order> query = context.CreateQuery<Order>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        [Category("Restriction Operators")]
        [Title("Where - Related Entities 2")]
        [Description("This sample finds orders for customers not in Mexico or the UK.")]
        public void EntitySQLR7() {
            string entitySQL = "SELECT VALUE o FROM Orders AS o WHERE o.Customer.Address.Country <> 'UK' AND o.Customer.Address.Country <> 'Mexico'  ;";
            ObjectQuery<Order> query = context.CreateQuery<Order>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);

        }

        [Category("Restriction Operators")]
        [Title("Exists")]
        [Description("This sample finds suppliers for any out-of-stock products.")]
        public void EntitySQLR8() {
            string entitySQL = "SELECT VALUE s FROM Suppliers AS s WHERE EXISTS(SELECT p FROM s.Products AS p WHERE p.UnitsInStock = 0);";
            ObjectQuery<Supplier> query = context.CreateQuery<Supplier>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        #endregion

        #region Aggregate Operators

        [Category("Aggregate Operators")]
        [Title("Count - Simple")]
        [Description("This sample uses COUNT to get the number of Orders.")]
        public void EntitySQLA1() {
            string entitySQL = "SELECT VALUE Count(o.OrderID) FROM Orders AS o;";
            ObjectQuery<Int32> query = context.CreateQuery<Int32>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Count - Predicate 1")]
        [Description("This sample uses COUNT to get the number of Orders placed by Customers in Mexico.")]
        public void EntitySQLA2() {
            string entitySQL = "SELECT VALUE Count(o.OrderID) FROM Orders AS o WHERE o.Customer.Address.Country = 'Mexico';";
            ObjectQuery<Int32> query = context.CreateQuery<Int32>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Count - Predicate 2")]
        [Description("This sample uses COUNT to get the number of Orders shipped to Mexico.")]
        public void EntitySQLA3() {
            string entitySQL = "SELECT VALUE Count(o.OrderID) FROM Orders AS o WHERE o.ShipCountry == 'Mexico';";
            ObjectQuery<Int32> query = context.CreateQuery<Int32>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Sum - Simple 1")]
        [Description("This sample uses SUM to find the total freight over all Orders.")]
        public void EntitySQLA4() {
            string entitySQL = "SELECT VALUE Sum(o.Freight) FROM Orders AS o;";
            ObjectQuery<Decimal> query = context.CreateQuery<Decimal>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Sum - Simple 2")]
        [Description("This sample uses SUM to find the total number of units on order over all Products.")]
        public void EntitySQLA5() {
            string entitySQL = "SELECT VALUE Sum(p.UnitsOnOrder) FROM Products AS p;";
            ObjectQuery<Int32> query = context.CreateQuery<Int32>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Sum - Simple 3")]
        [Description("This sample uses SUM to find the total number of units on order over all Products out-of-stock.")]
        public void EntitySQLA6() {
            string entitySQL = "SELECT VALUE Sum(p.UnitsOnOrder) FROM Products AS p WHERE p.UnitsInStock = 0;";
            ObjectQuery<Int32> query = context.CreateQuery<Int32>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Simple 1")]
        [Description("This sample uses MIN to find the lowest unit price of any Product.")]
        public void EntitySQLA7() {
            string entitySQL = "SELECT VALUE Min(p.UnitPrice) FROM Products AS p;";
            ObjectQuery<Decimal> query = context.CreateQuery<Decimal>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Simple 2")]
        [Description("This sample uses MIN to find the lowest freight of any Order.")]
        public void EntitySQLA8() {
            string entitySQL = "SELECT VALUE Min(o.Freight) FROM Orders AS o;";
            ObjectQuery<Decimal> query = context.CreateQuery<Decimal>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Predicate")]
        [Description("This sample uses MIN to find the lowest freight of any Order shipped to Mexico.")]
        public void EntitySQLA9() {
            string entitySQL = "SELECT VALUE Min(o.Freight) FROM Orders AS o WHERE o.ShipCountry = 'Mexico';";
            ObjectQuery<Decimal> query = context.CreateQuery<Decimal>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Grouping")]
        [Description("This sample uses Min to find the Products that have the lowest unit price in each category.")]
        public void EntitySQLA10() {
            string entitySQL = "SELECT Min(p.UnitPrice) AS MinPrice, p.Category.CategoryName FROM Products AS p GROUP BY p.Category.CategoryName;";
            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Simple 1")]
        [Description("This sample uses MAX to find the latest hire date of any Employee.")]
        public void EntitySQLA11() {
            string entitySQL = "SELECT VALUE Max(e.HireDate) FROM Employees AS e;";
            ObjectQuery<DateTime> query = context.CreateQuery<DateTime>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Simple 2")]
        [Description("This sample uses MAX to find the most units in stock of any Product.")]
        public void EntitySQLA12() {
            string entitySQL = "SELECT VALUE Max(p.UnitsInStock) FROM Products AS p;";
            ObjectQuery<Int16> query = context.CreateQuery<Int16>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Predicate")]
        [Description("This sample uses MAX to find the most units in stock of any Product with CategoryID = 1.")]
        public void EntitySQLA13() {
            string entitySQL = "SELECT VALUE Max(p.UnitsInStock) FROM Products AS p WHERE p.Category.CategoryID = 1;";
            ObjectQuery<Int16> query = context.CreateQuery<Int16>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Grouping")]
        [Description("This sample uses MAX to find the Products that have the highest unit price in each category, and returns the result AS an anonoymous type.")]
        public void EntitySQLA14() {
            string entitySQL = "SELECT Max(p.UnitPrice) AS MaxPrice, CategoryName FROM Products AS p GROUP BY p.Category.CategoryName AS CategoryName;";
            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Simple 1")]
        [Description("This sample uses AVERAGE to find the average freight of all Orders.")]
        public void EntitySQLA15() {
            string entitySQL = "SELECT VALUE Avg(o.Freight) FROM Orders AS o;";
            ObjectQuery<Decimal> query = context.CreateQuery<Decimal>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Simple 2")]
        [Description("This sample uses AVERAGE to find the average unit price of all Products.")]
        public void EntitySQLA16() {
            string entitySQL = "SELECT VALUE Avg(p.UnitPrice) FROM Products AS p;";
            ObjectQuery<Decimal> query = context.CreateQuery<Decimal>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Predicate")]
        [Description("This sample uses AVERAGE to find the average unit price of all Products with CategoryID = 1.")]
        public void EntitySQLA17() {
            string entitySQL = "SELECT VALUE Avg(p.UnitPrice) FROM Products AS p WHERE p.Category.CategoryID = 1;";
            ObjectQuery<Decimal> query = context.CreateQuery<Decimal>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Grouping 2")]
        [Description("This sample uses AVERAGE to find the average unit price of each category.")]
        public void EntitySQLA19() {
            string entitySQL = "SELECT Avg(p.UnitPrice) AS AvgPrice, p.Category.CategoryName FROM Products AS p GROUP BY p.Category.CategoryName;";
            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Aggregate in subquery predicate 1")]
        [Description("This sample retrieves the Product whith the maximum ProductID using MAX() on a subquery of ProductID.")]
        public void EntitySQLA20() {
            string entitySql = @"SELECT VALUE p FROM Products AS p WHERE p.ProductID = MAX(SELECT VALUE p2.ProductID FROM Products AS p2);";
            var query = context.CreateQuery<Product>(entitySql);

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Aggregate in subquery predicate 2")]
        [Description("This sample retrieves the Product whith the maximum ProductID using ANYELEMENT() on a subquery containing MAX(ProductID).")]
        public void EntitySQLA21() {
            string entitySql = @"SELECT VALUE p FROM Products AS p WHERE p.ProductID = ANYELEMENT(SELECT VALUE MAX(p2.ProductID) FROM Products AS p2);";
            var query = context.CreateQuery<Product>(entitySql);

            ObjectDumper.Write(query, 1);

        }


        #endregion

        #region Ordering and Grouping

        [Category("Ordering and Grouping")]
        [Title("Order By - Simple 1")]
        [Description("Select all customers ordered by ContactName.")]
        public void EntitySQLO1() {
            string entitySQL = "SELECT VALUE c FROM Customers AS c ORDER BY c.ContactName;";
            ObjectQuery<Customer> query = context.CreateQuery<Customer>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Ordering and Grouping")]
        [Title("Order By - Simple 2")]
        [Description("Select all customers ordered by ContactName descending.")]
        public void EntitySQLO2() {
            string entitySQL = "SELECT VALUE c FROM Customers AS c ORDER BY c.ContactName DESC;";
            ObjectQuery<Customer> query = context.CreateQuery<Customer>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        //[Category("Ordering and Grouping")]
        //[Title("Order By - Related Entities")]
        //[Description("Retrieve customers ordered by number of orders.")]
        //public void EntitySQL57()
        //{
        //    string entitySQL = "SELECT VALUE c2.c FROM (SELECT c,  ANYELEMENT(SELECT VALUE Count(o.OrderId) FROM c.Orders AS o) AS orderCount FROM Customers AS c) AS c2 ORDER BY c2.orderCount";
        //    ObjectQuery<Customer> query = context.CreateQuery<Customer>(entitySQL);

        //    ObjectDumper.Write(query, 1);
        //}

        //[Category("Ordering and Grouping")]
        //[Title("Order By - Related Entities with Predicate")]
        //[Description("Retrieve customers with orders in year 1996 ordered by number of those orders.")]
        //public void EntitySQL58()
        //{
        //    string entitySQL = "SELECT VALUE c2.c FROM (SELECT c,  ANYELEMENT(SELECT VALUE Count(o.OrderId) FROM c.Orders AS o WHERE Year(o.OrderDate) = 1996) AS orderCount FROM Customers AS c) AS c2 WHERE c2.OrderCount > 0 ORDER BY c2.orderCount";
        //    ObjectQuery<Customer> query = context.CreateQuery<Customer>(entitySQL);

        //    ObjectDumper.Write(query, 1);
        //}

        [Category("Ordering and Grouping")]
        [Title("Order By - Multiple Ordering ")]
        [Description("")]
        public void EntitySQL61() {
            string entitySQL = "SELECT VALUE c FROM Customers AS c ORDER BY c.CompanyName ASC, c.ContactTitle DESC";
            ObjectQuery<Customer> query = context.CreateQuery<Customer>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Ordering and Grouping")]
        [Title("Order By - Navigation Reference")]
        [Description("Select all orders for a customer ordered by date that the order was placed.")]
        public void EntitySQL63() {
            string entitySQL = "SELECT VALUE o FROM Orders AS o WHERE o.Customer.CustomerID = 'ROMEY' ORDER BY o.OrderDate";
            ObjectQuery<Order> query = context.CreateQuery<Order>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Ordering and Grouping")]
        [Title("Grouping - Simple")]
        [Description("Select all dates with number of orders placed.")]
        public void EntitySQL65() {
            string entitySQL = "SELECT o.OrderDate, Count(o.OrderID) AS Count FROM Orders AS o GROUP BY o.OrderDate";
            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Ordering and Grouping")]
        [Title("GroupPartition - Simple")]
        [Description("Project all orders grouped by employee")]
        public void EntitySQL66() {
            string entitySQL = "SELECT GroupPartition(o) As OrdersByEmployee FROM Orders AS o GROUP BY o.EmployeeID";

            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Ordering and Grouping")]
        [Title("GroupPartition - With IQF")]
        [Description("Project the name of each category, along with the count of products in that category.")]
        public void EntitySQL67() {
            string entitySQL = @"Using NorthwindEFModel;
                                Function ProductCount(products Collection(Product)) AS
                                (   
	                                Count( Select value 1 From products)
                                )
                                SELECT CategoryName, ProductCount(GroupPartition(P)) as ProductCount
                                FROM Products as P 
                                GROUP BY P.Category.CategoryName as CategoryName
                                ";

            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }
        #endregion

        #region Canonical Functions

        [Category("Canonical Functions")]
        [Title("Left() and Right()")]
        [Description("LEFT,RIGHT,SUBSTRING - 2")]
        public void EntitySQLCanonicalLeftAndRight() {
            ObjectQuery<String> query;
            string entitySQL;

            entitySQL = "CONCAT(LEFT('foo',3),RIGHT('bar',3))";
            query = context.CreateQuery<String>(entitySQL);
            ObjectDumper.Write(query, 1);

            entitySQL = "CONCAT(LEFT('foobar',3),RIGHT('barfoo',3))";
            query = context.CreateQuery<String>(entitySQL);
            ObjectDumper.Write(query, 1);

            entitySQL = "CONCAT(LEFT('foobar',8),RIGHT('barfoo',8))";
            query = context.CreateQuery<String>(entitySQL);
            ObjectDumper.Write(query, 1);
        }

        [Category("Canonical Functions")]
        [Title("Substring()")]
        [Description("Substring canonical function")]
        public void EntitySQLCanonicalSubstring() {
            ObjectQuery<String> query;
            string entitySQL;

            entitySQL = "SUBSTRING('foobarfoo',4,3)";
            query = context.CreateQuery<String>(entitySQL);
            ObjectDumper.Write(query, 1);

            entitySQL = "SUBSTRING('foobarfoo',4,30)";
            query = context.CreateQuery<String>(entitySQL);
            ObjectDumper.Write(query, 1);
        }

        [Category("Canonical Functions")]
        [Title("Replace()")]
        [Description("Replace")]
        public void EntitySQLCanonicalTrim() {
            ObjectDumper.Write(context.CreateQuery<string>("Replace('abcdefghi','def','zzz')"), 1);
        }

        [Category("Canonical Functions")]
        [Title("Round()/Floor()/Ceiling()")]
        [Description("Round()/Floor()/Ceiling()")]
        public void EntitySQLCanonicalRound() {
            ObjectDumper.Write(context.CreateQuery<DbDataRecord>(@"
        SELECT o.OrderID, 
               o.Freight, 
               Round(o.Freight) AS [Rounded Freight],
               Floor(o.Freight) AS [Floor of Freight], 
               Ceiling(o.Freight) AS [Ceiling of Freight] 
               FROM Orders AS o"), 2);
        }

        [Category("Canonical Functions")]
        [Title("Bitwise operators")]
        [Description("BitwiseAnd() BitwiseOr() BitwiseNot() BitwiseXor()")]
        public void EntitySQLCanonicalBitwise() {
            ObjectDumper.Write(context.CreateQuery<Int32>(@"BitwiseAnd(255,15)"));
            ObjectDumper.Write(context.CreateQuery<Int32>(@"BitwiseOr(240,31)"));
            ObjectDumper.Write(context.CreateQuery<Int32>(@"BitwiseXor(255,15)"));
        }

        [Category("Canonical Functions")]
        [Title("ToUpper()/ToLower()/Reverse()")]
        [Description("ToUpper()/ToLower()")]
        public void EntitySQLCanonicalToUpperToLower() {
            ObjectDumper.Write(context.CreateQuery<DbDataRecord>("SELECT ToUpper(e.LastName),ToLower(e.FirstName) FROM Employees AS e"), 2);
        }

        [Category("Canonical Functions")]
        [Title("IndexOf()")]
        [Description("IndexOf()")]
        public void EntitySQLCanonicalIndexOf() {
            ObjectQuery<Int32> query;
            string entitySQL;

            entitySQL = "IndexOf('haystackneedle','needle')";
            query = context.CreateQuery<Int32>(entitySQL);
            ObjectDumper.Write(query, 1);

            entitySQL = "IndexOf('haystack','needle')";
            query = context.CreateQuery<Int32>(entitySQL);
            ObjectDumper.Write(query, 1);
        }

        [Category("Canonical Functions")]
        [Title("Trim(),LTrim(),RTrim()")]
        [Description("Trim(),LTrim(),RTrim()")]
        public void EntitySQLCanonicalReplace() {
            ObjectDumper.Write(context.CreateQuery<string>("LTrim('   text   ')"), 1);
            ObjectDumper.Write(context.CreateQuery<string>("RTrim('   text   ')"), 1);
            ObjectDumper.Write(context.CreateQuery<string>("Trim('   text   ')"), 1);
        }

        [Category("Canonical Functions")]
        [Title("Length()")]
        [Description("Length()")]
        public void EntitySQLCanonicalLength() {
            ObjectQuery<Int32> query;
            string entitySQL;

            entitySQL = "Length('abc')";
            query = context.CreateQuery<Int32>(entitySQL);
            ObjectDumper.Write(query, 1);
        }

        [Category("Canonical Functions")]
        [Title("Year(), Month(), Day()")]
        [Description("date part functions")]
        public void EntitySQLCanonicalDatePart() {
            ObjectQuery<DbDataRecord> query;
            string entitySQL;

            entitySQL = "SELECT o.RequiredDate, Year(o.RequiredDate), Month(o.RequiredDate), Day(o.RequiredDate) FROM Orders AS o";
            query = context.CreateQuery<DbDataRecord>(entitySQL);
            ObjectDumper.Write(query, 1);
        }

        [Category("Canonical Functions")]
        [Title("Hour(), Minute(), Second()")]
        [Description("date part functions")]
        public void EntitySQLCanonicalDatePart2() {
            ObjectQuery<DbDataRecord> query;
            string entitySQL;

            entitySQL = "SELECT o.RequiredDate, Year(o.RequiredDate), Month(o.RequiredDate), Day(o.RequiredDate), Hour(o.RequiredDate), Minute(o.RequiredDate), Second(o.RequiredDate) FROM Orders AS o";
            query = context.CreateQuery<DbDataRecord>(entitySQL).OrderBy("it.RequiredDate").Top("3");
            ObjectDumper.Write(query, 1);
        }

        [Category("Canonical Functions")]
        [Title("CurrentDateTime()")]
        [Description("date part functions")]
        public void EntitySQLCanonicalCurrentDateTimeFunction() {
            ObjectDumper.Write(context.CreateQuery<DateTime>("CurrentDateTime()"), 1);
        }

        //[Category("Canonical Functions")]
        //[Title("CurrentUtcDateTime()")]
        //[Description("date part functions")]
        //public void EntitySQLCanonicalCurrentUtcDateTimeFunction()
        //{
        //    ObjectDumper.Write(context.CreateQuery<DateTime>("CurrentUtcDateTime()"), 1);
        //}

        //[Category("Canonical Functions")]
        //[Title("CurrentDateTimeOffset()")]
        //[Description("date part functions")]
        //public void EntitySQLCanonicalCurrentDateTimeOffsetFunctions()
        //{
        //    ObjectDumper.Write(context.CreateQuery<DateTimeOffset>("CurrentDateTimeOffset()"), 1);
        //}

        //[Category("Canonical Functions")]
        //[Title("GetTotalOffsetMinutes()")]
        //[Description("date part functions")]
        //public void EntitySQLCanonicalGetTotalOffsetMinutes()
        //{
        //    ObjectDumper.Write(context.CreateQuery<Int32>("GetTotalOffsetMinutes(CurrentDateTimeOffset())"), 1);
        //}

        //[Category("Canonical Functions")]
        //[Title("NewGuid()")]
        //[Description("NewGuid()")]
        //public void EntitySQLCanonicalNewGuid()
        //{
        //    string entitySQL;

        //    entitySQL = "CAST(NewGuid() as System.String)";
        //    ObjectDumper.Write(context.CreateQuery<string>(entitySQL), 1);
        //}

        [Category("Canonical Functions")]
        [Title("DateDiff functions")]
        [Description("Use the DiffYears canonical function to return all orders placed during the last 20 years")]
        public void EntitySQLCanonicalDateDiff() {
            ObjectQuery<Order> query;
            string entitySQL;

            entitySQL = "Select Value o From Orders as o Where DiffYears(o.OrderDate, CurrentDateTime()) < 20";
            query = context.CreateQuery<Order>(entitySQL);
            ObjectDumper.Write(query);
        }

        #endregion

        #region Query Operators

        [Category("Query Operators")]
        [Title("CAST with literals")]
        [Description("Casting literals")]
        public void EntitySQLOpCast1() {
            ObjectDumper.Write(context.CreateQuery<object>(@"
                ROW(
                    CAST(1 AS System.Byte) AS [ByteVal],
                    CAST(1 AS System.Int16) AS [Int16Val],
                    CAST(1 AS System.Int32) AS [Int32Val],
                    CAST(1 AS System.Int64) AS [Int64Val],
                    CAST(true AS System.Boolean) AS [BooleanVal],
                    CAST(3.14159265358979323846 AS System.Single) AS [SingleVal],
                    CAST(3.14159265358979323846 AS System.Double) AS [DoubleVal],
                    '' AS [StringVal]
                )
            "), 2);
        }

        #endregion

        #region Relational Operators

        //[Category("Relational Operators")]
        //[Title("CROSS JOIN")]
        //[Description("This sample finds a cross product of all Categories and Products")]
        //public void EntitySQLJoin1()
        //{
        //    string entitySQL = "SELECT TOP(10) c,c2 FROM Categories AS c CROSS JOIN Products AS c2";
        //    ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

        //    ObjectDumper.Write(query, 1);
        //}

        [Category("Relational Operators")]
        [Title("UNION")]
        [Description("Union of all customer and employee names along with their titles")]
        public void EntitySQLUnion() {
            string entitySQL = "(SELECT c.ContactName, c.ContactTitle FROM Customers as c) UNION (SELECT e.FirstName + ' ' + e.LastName, e.Title FROM Employees as e)";
            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        [Category("Relational Operators")]
        [Title("UNION ALL")]
        [Description("Union of all customer and employee names along with their titles")]
        public void EntitySQLUnionAll() {
            string entitySQL = "(SELECT c.ContactName, c.ContactTitle FROM Customers as c) UNION ALL (SELECT e.FirstName + ' ' + e.LastName, e.Title FROM Employees as e)";
            ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

            ObjectDumper.Write(query, 1);
        }

        //[Category("Relational Operators")]
        //[Title("EXCEPT")]
        //[Description("Selects all customers which are not employees")]
        //public void EntitySQLExcept()
        //{
        //    string entitySQL = "(SELECT c.ContactName, c.ContactTitle FROM Customers as c) EXCEPT (SELECT e.FirstName + ' ' + e.LastName, e.Title FROM Employees as e)";
        //    ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

        //    ObjectDumper.Write(query, 1);
        //}

        //[Category("Relational Operators")]
        //[Title("INTERSECT")]
        //[Description("Selects all customers which are also employees")]
        //public void EntitySQLIntersect()
        //{
        //    string entitySQL = "(SELECT c.ContactName, c.ContactTitle FROM Customers as c) INTERSECT (SELECT e.FirstName + ' ' + e.LastName, e.Title FROM Employees as e)";
        //    ObjectQuery<DbDataRecord> query = context.CreateQuery<DbDataRecord>(entitySQL);

        //    ObjectDumper.Write(query, 1);
        //}

        #endregion

        #region User Defined Functions
        [Category("User Defined Functions")]
        [Title("Calling TimesMDF in Query")]
        [Description("Calling a simple Model Defined Function (MDF) from a query")]
        public void EntitySQLMDF1() {
            string entitySQL = @"NorthwindEFModel.TimesMDF(6,7)";

            ObjectQuery<object> query = context.CreateQuery<object>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        [Category("User Defined Functions")]
        [Title("Calling GetProductsForCategory MDF in Query")]
        [Description("Calling a MDF that returns a collection of entities")]
        public void EntitySQLMDF2() {
            string entitySQL = @" NorthwindEFModel.GetProductsForCategory('Beverages')";

            ObjectQuery<object> query = context.CreateQuery<object>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        [Category("User Defined Functions")]
        [Title("Calling overloaded GetProductsForCategory MDF in Query")]
        [Description("Calling an overloaded MDF that takes a collection of entities and returns a filtered collection of entities")]
        public void EntitySQLMDF3() {
            string entitySQL = @"NorthwindEFModel.GetProductsForCategory(
                                    Oftype(Products, NorthwindEFModel.DiscontinuedProduct), 'Meat/Poultry')";

            ObjectQuery<object> query = context.CreateQuery<object>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        [Category("User Defined Functions")]
        [Title("Defining an IQF")]
        [Description("Defining and calling an Inline Query Function (IQF) in a query")]
        public void EntitySQLIQF1() {
            string entitySQL = @"--Define the IQF
                             FUNCTION GetCustomersForCity(cityName System.String) as
                             (
                                 SELECT VALUE c FROM Customers AS c WHERE c.Address.City = cityName
                             )
                             --Call the IQF in the query body
                             GetCustomersForCity('London')";

            ObjectQuery<object> query = context.CreateQuery<object>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        [Category("User Defined Functions")]
        [Title("Defining an IQF that calls a MDF")]
        [Description("Defining an Inline Query Function (IQF) that calls a Model Defined Function (MDF)")]
        public void EntitySQLIQF_MDF1() {
            string entitySQL = @"Function XSquared(X System.Int32) as
                                (
                                    NorthwindEFModel.TimesMDF(X,X)
                                )
                                XSquared(4)";

            ObjectQuery<object> query = context.CreateQuery<object>(entitySQL);

            // show top level objects only
            ObjectDumper.Write(query, 1);
        }

        #endregion
    }
}