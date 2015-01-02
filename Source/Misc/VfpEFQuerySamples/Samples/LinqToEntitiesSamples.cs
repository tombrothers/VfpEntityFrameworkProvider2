using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using NorthwindEFModel;
using SampleQueries.Harness;
using VfpEntityFrameworkProvider;

namespace SampleQueries.Samples {
    [Title("Linq To Entities")]
    [Prefix("LinqToEntities")]
    class LinqToEntitiesSamples : NorthwindBasedSample {

        #region Restriction Operators

        [Category("Restriction Operators")]
        [Title("Where - Simple 1")]
        [Description("This sample uses WHERE to find all customers whose contact title is Sales Representative.")]
        public void LinqToEntities1() {
            var query = from cust in context.Customers
                        where cust.ContactTitle == "Sales Representative"
                        select cust;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Where - Simple 2")]
        [Description("This sample uses WHERE to find all orders placed before 1997.")]
        public void LinqToEntities2() {
            DateTime dt = new DateTime(1997, 1, 1);
            var query = from order in context.Orders
                        where order.OrderDate < dt
                        select order;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Where - Simple 3")]
        [Description("This sample uses WHERE to filter for Products that have stock below their reorder level and have a units on order of zero.")]
        public void LinqToEntities3() {
            var query = from p in context.Products
                        where p.UnitsInStock < p.ReorderLevel && p.UnitsOnOrder == 0
                        select p;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Where - Simple 4")]
        [Description("This sample uses WHERE to filter out Products that have a UnitPrice less than 10.")]
        public void LinqToEntities4() {
            var query = from p in context.Products
                        where p.UnitPrice < 10
                        select p;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Where - Complex Type")]
        [Description("This sample uses WHERE to find Employees in London.")]
        public void LinqToEntities4a() {
            var query = from e in context.Employees
                        where e.Address.City == "London"
                        select e;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Where - TypeOf")]
        [Description("This sample uses WHERE to get previous employees.")]
        public void LinqToEntities5() {
            var query = from e in context.Employees
                        where e is NorthwindEFModel.PreviousEmployee
                        select e;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Any")]
        [Description("This sample uses WHERE to get employees who handle the Boston territory.")]
        public void LinqToEntities6() {
            var query = from e in context.Employees
                        where e.Territories.Any(t => t.TerritoryDescription == "Boston")
                        select e;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Any - 2")]
        [Description("This sample uses any Customers who placed an order in 1997.")]
        public void LinqToEntities7() {
            var query = from c in context.Customers
                        where c.Orders.Any(o => o.OrderDate.HasValue == true && o.OrderDate.Value.Year == 1997)
                        select c;

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Any - 3")]
        [Description("This sample uses ANY to check for any out-of-stock products.")]
        public void LinqToEntities8() {
            var query = context
                .Suppliers
                .Where(s => s.Products
                           .Any(p => p.UnitsInStock == 0))
                .Select(s => s);

            ObjectDumper.Write(query);
        }

        [Category("Restriction Operators")]
        [Title("Any - Related Entities")]
        [Description("This sample uses WHERE and ANY to get orders containing a product with a unit on order.")]
        public void LinqToEntities9() {
            var query = from o in context.Orders
                        where o.OrderDetails.Any(od => od.Product.UnitsOnOrder > 0)
                        select o;

            ObjectDumper.Write(query);
        }

        //[Category("Restriction Operators")]
        //[Title("Count - Related Entities Aggregate")]
        //[Description("This sample uses COUNT to get Products sold to Customers in the same Country as the Products Suppliers, and where all the Products in the order were from the same Country.")]
        //public void LinqToEntities10()
        //{
        //    var query = from p in context.Products
        //                where p.OrderDetails.Count(od => od.Order.Customer.Address.Country == p.Supplier.Address.Country) > 2
        //                select p;

        //    ObjectDumper.Write(query);
        //}

        #endregion

        #region Projection Operators

        [Category("Projection Operators")]
        [Title("Select - Simple 1")]
        [Description("This samples uses SELECT to get all Customers as Entity Objects.")]
        public void LinqToEntities11() {
            var query = from c in context.Customers
                        select c;

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("Select - Simple 2")]
        [Description("This samples uses SELECT to get all Customer Contact Names as Strings.")]
        public void LinqToEntities12() {
            var query = from c in context.Customers
                        select c.ContactName;

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("Select - Anonymous 1")]
        [Description("This samples uses SELECT to get all Customer Contact Names as an anonoymous type.")]
        public void LinqToEntities13() {
            var query = from c in context.Customers
                        select new { c.ContactName };

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("Select - Anonymous 2")]
        [Description("This sample uses SELECT to get Orders as anonymous type")]
        public void LinqToEntities14() {
            var query = from o in context.Orders
                        where o.Customer.Address.City == "London"
                        select new { o };

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("Select - Anonymous 3")]
        [Description("This sample uses SELECT to get all Orders and associated Customers as anonymous type")]
        public void LinqToEntities15() {
            var query = from o in context.Orders
                        where o.Customer.Address.City == "London"
                        select new { o, o.Customer };

            ObjectDumper.Write(query, 1, 3);
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Simple 1")]
        [Description("This sample uses SELECTMANY to get all Orders for a Customer as a flat result")]
        public void LinqToEntities16() {
            var query = from c in context.Customers
                        where c.CustomerID == "ALFKI"
                        from o in c.Orders
                        select o;

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Simple 2")]
        [Description("This sample uses SELECTMANY to get all Orders for a Customer as a flat result as a method query")]
        public void LinqToEntities17() {
            var query = context.Customers.Where(cust => cust.CustomerID == "ALFKI")
                .SelectMany(cust => cust.Orders);

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Simple 3")]
        [Description("This sample uses SELECTMANY to get all Orders for Customers in Denmark as a flat result")]
        public void LinqToEntities18() {
            var query = from c in context.Customers
                        where c.Address.Country == "Denmark"
                        from o in c.Orders
                        select o;

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Simple 4")]
        [Description("This sample uses SELECTMANY to get all Orders for Customers in Denmark as a flat result as a method query")]
        public void LinqToEntities19() {
            var query = context.Customers.Where(cust => cust.Address.Country == "Denmark")
                .SelectMany(cust => cust.Orders);

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Predicate 1")]
        [Description("This sample uses SELECTMANY to get all Orders for Customers in Denmark as a flat result")]
        public void LinqToEntities20() {
            var query = from c in context.Customers
                        where c.Address.Country == "Denmark"
                        from o in c.Orders
                        where o.Freight > 5
                        select o;

            ObjectDumper.Write(query);
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Predicate 2")]
        [Description("This sample uses SELECTMANY to get all Orders for Customers in Denmark as an anonymous type containing the Orders and Customer flat result")]
        public void LinqToEntities21() {
            var query = from c in context.Customers
                        where c.Address.Country == "Denmark"
                        from o in c.Orders
                        where o.Freight > 5
                        select new { c, o };

            ObjectDumper.Write(query, 1);
        }

        [Category("Projection Operators")]
        [Title("SelectMany - Predicate 3")]
        [Description("This sample uses SELECTMANY to get all Orders for Customers in Denmark as a flat result using LINQ opeartors")]
        public void LinqToEntities22() {
            var query = context.Customers.Where(cust => cust.Address.Country == "Denmark")
                .SelectMany(cust => cust.Orders.Where(o => o.Freight > 5));

            ObjectDumper.Write(query);
        }

        #endregion

        #region Aggregate Operators

        [Category("Aggregate Operators")]
        [Title("Count - Simple")]
        [Description("This sample uses COUNT to get the number of Orders.")]
        public void LinqToEntities23() {
            var query = context.Orders.Count();

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Count - Predicate 1")]
        [Description("This sample uses COUNT to get the number of Orders placed by Customers in Mexico.")]
        public void LinqToEntities24() {
            var query = context.Orders.Where(o => o.Customer.Address.Country == "Mexico").Count();

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Count - Predicate 2")]
        [Description("This sample uses COUNT to get the number of Orders shipped to Mexico.")]
        public void LinqToEntities25() {
            var query = context.Orders
                .Where(o => o.ShipCountry == "Mexico").Count();

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Sum - Simple 1")]
        [Description("This sample uses SUM to find the total freight over all Orders.")]
        public void LinqToEntities26() {
            var query = context.Orders.Select(o => o.Freight).Sum();

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Sum - Simple 2")]
        [Description("This sample uses SUM to find the total number of units on order over all Products.")]
        public void LinqToEntities27() {
            var query = context.Products.Sum(p => p.UnitsOnOrder);

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Sum - Simple 3")]
        [Description("This sample uses SUM to find the total number of units on order over all Products out-of-stock.")]
        public void LinqToEntities28() {
            var query = context.Products.Where(p => p.UnitsInStock == 0).Sum(p => p.UnitsOnOrder);

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Simple 1")]
        [Description("This sample uses MIN to find the lowest unit price of any Product.")]
        public void LinqToEntities29() {
            var query = context.Products.Select(p => p.UnitPrice).Min();

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Simple 2")]
        [Description("This sample uses MIN to find the lowest freight of any Order.")]
        public void LinqToEntities30() {
            var query = context.Orders.Min(o => o.Freight);

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Predicate")]
        [Description("This sample uses MIN to find the lowest freight of any Order shipped to Mexico.")]
        public void LinqToEntities31() {
            var query = context.Orders.Where(o => o.ShipCountry == "Mexico").Min(o => o.Freight);

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Min - Grouping")]
        [Description("This sample uses Min to find the Products that have the lowest unit price in each category, and returns the result as an anonoymous type.")]
        public void LinqToEntities32() {
            var query = from p in context.Products
                        group p by p.Category.CategoryID into g
                        select new {
                            CategoryID = g.Key,
                            CheapestProducts =
                                from p2 in g
                                where p2.UnitPrice == g.Min(p3 => p3.UnitPrice)
                                select p2
                        };

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Simple 1")]
        [Description("This sample uses MAX to find the latest hire date of any Employee.")]
        public void LinqToEntities33() {
            var query = context.Employees.Select(e => e.HireDate).Max();

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Simple 2")]
        [Description("This sample uses MAX to find the most units in stock of any Product.")]
        public void LinqToEntities34() {
            var query = context.Products.Max(p => p.UnitsInStock);

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Predicate")]
        [Description("This sample uses MAX to find the most units in stock of any Product with CategoryID = 1.")]
        public void LinqToEntities35() {
            var query = context.Products.Where(p => p.Category.CategoryID == 1).Max(p => p.UnitsInStock);
            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Max - Grouping")]
        [Description("This sample uses MAX to find the Products that have the highest unit price in each category, and returns the result as an anonoymous type.")]
        public void LinqToEntities36() {
            var query = from p in context.Products
                        group p by p.Category.CategoryID into g
                        select new {
                            g.Key,
                            MostExpensiveProducts =
                                from p2 in g
                                where p2.UnitPrice == g.Max(p3 => p3.UnitPrice)
                                select p2
                        };

            ObjectDumper.Write(query, 1);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Simple 1")]
        [Description("This sample uses AVERAGE to find the average freight of all Orders.")]
        public void LinqToEntities37() {
            var query = context.Orders.Select(o => o.Freight).Average();

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Simple 2")]
        [Description("This sample uses AVERAGE to find the average unit price of all Products.")]
        public void LinqToEntities38() {
            var query = context.Products.Average(p => p.UnitPrice);

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Predicate")]
        [Description("This sample uses AVERAGE to find the average unit price of all Products with CategoryID = 1.")]
        public void LinqToEntities39() {
            var query = context.Products.Where(p => p.Category.CategoryID == 1).Average(p => p.UnitPrice);

            ObjectDumper.Write(query);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Grouping 1")]
        [Description("This sample uses AVERAGE to find the Products that have unit price higher than the average unit price of the category for each category.")]
        public void LinqToEntities40() {
            var query = from p in context.Products
                        group p by p.Category.CategoryID into g
                        select new {
                            g.Key,
                            ExpensiveProducts =
                                from p2 in g
                                where p2.UnitPrice > g.Average(p3 => p3.UnitPrice)
                                select p2
                        };

            ObjectDumper.Write(query, 2);
        }

        [Category("Aggregate Operators")]
        [Title("Average - Grouping 2")]
        [Description("This sample uses AVERAGE to find the average unit price of each category.")]
        public void LinqToEntities41() {
            var query = from p in context.Products
                        group p by p.Category.CategoryID into g
                        select new {
                            g.Key,
                            Average = g.Average(p => p.UnitPrice)
                        };

            ObjectDumper.Write(query, 1);
        }

        #endregion

        #region Set Operators

        [Category("Set Operators")]
        [Title("First - Simple")]
        [Description("This sample uses FIRST and WHERE to get the first (database order) order that is shipped to Seattle. The WHERE predicate is evaluated on the server.")]
        public void LinqToEntities42() {
            var query = from o in context.Orders
                        where o.ShipCity == "Seattle"
                        orderby o.OrderID
                        select o;

            var result = query.First();

            ObjectDumper.Write(result);
        }

        [Category("Set Operators")]
        [Title("First - Predicate")]
        [Description("This sample uses FIRST to get the first (database order) order that is shipped to Seattle. The predicate is evaluated on the client.")]
        public void LinqToEntities43() {
            var query = from o in context.Orders
                        orderby o.OrderID
                        select o;

            var result = query
                .First(x => x.ShipCity == "Seattle");

            ObjectDumper.Write(result);
        }

        [Category("Set Operators")]
        [Title("First - Ordered")]
        [Description("This sample uses FIRST, WHERE and ORDER BY to get the first order that is shipped to Seattle, ordered by date. The predicate is evaluated on the server.")]
        public void LinqToEntities44() {
            var query = from o in context.Orders
                        where o.ShipCity == "Seattle"
                        orderby o.OrderDate
                        select o;

            var result = query.First();

            ObjectDumper.Write(result);
        }

        [Category("Set Operators")]
        [Title("Distinct - Simple")]
        [Description("This sample uses DISTINCT to get all the categories of products.")]
        public void LinqToEntities45() {
            var query = context.Products.Select(o => o.Category.CategoryID).Distinct();

            ObjectDumper.Write(query);
        }

        [Category("Set Operators")]
        [Title("Union - Simple")]
        [Description("This sample uses UNION to get all the orders where the shipping country was Mexico or Canada.")]
        public void LinqToEntities46() {
            var mexico = context.Orders.Where(o => o.ShipCountry == "Mexico").Select(o => o);
            var canada = context.Orders.Where(o => o.ShipCountry == "Canada").Select(o => o);
            var query = mexico.Union(canada);

            ObjectDumper.Write(query);
        }

        [Category("Set Operators")]
        [Title("Union - With Distinct")]
        [Description("This sample uses UNION and DISTINCT to get all the Customers from orders where the shipping country was Mexico or Canada.")]
        public void LinqToEntities47() {
            var mexico = context.Orders.Where(o => o.ShipCountry == "Mexico").Select(o => o);
            var canada = context.Orders.Where(o => o.ShipCountry == "Canada").Select(o => o);
            var union = mexico.Union(canada).Select(o => o.Customer);

            var query = union.Distinct();

            ObjectDumper.Write(query, 1);
        }

        [Category("Set Operators")]
        [Title("Concat - Simple")]
        [Description("This sample uses CONCAT to get all orders where the shipping country was Mexico or Canada.")]
        public void LinqToEntities48() {
            var mexico = context.Orders.Where(o => o.ShipCountry == "Mexico").Select(o => o);
            var canada = context.Orders.Where(o => o.ShipCountry == "Canada").Select(o => o);

            var query = mexico.Concat(canada);

            ObjectDumper.Write(query);
        }

        //[Category("Set Operators")]
        //[Title("Intersect - Simple 1")]
        //[Description("This sample uses INTERSECT to get common products where an order was shipped to Mexico or Canada.")]
        //public void LinqToEntities49()
        //{
        //    var mexico = context.OrderDetails.Where(od => od.Order.ShipCountry == "Mexico").Select(od => od.Product);
        //    var canada = context.OrderDetails.Where(od => od.Order.ShipCountry == "Canada").Select(od => od.Product);

        //    var query = mexico.Intersect(canada);

        //    ObjectDumper.Write(query);
        //}

        //[Category("Set Operators")]
        //[Title("Intersect - Simple 2")]
        //[Description("This sample uses INTERSECT to get common products where an order was shipped to Mexico or USA in one consolidated query.")]
        //public void LinqToEntities50()
        //{
        //    var query = context.OrderDetails.Where(od => od.Order.ShipCountry == "Mexico").Select(od => od.Product).Intersect(context.OrderDetails.Where(od => od.Order.ShipCountry == "USA").Select(o => o.Product));

        //    ObjectDumper.Write(query);
        //}

        //[Category("Set Operators")]
        //[Title("Except - Simple 1")]
        //[Description("This sample uses EXCEPT to get customers who shipped orders to Mexico but not Canada.")]
        //public void LinqToEntities51()
        //{
        //    var query = context.Orders.Where(o => o.ShipCountry == "Mexico").Select(o => o.Customer).Except(context.Orders.Where(o => o.ShipCountry == "Canada").Select(o => o.Customer));

        //    ObjectDumper.Write(query);
        //}

        //[Category("Set Operators")]
        //[Title("Except - Simple 2")]
        //[Description("This sample uses EXCEPT to get customers with no orders sent to Mexico.")]
        //public void LinqToEntities52()
        //{
        //    var query = context.Customers.Select(e => e)
        //        .Except(context.Orders.Where(o => o.ShipCountry == "Mexico").Select(o => o.Customer));

        //    ObjectDumper.Write(query);
        //}

        [Category("Set Operators")]
        [Title("Contains method")]
        [Description("This sample uses the LINQ Contains method that operates on a sequence. The first version of the Entity Framework did not support this method.")]
        public void LinqToEntitiesContains() {
            string[] cities = new string[] { "New York", "London", "Seattle" };

            var query = context.Orders.Where(c => cities.Contains(c.Customer.Address.City)).Select(o => o);

            ObjectDumper.Write(query);
        }

        #endregion

        #region Ordering and Grouping

        [Category("Ordering and Grouping")]
        [Title("OrderBy - Simple 1")]
        [Description("Select all customers ordered by ContactName.")]
        public void LinqToEntities53() {
            var query = from c in context.Customers
                        orderby c.ContactName
                        select c;

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderBy - Simple 2")]
        [Description("Select all customers ordered by ContactName descending.")]
        public void LinqToEntities54() {
            var query = from c in context.Customers
                        orderby c.CompanyName descending
                        select c;

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderBy - Simple 3")]
        [Description("Select an anonoymous type with all product IDs ordered by UnitsInStock.")]
        public void LinqToEntities55() {
            var query = from p in context.Products
                        orderby p.UnitsInStock
                        select new { p.ProductID, p.UnitsInStock };

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderBy - Simple 4")]
        [Description("Select an anonoymous type with all product IDs ordered by UnitsInStock as a method query.")]
        public void LinqToEntities56() {
            var query = context.Products.OrderBy(p => p.UnitsInStock)
                .Select(p2 => new { p2.ProductID, p2.UnitsInStock });

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderByDescending - Simple 1")]
        [Description("Select all customers ordered by the descending region.")]
        public void LinqToEntities57() {
            var query = from c in context.Customers
                        orderby c.Address.Region descending
                        select c;

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderByDescending - Simple 2")]
        [Description("Select all customers ordered by the descending region as a method query.")]
        public void LinqToEntities58() {
            var query = context.Customers.Select(c => c).OrderByDescending(c2 => c2.Address.Region);

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderBy with ThenBy")]
        [Description("Select all customers ordered by the region, then the contact name.")]
        public void LinqToEntities59() {
            var query = context.Customers.Select(c => c).OrderBy(c => c.Address.Region).ThenBy(c => c.ContactName);

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderByDescending with ThenBy")]
        [Description("Select all customers ordered by the region in descending order, then the contact name.")]
        public void LinqToEntities60() {
            var query = context.Customers.Select(c => c).OrderByDescending(c => c.Address.Region).ThenBy(c => c.ContactName);

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderBy with ThenByDescending")]
        [Description("Select all customers ordered by the region then the contact name in descending order.")]
        public void LinqToEntities61() {
            var query = context.Customers.Select(c => c).OrderBy(c => c.Address.Region).ThenByDescending(c => c.ContactName);

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderByDescending - Simple 3")]
        [Description("Select all products ordered by the descending unit price.")]
        public void LinqToEntities62() {
            var query = from p in context.Products
                        orderby p.UnitPrice descending
                        select p;

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("OrderBy - FK Collection")]
        [Description("Select all orders for a customer ordered by date that the order was placed.")]
        public void LinqToEntities63() {
            var query = context.Customers.Where(cust => cust.CustomerID == "ALFKI")
                .SelectMany(c => c.Orders.Select(o => o))
                .OrderBy(o2 => o2.OrderDate);

            foreach (var order in query) {
                ObjectDumper.Write(order);
            }
        }

        [Category("Ordering and Grouping")]
        [Title("Grouping - Simple 1")]
        [Description("Select all Regions with a customer.")]
        public void LinqToEntities64() {
            var query = from c in context.Customers
                        group c by c.Address.Region into regions
                        select new { regions.Key };

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("Grouping - Simple 2")]
        [Description("Select all dates with orders placed.")]
        public void LinqToEntities65() {
            var query = from o in context.Orders
                        group o by o.OrderDate into dates
                        select new { dates.Key };

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("Grouping - Join 1")]
        [Description("Select all Regions and customer count for each region.")]
        public void LinqToEntities66() {
            var query = from c in context.Customers
                        group c by c.Address.Region into regions
                        select new { region = regions.Key, count = regions.Count() };

            ObjectDumper.Write(query);
        }

        [Category("Ordering and Grouping")]
        [Title("Grouping - Join 2")]
        [Description("Select all Regions and customer count for each region as a method query.")]
        public void LinqToEntities67() {
            var query = context.Customers.GroupBy(c => c.Address.Region).Select(r => new { region = r.Key, count = r.Count() });

            ObjectDumper.Write(query);
        }

        //[Category("Ordering and Grouping")]
        //[Title("Grouping with a join on Key 1")]
        //[Description("Select all Customer Regions with the total Freight on all orders for Customers in that Region.")]
        //public void LinqToEntities68()
        //{
        //    var query = from c in context.Customers
        //                group c by c.Address.Region into regions
        //                join c2 in context.Customers on regions.Key equals c2.Address.Region
        //                select new { region = regions.Key, total = c2.Orders.Sum(o => o.Freight) };

        //    ObjectDumper.Write(query);
        //}

        //[Category("Ordering and Grouping")]
        //[Title("Grouping with a join on Key 2")]
        //[Description("Select all Customer Regions with the total Freight on all orders for Customers in that Region as a method query.")]
        //public void LinqToEntities69()
        //{
        //    var query = context.Customers.GroupBy(c => c.Address.Region)
        //        .Select(g => new
        //                     {
        //                         Region = g.Key,
        //                         FreightTotal = g
        //                             .SelectMany(c2 => c2.Orders)
        //                             .Sum(o => o.Freight)
        //                     });

        //    ObjectDumper.Write(query);
        //}

        [Category("Ordering and Grouping")]
        [Title("OrderBy lifting")]
        [Description("Select the least expensive product with a name that starts with C. In the first version of the Entity Framework, the OrderBy operation had to be the last call in the query to produce defined results. The call to Take on an undefined ordering would have thrown an exception.")]
        public void LinqToEntitiesOrderByLifting() {
            var query = context.Products.OrderBy(p => p.UnitPrice).Where(p => p.ProductName.StartsWith("c")).Take(1);

            ObjectDumper.Write(query);
        }

        #endregion

        #region Relationship Navigation

        [Category("Relationship Navigation")]
        [Title("Relationship Collection 1")]
        [Description("Select a sequence of all the orders for a customer using Select.")]
        public void LinqToEntities70() {
            var query = context.Customers.Where(cust => cust.CustomerID == "ALFKI")
                .Select(c => c.Orders.Select(o => o));

            foreach (var order in query) {
                ObjectDumper.Write(order);
            }
        }

        [Category("Relationship Navigation")]
        [Title("Relationship Collection 2")]
        [Description("Select all the orders for a customer using SelectMany.")]
        public void LinqToEntities71() {
            var query = context.Customers.Where(cust => cust.CustomerID == "ALFKI").SelectMany(c => c.Orders);

            ObjectDumper.Write(query);
        }


        [Category("Relationship Navigation")]
        [Title("Relationship Collection 3")]
        [Description("Select number of orders placed in 1998 for a customer.")]
        public void LinqToEntities74() {
            var query = context.Customers
                .Where(cust => cust.CustomerID == "ALFKI")
                .SelectMany(c => c.Orders)
                .Where(o => o.OrderDate.HasValue == true && o.OrderDate.Value.Year == 1998);

            ObjectDumper.Write(query);
        }

        //[Category("Relationship Navigation")]
        //[Title("Relationship Collection Aggregate")]
        //[Description("Select a customer and the sum of the freight of thier orders.")]
        //public void LinqToEntities73()
        //{
        //    var query = context.Customers.Where(cust => cust.CustomerID == "ALFKI")
        //        .Select(c => c.Orders.Sum(o => o.Freight));

        //    ObjectDumper.Write(query);
        //}

        [Category("Relationship Navigation")]
        [Title("Relationship Collection with Predicate")]
        [Description("Select customers with an order where the shipping address is the same as the customers.")]
        public void LinqToEntities75() {
            var query = context.Customers.Where(cust => cust.Orders.Any(o => o.ShipAddress == cust.Address.Address)).Select(c2 => c2);

            ObjectDumper.Write(query);
        }

        //[Category("Relationship Navigation")]
        //[Title("Relationship Collection with Grouping and Aggregate")]
        //[Description("Selects all regions with a customer, and shows the sum of orders for customers for each region.")]
        //public void LinqToEntities76()
        //{
        //    var query = from c in context.Customers
        //                group c by c.Address.Region into regions
        //                join c2 in context.Customers on regions.Key equals c2.Address.Region
        //                select new { region = regions.Key, total = c2.Orders.Sum(o => o.Freight) };

        //    ObjectDumper.Write(query);
        //}

        #endregion

        #region Inheritance

        [Category("Inheritance")]
        [Title("Show Type in Hierarchy")]
        [Description("Select all products, both active and discontinued products, and shows the type.")]
        public void LinqToEntities77() {
            var query = context
                .Products
                .Select(p => p);

            var query2 = query
                // force local execution to show local type
                .AsEnumerable()
                .Select(p => new { type = p.GetType().ToString(), prod = p });

            ObjectDumper.Write(query2, 2, 3);
        }

        [Category("Inheritance")]
        [Title("Select by Type in Hierarchy - OfType 1")]
        [Description("Select only discontinued products.")]
        public void LinqToEntities78() {
            var query = context.Products.OfType<DiscontinuedProduct>().Select(p => p);

            ObjectDumper.Write(query);
        }

        [Category("Inheritance")]
        [Title("Select by Type in Hierarchy - OfType 2")]
        [Description("Select only products, which will reutrn all Products and subtypes of Products (DiscontinuedProducts and ActiveProducts).")]
        public void LinqToEntities79() {
            var query = context.Products.OfType<Product>().Select(p => p);

            ObjectDumper.Write(query);
        }

        [Category("Inheritance")]
        [Title("Select by Type in Hierarchy - OfType 3")]
        [Description("Select only discontinued products.")]
        public void LinqToEntities80() {
            var query = context.Products.OfType<DiscontinuedProduct>();

            ObjectDumper.Write(query);
        }

        [Category("Inheritance")]
        [Title("Select by Type in Hierarchy - is")]
        [Description("Select only discontinued products.")]
        public void LinqToEntities81() {
            var query = context.Products.Where(p => p is DiscontinuedProduct);

            ObjectDumper.Write(query);
        }

        [Category("Inheritance")]
        [Title("Select by Subtype in Hierarchy - OfType")]
        [Description("Select all current employees.")]
        public void LinqToEntities87() {
            var query = context.Employees.OfType<CurrentEmployee>().ToList().Select(p => new { type = p.GetType().ToString(), p });

            ObjectDumper.Write(query, 2, 3);
        }

        #endregion

        #region Runtime behavior closure

        class MyClass {
            public static decimal Val = 50;

            public decimal GetVal() {
                return MyClass.Val;
            }
        }

        [Category("Runtime behavior example")]
        [Title("Static variable reference")]
        [Description("Uses a local variable as a query parameter.")]
        public void LinqToEntities91() {
            MyClass c = new MyClass();
            // public static decimal MyClass.Val = 50;

            var query = context.Orders.Where(o => o.Freight > MyClass.Val).Select(o => o);

            ObjectDumper.Write(query, 1);
        }

        [Category("Runtime behavior example")]
        [Title("Deferred execution")]
        [Description("Uses a the value of the local variable at query execution time.")]
        public void LinqToEntities92() {
            decimal x = 50;

            var query = context.Orders.Where(o => o.Freight > x).Select(o => new { o.Freight, o });

            x = 100;

            ObjectDumper.Write(query);
        }

        [Category("Runtime behavior example")]
        [Title("Closure")]
        [Description("Each execution uses the current value of the local variable.")]
        public void LinqToEntities93() {
            decimal x = 100;

            var query = context.Orders.Where(o => o.Freight > x).Select(o => new { o.Freight, o });

            ObjectDumper.Write(x);
            ObjectDumper.Write(query);

            x = 200;
            ObjectDumper.Write(x);
            ObjectDumper.Write(query);
        }

        #endregion

        #region Span

        [Category("Span")]
        [Title("Load Related Entities - One Level")]
        [Description("Load OrderDetails with Orders ")]
        public void LinqToEntities94() {
            var query = context.Orders.Include("OrderDetails").Where(c => c.Customer.Address.City == "London").Select(o => o);

            ObjectDumper.Write(query, 1);
        }

        [Category("Span")]
        [Title("Load Related Entities - Two Levels")]
        [Description("Load OrderDetails and Products with Orders ")]
        public void LinqToEntities95() {
            var query = context.Orders.Include("OrderDetails").Include("OrderDetails.Product").OrderBy(x => x.OrderID).Take(3).Select(o => o);

            ObjectDumper.Write(query, 2);
        }

        #endregion

        #region Paging

        [Category("Paging")]
        [Title("Skip 1")]
        [Description("Skip the most recent 2 orders from customers in London")]
        public void LinqToEntities96() {
            var query = context.Orders
                .Where(o => o.Customer.Address.City == "London")
                .OrderBy(o => o.OrderDate)
                .Skip(2).Select(o => o);

            ObjectDumper.Write(query);
        }

        [Category("Paging")]
        [Title("Take 1")]
        [Description("Take the 2 most recent Orders ")]
        public void LinqToEntities97() {
            var query = context.Orders
                .OrderBy(o => o.OrderDate)
                .Take(2).Select(o => o);

            ObjectDumper.Write(query);
        }

        [Category("Paging")]
        [Title("Take and Skip - 1")]
        [Description("Take the 10th to the 20th Orders, ordered by date ")]
        public void LinqToEntities98() {
            var query = context.Orders
                .OrderBy(o => o.OrderDate)
                .Skip(10).Take(10).Select(o => o);

            ObjectDumper.Write(query);
        }

        [Category("Paging")]
        [Title("Take and Skip - 2")]
        [Description("Use a page number variable to get the xth page")]
        public void LinqToEntities99() {
            int pageSize = 10;
            int pageNumber = 4;

            var query = context.Orders
                .OrderBy(o => o.OrderDate)
                .Skip(pageSize * pageNumber).Take(pageSize).Select(o => o);

            ObjectDumper.Write(query);
        }

        #endregion

        #region Compiled Queries

        [Category("Compiled Queries")]
        [Title("Simple")]
        [Description("This sample uses WHERE in a compiled query to find all customers whose contact title is Sales Representative.")]
        public void LinqToEntities100() {

            var cq = CompiledQuery.Compile<NorthwindEntities, IQueryable<Customer>>(
                ctx =>
                from cust in ctx.Customers
                where cust.ContactTitle == "Sales Representative"
                select cust);

            ObjectDumper.Write(cq(context));
        }

        [Category("Compiled Queries")]
        [Title("Anonymous Type Result")]
        [Description("This sample uses WHERE in a compiled query to find all customers whose contact title is Sales Representative. The result type is an anonymous type, so the non-generic form of CompiledQuery.Compile() is used.")]
        public void LinqToEntities101() {

            var cq = CompiledQuery.Compile(
                (NorthwindEntities ctx) =>
                from cust in ctx.Customers
                where cust.ContactTitle == "Sales Representative"
                select new { cust.CustomerID, cust.ContactName });

            ObjectDumper.Write(cq(context));
        }

        [Category("Compiled Queries")]
        [Title("Parametrized")]
        [Description("This sample uses WHERE in a parametrized compiled query to find all orders placed before 1997.")]
        public void LinqToEntities102() {
            var cq = CompiledQuery.Compile(
                (NorthwindEFModel.NorthwindEntities ctx, DateTime minOrderDate) =>
                from order in ctx.Orders
                where order.OrderDate < minOrderDate
                select order);

            ObjectDumper.Write(cq(context, new DateTime(1997, 1, 1)));
        }
        #endregion

        #region Functions

        [Category("Functions in LINQ queries")]
        [Title("Canonical")]
        [Description("Use the DiffYears canonical function to return all orders placed during the last 20 years")]
        public void LinqToEntitiesFunctions_EntityFunctions() {
            var query = from o in context.Orders
                        where DbFunctions.DiffYears(o.OrderDate, DateTime.Now) < 20
                        select o;

            ObjectDumper.Write(query);
        }

        [Category("Functions in LINQ queries")]
        [Title("Database Functions")]
        [Description("Use the CharIndex function from SQL Server to return all customers whose contact name starts with 'Simon'")]
        public void LinqToEntitiesFunctions_SqlFunctions() {
            var query = from c in context.Customers
                        where VfpFunctions.Atc("Simon", c.ContactName) == 1
                        select c;

            ObjectDumper.Write(query);
        }

        [Category("Functions in LINQ queries")]
        [Title("Model Defined Functions")]
        [Description("Use the method mapped to the NorthwindEFModel.TimesMDF model defined function inside a LINQ query")]
        public void LinqToEntitiesFunctions_MDFs() {
            var query = from p in context.Products
                        where p.ProductID > NorthwindEntitiesExtensions.TimesMDF(6, 7)
                        select p;

            ObjectDumper.Write(query);
        }

        [Category("Functions in LINQ queries")]
        [Title("Model Defined Functions as Object Methods")]
        [Description("Use the method mapped to the NorthwindEFModel.GetProductsForCategory model defined function to return products whose category is 'Beverages'")]
        public void LinqToEntitiesFunctions_MDFsOnContext() {
            var query = from c in context.GetProductsForCategory("Beverages")
                        select c;

            ObjectDumper.Write(query);
        }
        #endregion
    }
}