using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using NorthwindEFModel;
using SampleQueries.Harness;

namespace SampleQueries.Samples {
    [Title("Query Builder Method")]
    [Prefix("ObjectServices")]
    class BuilderMethodSamples : NorthwindBasedSample {
        #region Where

        [Category("Restriction")]
        [Title("Where - Simple")]
        [Description("This sample loads Categories with the CategoryName equal to Beverages.")]
        public void ObjectServicesRestriction1() {
            var query = context.Categories.Where("it.CategoryName = 'Beverages'");

            ObjectDumper.Write(query);
        }

        [Category("Restriction")]
        [Title("Where - Wildcard")]
        [Description("This sample loads Categories where the CategoryName starts with C.")]
        public void ObjectServicesRestriction2() {
            var query = context.Categories.Where("it.CategoryName LIKE 'C%'");

            ObjectDumper.Write(query);
        }

        [Category("Restriction")]
        [Title("Where - Related Simple")]
        [Description("This sample loads Order Details where the related Product is Aniseed Syrup.")]
        public void ObjectServicesRestriction3() {
            var query = context.OrderDetails.Where("it.Product.ProductName = 'Aniseed Syrup'");

            ObjectDumper.Write(query);
        }

        [Category("Restriction")]
        [Title("Where - Related Wilcard")]
        [Description("This sample loads Order Details where the related Product starts with C.")]
        public void ObjectServicesRestriction4() {
            var query = context.OrderDetails.Where("it.Product.ProductName LIKE 'C%'");

            ObjectDumper.Write(query);
        }

        [Category("Restriction")]
        [Title("Where - Parameter")]
        [Description("This sample loads Categories where the CategoryName starts with C.")]
        public void ObjectServicesRestriction5() {
            string name = "Suyama";
            var query = context.Employees.Where("it.LastName = @name", new ObjectParameter("name", name));

            ObjectDumper.Write(query);
        }

        #endregion

        #region Set

        [Category("Set operations")]
        [Title("Union - Entity Type")]
        [Description("This sample shows the union of Products with UnitPrice less than 10 and greater than 50.")]
        public void ObjectServicesSet1() {
            var query1 = context.Products.Where("it.UnitPrice < 10");
            var query2 = context.Products.Where("it.UnitPrice > 50");

            var query = query1.Union(query2);

            ObjectDumper.Write(query);
        }

        [Category("Set operations")]
        [Title("Union - Primitive Type")]
        [Description("This sample shows the union of the LastName of Employees with a title that starts with Sales and the FirstName of Employees with a title that starts with Vice.")]
        public void ObjectServicesSet2() {
            var query1 = context.Employees.Where("it.Title LIKE 'Sales%'").Select("it.LastName, it.Title");
            var query2 = context.Employees.Where("it.Title LIKE 'Vice%'").Select("it.FirstName, it.Title");

            var query = query1.Union(query2);

            ObjectDumper.Write(query);
        }

        [Category("Set operations")]
        [Title("Union - Primitive Type with filter")]
        [Description("This sample shows the union of the LastName of Employees with a title that starts with Sales and the FirstName of Employees with a title that starts with Vice where the result is greater than 6 characters long.")]
        public void ObjectServicesSet3() {
            var query1 = context.Employees.Where("it.Title LIKE 'Sales%'").Select("it.LastName");
            var query2 = context.Employees.Where("it.Title LIKE 'Vice%'").Select("it.FirstName");

            // The result column is named LastName, as that is the first column that we selected
            var query = query1.Union(query2).Where("EDM.Length(it.LastName) > 6");

            ObjectDumper.Write(query, 2);
        }

        //[Category("Set operations")]
        //[Title("Intersect - Entity Type")]
        //[Description("This sample shows the intersection of Products with a UnitPrice of less than 10 and Chocolate in the ProductName.")]
        //public void ObjectServicesSet4()
        //{
        //    var query1 = context.Products.Where("it.UnitPrice < 10");
        //    var query2 = context.Products.Where("it.ProductName LIKE '%Chocolate%'");

        //    var query = query1.Intersect(query2);

        //    ObjectDumper.Write(query);
        //}

        //[Category("Set operations")]
        //[Title("Intersect - Primitive Type")]
        //[Description("This sample shows the intersection of ProductIDs with a UnitPrice of less than 10 and Chocolate in the ProductName.")]
        //public void ObjectServicesSet5()
        //{
        //    var query1 = context.Products.Where("it.UnitPrice < 10").Select("it.ProductID");
        //    var query2 = context.Products.Where("it.ProductName LIKE '%Chocolate%'").Select("it.ProductID"); ;

        //    var query = query1.Intersect(query2);

        //    ObjectDumper.Write(query);
        //}

        //[Category("Set operations")]
        //[Title("Except")]
        //[Description("This sample shows the all Products Except for Products with a UnitPrice of less than 10.")]
        //public void ObjectServicesSet6()
        //{

        //    var query1 = context.Products;
        //    var query2 = context.Products.Where("it.UnitPrice < 10");

        //    var query = query1.Except(query2); 

        //    ObjectDumper.Write(query);
        //}

        //[Category("Set operations")]
        //[Title("Exists")]
        //[Description("This sample shows if a Product exists with a UnitPrice of less than 10. Use the LINQ Any operator.")]
        //public void ObjectServicesSet7()
        //{
        //    var query = context.Products.Where("it.UnitPrice < 10").Any();

        //    ObjectDumper.Write(query);
        //}

        [Category("Set operations")]
        [Title("Union All")]
        [Description("This sample shows the union all of all Products with a UnitPrice of less than 10 or less than 20 UnitsInStock.")]
        public void ObjectServicesSet8() {
            var query1 = context.Products.Where("it.UnitPrice < 10");
            var query2 = context.Products.Where("it.UnitsInStock < 20");

            var query = query1.UnionAll(query2);

            ObjectDumper.Write(query);
        }

        #endregion

        #region Ordering operations

        [Category("Ordering operations")]
        [Title("OrderBy - Simple Ascending")]
        [Description("This sample shows all Products ordered by the ProductName.")]
        public void ObjectServicesOrdering1() {
            var query = context.Products.OrderBy("it.ProductName");

            ObjectDumper.Write(query);
        }

        [Category("Ordering operations")]
        [Title("OrderBy - Simple Descending")]
        [Description("This sample shows all Products ordered by the ProductName in descending order.")]
        public void ObjectServicesOrdering2() {
            var query = context.Products.OrderBy("it.ProductName DESC");

            ObjectDumper.Write(query);
        }

        [Category("Ordering operations")]
        [Title("OrderBy - Related")]
        [Description("This sample shows all Products ordered by their CategoryName.")]
        public void ObjectServicesOrdering3() {
            var query = context.Products.OrderBy("it.Category.CategoryName");

            ObjectDumper.Write(query);
        }

        [Category("Ordering operations")]
        [Title("OrderBy - Multiple")]
        [Description("This sample shows all Products ordered by the Category and then by ProductName.")]
        public void ObjectServicesOrdering4() {
            var query = context.Products.OrderBy("it.Category.CategoryName, it.ProductName");

            ObjectDumper.Write(query);
        }

        [Category("Ordering operations")]
        [Title("OrderBy - Function")]
        [Description("This sample shows all Products ordered by the first letter of the ProductName.")]
        public void ObjectServicesOrdering5() {
            var query = context.Products.OrderBy("Left(it.ProductName,1) desc");

            ObjectDumper.Write(query);
        }

        [Category("Ordering operations")]
        [Title("OrderBy - Parameters")]
        [Description("This sample shows all Products ordered by the UnitPrice modulus 3.")]
        public void ObjectServicesOrdering6() {
            var query = context.Products.OrderBy("it.UnitPrice % @col", new ObjectParameter("col", 3));

            ObjectDumper.Write(query);
        }


        #endregion

        #region Paging

        [Category("Paging operations")]
        [Title("Skip")]
        [Description("This sample shows all Categories with the first two skipped.")]
        public void ObjectServicesPaging1() {
            var query = context.Categories.Skip("it.CategoryName", "2");

            ObjectDumper.Write(query);
        }

        [Category("Paging operations")]
        [Title("Skip with Parameter")]
        [Description("This sample shows all Products, with the first 5 skipped.")]
        public void ObjectServicesPaging2() {
            int skipNumber = 5;
            var query = context.Categories.Skip("it.CategoryName", "@count", new ObjectParameter("count", skipNumber));

            ObjectDumper.Write(query);
        }

        [Category("Paging operations")]
        [Title("Top")]
        [Description("This sample shows the first 10 Products.")]
        public void ObjectServicesPaging3() {
            var query = context.Categories.OrderBy("it.CategoryID").Top("10");

            ObjectDumper.Write(query);
        }

        [Category("Paging operations")]
        [Title("Top with Parameter")]
        [Description("This sample shows the first 10 Products.")]
        public void ObjectServicesPaging4() {
            int topNumber = 10;
            var query = context.Categories.OrderBy("it.CategoryID").Top("@count", new ObjectParameter("count", topNumber));

            ObjectDumper.Write(query);
        }

        #endregion

        #region Misc

        [Category("Misc")]
        [Title("First")]
        [Description("This sample shows the first Category.")]
        public void ObjectServicesMisc1() {
            var query = context.Categories.OrderBy("it.CategoryID").First();

            ObjectDumper.Write(query);
        }

        [Category("Misc")]
        [Title("FirstOrDefault")]
        [Description("This sample gets the first or default of an empty set using the LINQ operator.")]
        public void ObjectServicesMisc2() {
            ObjectDumper.Write(context.Categories.Where("it.CategoryName = 'none'").OrderBy("it.CategoryID").FirstOrDefault());
        }

        [Category("Misc")]
        [Title("ToList")]
        [Description("This sample gets all Employees as an IList.")]
        public void ObjectServicesMisc3() {
            var query = context.Employees.ToList();

            ObjectDumper.Write(query);
        }

        [Category("Misc")]
        [Title("GetResultType")]
        [Description("This sample gets the result type of a query.")]
        public void ObjectServicesMisc4() {
            if (context.Connection.State != System.Data.ConnectionState.Open) context.Connection.Open(); //workaround
            var query = context.Employees.GetResultType();

            ObjectDumper.Write(query, 3, 6);
        }

        [Category("Misc")]
        [Title("Name")]
        [Description("This sample gets the name of the local name of the current result set.")]
        public void ObjectServicesMisc5() {
            var query = context.Employees.Name;

            ObjectDumper.Write(query);
        }

        [Category("Misc")]
        [Title("OfType")]
        [Description("This sample gets all the PreviousEmployees.")]
        public void ObjectServicesMisc6() {
            var query = context.Employees.OfType<PreviousEmployee>();

            ObjectDumper.Write(query);
        }

        [Category("Misc")]
        [Title("Constants - Numeric")]
        [Description("This sample selects numeric constant values.")]
        public void ObjectServicesConstantsNumeric() {
            ObjectDumper.Write(context.Employees.OrderBy(x => x.EmployeeID).Select(c => new {
                IntValue = (int)1,
                ShortValue = (short)2,
                LongValue = (long)3,
                ByteValue = (byte)4,
                TrueValue = true,
                FalseValue = false,
                SingleValue = (float)3.14159265358979323846,
                DoubleValue = (double)3.14159265358979323846,
            }).Take(1));
        }

        //[Category("Misc")]
        //[Title("Constants - Binary & String")]
        //[Description("This sample selects binary constant values.")]
        //public void ObjectServicesConstantsBinaryAndString()
        //{
        //    ObjectDumper.Write(context.Employees.Select("'foo',N'bar',BINARY'CAFE0123456789ABCDEFFEDCBA9876543210CAFE'").Top("1"));
        //}

        [Category("Misc")]
        [Title("Constants - Boolean")]
        [Description("This sample selects binary constant values.")]
        public void ObjectServicesConstantsBoolean() {
            ObjectDumper.Write(context.Employees.Select("true,false"));
        }

        //[Category("Misc")]
        //[Title("Constants - Time")]
        //[Description("This sample selects Time constant values.")]
        //public void ObjectServicesConstantTime()
        //{
        //    ObjectDumper.Write(context.Employees.Select("TIME'23:59:59'").Top("1"));
        //}

        [Category("Misc")]
        [Title("Constants - DateTime")]
        [Description("This sample selects DateTime constant values.")]
        public void ObjectServicesConstantDateTime() {
            ObjectDumper.Write(context.Employees.Select("DATETIME'2008-01-01 23:59:59'"));
        }

        //[Category("Misc")]
        //[Title("Constants - DateTimeOffset")]
        //[Description("This sample selects DateTimeOffset constant values.")]
        //public void ObjectServicesConstantsDateTimeOffset()
        //{
        //    ObjectDumper.Write(context.Employees.Select("DATETIMEOFFSET'2008-01-01 23:59:59+11:30'").Top("1"));
        //}

        #endregion

        #region Grouping

        [Category("Grouping")]
        [Title("Count")]
        [Description("This sample gets the count of all Employees, grouped by HireDate.")]
        public void ObjectServicesGrouping1() {
            var query = context.Employees.GroupBy("it.HireDate", "count(it.EmployeeID) AS Count");

            ObjectDumper.Write(query);
        }

        [Category("Grouping")]
        [Title("Min with Relationship")]
        [Description("This sample gets Minimum of all UnitsInStock for each Category, and display the number and the CategoryName.")]
        public void ObjectServicesGrouping2() {
            var query = context.Products.GroupBy("it.Category.CategoryName", "Min(it.UnitsInStock) AS MinStock, it.Category.CategoryName");

            ObjectDumper.Write(query);
        }

        [Category("Grouping")]
        [Title("Min Composite")]
        [Description("This sample gets smallest order line total from each order.")]
        public void ObjectServicesGrouping3() {
            var query = context.OrderDetails.GroupBy("it.OrderID", "it.OrderID, Min((it.UnitPrice * it.Quantity)) AS MinOrderItem");

            ObjectDumper.Write(query);
        }

        [Category("Grouping")]
        [Title("Average")]
        [Description("This sample gets average of order lines total from each order.")]
        public void ObjectServicesGrouping4() {
            var query = context.OrderDetails.GroupBy("it.OrderID", "it.OrderID, Avg((it.UnitPrice * it.Quantity)) AS AvgOrderItem");

            ObjectDumper.Write(query);
        }
        #endregion

        #region Include

        [Category("Include")]
        [Title("Single")]
        [Description("This sample loads all OrderDetails with the Order, and loads the top Order.")]
        public void ObjectServicesInclude1() {
            var query = context.Orders.Include("OrderDetails").OrderBy("it.OrderID").Top("1");

            ObjectDumper.Write(query, 2);
        }

        #endregion

        #region Object Context

        [Category("Object Context")]
        [Title("Loading Options - NoTracking")]
        [Description("This sample loads the top 5 Orders, and does not track them.")]
        public void ObjectServicesOC1() {
            var query = context.Orders.OrderBy("it.OrderID").Top("5").Execute(MergeOption.NoTracking);

            // we update the OrderDate
            foreach (Order o in query) {
                o.OrderDate = DateTime.Now;

                try {
                    // check for the State Entry for each of the objects
                    var state = context.ObjectStateManager.GetObjectStateEntry(o.EntityKey);
                    ObjectDumper.Write(state);
                }
                catch (Exception ex) {
                    // we don't have a State Entry for the Order, because we loaded with NoTracking
                    ObjectDumper.Write(ex);
                }
            }
        }

        [Category("Object Context")]
        [Title("Loading Options - AppendOnly")]
        [Description("This sample loads the top 5 Orders into the Object Context and updates them locally. Another query is run with the AppendOnly option, which preserves local client values.")]
        public void ObjectServicesOC2() {
            var startQuery = context.Orders.OrderBy("it.OrderID").Top("5");
            // we update the OrderDate
            foreach (Order o in startQuery) {
                o.OrderDate = DateTime.Now;
            }

            var query = context.Orders.OrderBy("it.OrderID").Top("5").Execute(MergeOption.AppendOnly);
            // note that the OrderDate is still the client updated value
            ObjectDumper.Write(query, 1);
        }

        [Category("Object Context")]
        [Title("Loading Options - OverwriteChanges")]
        [Description("This sample loads the top 5 Orders into the Object Context and updates them locally. Another query is run with the OverwriteChanges option, which overwrites the local client values.")]
        public void ObjectServicesOC3() {
            var startQuery = context.Orders.OrderBy("it.OrderID").Top("5");
            // we update the OrderDate
            foreach (Order o in startQuery) {
                o.OrderDate = DateTime.Now;
            }

            var query = context.Orders.OrderBy("it.OrderID").Top("5").Execute(MergeOption.OverwriteChanges);
            // note that the OrderDate is now the server value
            ObjectDumper.Write(query, 1);
        }

        [Category("Object Context")]
        [Title("Loading Options - PreserveChanges")]
        [Description("This sample loads the top 5 Orders into the Object Context and updates them locally. Another query is run with the PreserveChanges option, which keeps the local client values.")]
        public void ObjectServicesOC4() {
            var startQuery = context.Orders.OrderBy("it.OrderID").Top("5");
            // we update the OrderDate
            foreach (Order o in startQuery) {
                o.OrderDate = DateTime.Now;
            }

            var query = context.Orders.OrderBy("it.OrderID").Top("5").Execute(MergeOption.PreserveChanges);
            // note that the OrderDate is still the client updated value
            ObjectDumper.Write(query, 1);
        }

        #endregion
    }
}
