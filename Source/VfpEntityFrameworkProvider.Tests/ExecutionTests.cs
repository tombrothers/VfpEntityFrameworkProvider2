using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Tests.Dal.Decimal;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class ExecutionTests : TestBase {
        [TestMethod]
        public void DecmialFixTest() {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            DecimalEntity item;

            using (var context = CreateDecimalContext()) {
                Assert.AreEqual(1, context.Decimals.ToList().Count);
                Assert.AreEqual(987654321.123456M, context.Decimals.First().Value);

                context.Decimals.Add(new DecimalEntity { Value = 1.2m });
                context.SaveChanges();
            }

            using (var context = CreateDecimalContext()) {
                item = context.Decimals.Where(x => x.Value > 2m).Single();
                Assert.IsNotNull(item);
            }

            using (var context = CreateDecimalContext()) {
                Assert.AreEqual(2, context.Decimals.ToList().Count);
                context.Decimals.Remove(context.Decimals.First());

                context.SaveChanges();
            }

            using (var context = CreateDecimalContext()) {
                Assert.AreEqual(1, context.Decimals.ToList().Count);
            }

            Thread.CurrentThread.CurrentCulture = currentCulture;
        }

        private DecimalContext CreateDecimalContext() {
            var connectionString = Path.Combine(GetTestDeploymentDir(TestContext), "Decimal");
            var connection = new VfpConnection(connectionString);

            EnableTracing(connection);

            return new DecimalContext(connection);
        }

        [TestMethod]
        public void TestNotInList() {
            var northwind = GetContext();
            var ids = northwind.Orders.OrderBy(o => o.OrderID).Take(10).Select(o => o.OrderID).ToArray();
            var list = northwind.Orders.Where(o => !ids.Contains(o.OrderID)).ToList();

            Assert.AreEqual(820, list.Count);
        }

        [TestMethod]
        public void TestInList() {
            var northwind = GetContext();
            var ids = northwind.Orders.OrderBy(o => o.OrderID).Take(10).Select(o => o.OrderID).ToArray();
            var list = northwind.Orders.Where(o => ids.Contains(o.OrderID)).Where(x => x.EmployeeID == 5).ToList();

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestWhere() {
            var northwind = this.GetContext();
            Assert.AreEqual(6, northwind.Customers.Where(c => c.Address.City == "London").Count());
        }

        [TestMethod]
        public void TestWhereTrue() {
            var northwind = this.GetContext();
            var x = northwind.Customers.Where(c => true);

            Assert.AreEqual(91, northwind.Customers.Where(c => true).Count());
        }

        [TestMethod]
        public void TestCompareConstructedEqual() {
            var northwind = this.GetContext();
            Assert.AreEqual(6, northwind.Customers.Where(c => new { x = c.Address.City } == new { x = "London" }).Count());
        }

        [TestMethod]
        public void TestCompareConstructedMultiValueEqual() {
            var northwind = this.GetContext();
            Assert.AreEqual(6, northwind.Customers.Where(c => new { x = c.Address.City, y = c.Address.Country } == new { x = "London", y = "UK" }).Count());
        }

        [TestMethod]
        public void TestCompareConstructedMultiValueNotEqual() {
            var northwind = this.GetContext();
            Assert.AreEqual(85, northwind.Customers.Where(c => new { x = c.Address.City, y = c.Address.Country } != new { x = "London", y = "UK" }).Count());
        }

        [TestMethod]
        public void TestSelectScalar() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City == "London").Select(c => c.Address.City).ToList();

            Assert.AreEqual(6, list.Count);
            Assert.AreEqual("London", list[0]);
            Assert.IsTrue(list.All(x => x == "London"));
        }

        [TestMethod]
        public void TestSelectAnonymousOne() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City == "London").Select(c => new { c.Address.City }).ToList();

            Assert.AreEqual(6, list.Count);
            Assert.AreEqual("London", list[0].City);
            Assert.IsTrue(list.All(x => x.City == "London"));
        }

        [TestMethod]
        public void TestSelectAnonymousTwo() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City == "London").Select(c => new { c.Address.City, c.Phone }).ToList();

            Assert.AreEqual(6, list.Count);
            Assert.AreEqual("London", list[0].City);
            Assert.IsTrue(list.All(x => x.City == "London"));
            Assert.IsTrue(list.All(x => x.Phone != null));
        }

        [TestMethod]
        public void TestSelectCustomerTable() {
            var northwind = this.GetContext();
            var list = northwind.Customers.ToList();
            Assert.AreEqual(91, list.Count);
        }

        [TestMethod]
        public void TestSelectAnonymousWithObject() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City == "London").Select(c => new { c.Address.City, c }).ToList();

            Assert.AreEqual(6, list.Count);
            Assert.AreEqual("London", list[0].City);
            Assert.IsTrue(list.All(x => x.City == "London"));
            Assert.IsTrue(list.All(x => x.c.Address.City == x.City));
        }

        [TestMethod]
        public void TestSelectAnonymousLiteral() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City == "London").Select(c => new { X = 10 }).ToList();

            Assert.AreEqual(6, list.Count);
            Assert.IsTrue(list.All(x => x.X == 10));
        }

        [TestMethod]
        public void TestSelectConstantInt() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(c => 10).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(list.All(x => x == 10));
        }

        [TestMethod]
        public void TestSelectConstantNullString() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(c => (string)null).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(list.All(x => x == null));
        }

        [TestMethod]
        public void TestSelectLocal() {
            var northwind = this.GetContext();
            int x = 10;
            var list = northwind.Customers.Select(c => x).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(list.All(y => y == 10));
        }

        [TestMethod]
        public void TestSelectNestedCollectionInAnonymousType() {
            var northwind = this.GetContext();
            var list = (
                from c in northwind.Customers
                where c.CustomerID == "ALFKI"
                select new { Foos = northwind.Orders.Where(o => o.Customer.CustomerID == c.CustomerID).Select(o => o.OrderID).ToList() }
                ).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0].Foos.Count);
        }

        [TestMethod]
        public void TestJoinCustomerOrders() {
            var northwind = this.GetContext();
            var list = (
                from c in northwind.Customers
                where c.CustomerID == "ALFKI"
                join o in northwind.Orders on c.CustomerID equals o.Customer.CustomerID
                select new { c.ContactName, o.OrderID }
                ).ToList();

            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void TestJoinMultiKey() {
            var northwind = this.GetContext();
            var list = (
                from c in northwind.Customers
                where c.CustomerID == "ALFKI"
                join o in northwind.Orders on new { a = c.CustomerID, b = c.CustomerID } equals new { a = o.Customer.CustomerID, b = o.Customer.CustomerID }
                select new { c, o }
                ).ToList();

            Assert.AreEqual(6, list.Count);
        }

        [TestMethod, Ignore]
        public void TestJoinIntoCustomersOrdersCount() {
            Assert.Inconclusive("No ready for this test yet.");

            var northwind = this.GetContext();
            var list = (
                from c in northwind.Customers
                where c.CustomerID == "ALFKI"
                join o in northwind.Orders on c.CustomerID equals o.Customer.CustomerID into ords
                select new { cust = c, ords = ords.Count() }
                ).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0].ords);
        }

        [TestMethod]
        public void TestJoinIntoDefaultIfEmpty() {
            var northwind = this.GetContext();
            var list = (
                from c in northwind.Customers
                where c.CustomerID == "PARIS"
                join o in northwind.Orders on c.CustomerID equals o.Customer.CustomerID into ords
                from o in ords.DefaultIfEmpty()
                select new { c, o }
                ).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(null, list[0].o);
        }

        [TestMethod]
        public void TestMultipleJoinsWithJoinConditionsInWhere() {
            var northwind = this.GetContext();
            // this should reduce to inner joins
            var list = (
                from c in northwind.Customers
                from o in northwind.Orders
                from d in northwind.OrderDetails
                where o.Customer.CustomerID == c.CustomerID && o.OrderID == d.OrderID
                where c.CustomerID == "ALFKI"
                select d
                ).ToList();

            Assert.AreEqual(12, list.Count);
        }

        [TestMethod]
        public void TestOrderBy() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderBy(c => c.CustomerID).Select(c => c.CustomerID).ToList();
            var sorted = list.OrderBy(c => c).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        [TestMethod]
        public void TestOrderByOrderBy() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderBy(c => c.Phone).OrderBy(c => c.CustomerID).ToList();
            var sorted = list.OrderBy(c => c.CustomerID).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        [TestMethod]
        public void TestOrderByThenBy() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderBy(c => c.CustomerID).ThenBy(c => c.Phone).ToList();
            var sorted = list.OrderBy(c => c.CustomerID).ThenBy(c => c.Phone).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }


        [TestMethod]
        public void TestOrderByDescending() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderByDescending(c => c.CustomerID).ToList();
            var sorted = list.OrderByDescending(c => c.CustomerID).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        [TestMethod]
        public void TestOrderByDescendingThenBy() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderByDescending(c => c.CustomerID).ThenBy(c => c.Address.Country).ToList();
            var sorted = list.OrderByDescending(c => c.CustomerID).ThenBy(c => c.Address.Country).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        [TestMethod]
        public void TestOrderByDescendingThenByDescending() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderByDescending(c => c.CustomerID).ThenByDescending(c => c.Address.Country).ToList();
            var sorted = list.OrderByDescending(c => c.CustomerID).ThenByDescending(c => c.Address.Country).ToList();

            Assert.AreEqual(91, list.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        // Confirmed that EF drops the OrderBy condition.
        [TestMethod, Ignore]
        public void TestOrderByJoin() {
            Assert.Inconclusive("OrderBy doesn't appear to be included in the express tree as part of a join statement... need to verify functionality with the Sql Sever provider.");
            var northwind = this.GetContext();
            var list = (
                from c in northwind.Customers.OrderBy(c => c.CustomerID)
                join o in northwind.Orders.OrderBy(o => o.OrderID) on c.CustomerID equals o.Customer.CustomerID
                select new { c.CustomerID, o.OrderID }
                ).ToList();

            var sorted = list.OrderBy(x => x.CustomerID).ThenBy(x => x.OrderID);

            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        // Confirmed that EF drops the OrderBy condition.
        [TestMethod, Ignore]
        public void TestOrderBySelectMany() {
            Assert.Inconclusive("OrderBy doesn't appear to be included in the express tree as part of a join statement... need to verify functionality with the Sql Sever provider.");
            var northwind = this.GetContext();
            var list = (
                from c in northwind.Customers.OrderBy(c => c.CustomerID)
                from o in northwind.Orders.OrderBy(o => o.OrderID)
                where c.CustomerID == o.Customer.CustomerID
                select new { c.CustomerID, o.OrderID }
                ).ToList();

            var sorted = list.OrderBy(x => x.CustomerID).ThenBy(x => x.OrderID).ToList();

            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        [TestMethod]
        public void TestGroupBy() {
            var northwind = this.GetContext();
            Assert.AreEqual(69, northwind.Customers.GroupBy(c => c.Address.City).ToList().Count);
        }

        [TestMethod]
        public void TestGroupByOne() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City == "London").GroupBy(c => c.Address.City).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0].Count());
        }

        [TestMethod]
        public void TestGroupBySelectMany() {
            var northwind = this.GetContext();
            var list = northwind.Customers.GroupBy(c => c.Address.City).SelectMany(g => g).ToList();
            Assert.AreEqual(91, list.Count);
        }

        [TestMethod]
        public void TestGroupBySum() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID).Select(g => g.Sum(o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1))).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0]);
        }

        [TestMethod]
        public void TestGroupByCount() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID).Select(g => g.Count()).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0]);
        }

        [TestMethod]
        public void TestGroupByLongCount() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID).Select(g => g.LongCount()).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6L, list[0]);
        }

        [TestMethod]
        public void TestGroupBySumMinMaxAvg() {
            var northwind = this.GetContext();
            var list =
                northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID).Select(g =>
                    new {
                        Sum = g.Sum(o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1)),
                        Min = g.Min(o => o.OrderID),
                        Max = g.Max(o => o.OrderID),
                        Avg = g.Average(o => o.OrderID)
                    }).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0].Sum);
        }

        [TestMethod]
        public void TestGroupByWithResultSelector() {
            var northwind = this.GetContext();
            var list =
                northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID, (k, g) =>
                    new {
                        Sum = g.Sum(o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1)),
                        Min = g.Min(o => o.OrderID),
                        Max = g.Max(o => o.OrderID),
                        Avg = g.Average(o => o.OrderID)
                    }).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0].Sum);
        }

        [TestMethod]
        public void TestGroupByWithElementSelectorSum() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID, o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1)).Select(g => g.Sum()).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0]);
        }

        [TestMethod]
        public void TestGroupByWithElementSelector() {
            var northwind = this.GetContext();
            // note: groups are retrieved through a separately execute subquery per row
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID, o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1)).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0].Count());
            Assert.AreEqual(6, list[0].Sum());
        }

        [TestMethod]
        public void TestGroupByWithElementSelectorSumMax() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID, o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1)).Select(g => new { Sum = g.Sum(), Max = g.Max() }).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0].Sum);
            Assert.AreEqual(1, list[0].Max);
        }

        [TestMethod, Ignore]
        public void TestGroupByWithAnonymousElement() {
            Assert.Inconclusive("No ready for this test yet.");
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID, o => new { X = (o.Customer.CustomerID == "ALFKI" ? 1 : 1) }).Select(g => g.Sum(x => x.X)).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(6, list[0]);
        }

        [TestMethod]
        public void TestGroupByWithTwoPartKey() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => new { o.Customer.CustomerID, o.OrderDate }).Select(g => g.Sum(o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1))).ToList();

            Assert.AreEqual(6, list.Count);
        }


        [TestMethod]
        public void TestOrderByGroupBy() {
            var northwind = this.GetContext();
            // note: order-by is lost when group-by is applied (the sequence of groups is not ordered)
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").OrderBy(o => o.OrderID).GroupBy(o => o.Customer.CustomerID).ToList();
            Assert.AreEqual(1, list.Count);

            var grp = list[0].ToList();
            var sorted = grp.OrderBy(o => o.OrderID);
            Assert.IsTrue(Enumerable.SequenceEqual(grp, sorted));
        }

        [TestMethod]
        public void TestOrderByGroupBySelectMany() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").OrderBy(o => o.OrderID).GroupBy(o => o.Customer.CustomerID).SelectMany(g => g).ToList();
            Assert.AreEqual(6, list.Count);

            var sorted = list.OrderBy(o => o.OrderID).ToList();
            Assert.IsTrue(Enumerable.SequenceEqual(list, sorted));
        }

        [TestMethod]
        public void TestSumWithNoArg() {
            var northwind = this.GetContext();
            var sum = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").Select(o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1)).Sum();
            Assert.AreEqual(6, sum);
        }

        [TestMethod]
        public void TestSumWithArg() {
            var northwind = this.GetContext();
            var sum = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").Sum(o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1));
            Assert.AreEqual(6, sum);
        }

        [TestMethod]
        public void TestCountWithNoPredicate() {
            var northwind = this.GetContext();
            var cnt = northwind.Orders.Count();
            Assert.AreEqual(830, cnt);
        }

        [TestMethod]
        public void TestCountWithPredicate() {
            var northwind = this.GetContext();
            var cnt = northwind.Orders.Count(o => o.Customer.CustomerID == "ALFKI");
            Assert.AreEqual(6, cnt);
        }

        [TestMethod]
        public void TestDistinctNoDupes() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Distinct().ToList();
            Assert.AreEqual(91, list.Count);
        }

        [TestMethod]
        public void TestDistinctScalar() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(c => c.Address.City).Distinct().ToList();
            Assert.AreEqual(69, list.Count);
        }

        [TestMethod]
        public void TestOrderByDistinct() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City.StartsWith("P")).OrderBy(c => c.Address.City).Select(c => c.Address.City).Distinct().ToList();
            var sorted = list.OrderBy(x => x).ToList();
            Assert.AreEqual(list[0], sorted[0]);
            Assert.AreEqual(list[list.Count - 1], sorted[list.Count - 1]);
        }

        [TestMethod]
        public void TestDistinctOrderBy() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City.StartsWith("P")).Select(c => c.Address.City).Distinct().OrderBy(c => c).ToList();
            var sorted = list.OrderBy(x => x).ToList();
            Assert.AreEqual(list[0], sorted[0]);
            Assert.AreEqual(list[list.Count - 1], sorted[list.Count - 1]);
        }

        [TestMethod]
        public void TestDistinctGroupBy() {
            var northwind = this.GetContext();
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").Distinct().GroupBy(o => o.Customer.CustomerID).ToList();

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGroupByDistinct() {
            var northwind = this.GetContext();
            // distinct after group-by should not do anything
            var list = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").GroupBy(o => o.Customer.CustomerID).Distinct().ToList();

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestDistinctCount() {
            var northwind = this.GetContext();
            var cnt = northwind.Customers.Distinct().Count();
            Assert.AreEqual(91, cnt);
        }

        [TestMethod]
        public void TestSelectDistinctCount() {
            var northwind = this.GetContext();
            // cannot do: SELECT COUNT(DISTINCT some-colum) FROM some-table
            // because COUNT(DISTINCT some-column) does not count nulls
            var cnt = northwind.Customers.Select(c => c.Address.City).Distinct().Count();
            Assert.AreEqual(69, cnt);
        }

        [TestMethod]
        public void TestSelectSelectDistinctCount() {
            var northwind = this.GetContext();
            var cnt = northwind.Customers.Select(c => c.Address.City).Select(c => c).Distinct().Count();
            Assert.AreEqual(69, cnt);
        }

        [TestMethod]
        public void TestDistinctCountPredicate() {
            var northwind = this.GetContext();
            var cnt = northwind.Customers.Select(c => new { c.Address.City, c.Address.Country }).Distinct().Count(c => c.City == "London");
            Assert.AreEqual(1, cnt);
        }

        [TestMethod]
        public void TestDistinctSumWithArg() {
            var northwind = this.GetContext();
            var sum = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").Distinct().Sum(o => (o.Customer.CustomerID == "ALFKI" ? 1 : 1));
            Assert.AreEqual(6, sum);
        }

        [TestMethod]
        public void TestSelectDistinctSum() {
            var northwind = this.GetContext();
            var sum = northwind.Orders.Where(o => o.Customer.CustomerID == "ALFKI").Select(o => o.OrderID).Distinct().Sum();
            Assert.AreEqual(64835, sum);
        }

        [TestMethod]
        public void TestTake() {
            var northwind = this.GetContext();
            var list = northwind.Orders.OrderBy(o => o.Customer.CustomerID).Take(5).ToList();
            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void TestTakeDistinct() {
            var northwind = this.GetContext();
            // distinct must be forced to apply after top has been computed
            var list = northwind.Orders.OrderBy(o => o.Customer.CustomerID).Select(o => o.Customer.CustomerID).Take(5).Distinct().ToList();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestDistinctTake() {
            var northwind = this.GetContext();
            // top must be forced to apply after distinct has been computed
            var list = northwind.Orders.Select(o => o.Customer.CustomerID).Distinct().OrderBy(o => o).Take(5).ToList();
            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void TestDistinctTakeCount() {
            var northwind = this.GetContext();
            var cnt = northwind.Orders.Distinct().OrderBy(o => o.Customer.CustomerID).Select(o => o.Customer.CustomerID).Take(5).Count();
            Assert.AreEqual(5, cnt);
        }

        [TestMethod]
        public void TestTakeDistinctCount() {
            var northwind = this.GetContext();
            var cnt = northwind.Orders.OrderBy(o => o.Customer.CustomerID).Select(o => o.Customer.CustomerID).Take(5).Distinct().Count();
            Assert.AreEqual(1, cnt);
        }

        [TestMethod]
        public void TestFirst() {
            var northwind = this.GetContext();
            var first = northwind.Customers.OrderBy(c => c.ContactName).First();
            Assert.AreNotEqual(null, first);
            Assert.AreEqual("ROMEY", first.CustomerID);
        }

        [TestMethod]
        public void TestFirstPredicate() {
            var northwind = this.GetContext();
            var first = northwind.Customers.OrderBy(c => c.ContactName).First(c => c.Address.City == "London");
            Assert.AreNotEqual(null, first);
            Assert.AreEqual("EASTC", first.CustomerID);
        }

        [TestMethod]
        public void TestWhereFirst() {
            var northwind = this.GetContext();
            var first = northwind.Customers.OrderBy(c => c.ContactName).Where(c => c.Address.City == "London").First();
            Assert.AreNotEqual(null, first);
            Assert.AreEqual("EASTC", first.CustomerID);
        }

        [TestMethod]
        public void TestFirstOrDefault() {
            var northwind = this.GetContext();
            var first = northwind.Customers.OrderBy(c => c.ContactName).FirstOrDefault();
            Assert.AreNotEqual(null, first);
            Assert.AreEqual("ROMEY", first.CustomerID);
        }

        [TestMethod]
        public void TestFirstOrDefaultPredicate() {
            var northwind = this.GetContext();
            var first = northwind.Customers.OrderBy(c => c.ContactName).FirstOrDefault(c => c.Address.City == "London");
            Assert.AreNotEqual(null, first);
            Assert.AreEqual("EASTC", first.CustomerID);
        }

        [TestMethod]
        public void TestWhereFirstOrDefault() {
            var northwind = this.GetContext();
            var first = northwind.Customers.OrderBy(c => c.ContactName).Where(c => c.Address.City == "London").FirstOrDefault();
            Assert.AreNotEqual(null, first);
            Assert.AreEqual("EASTC", first.CustomerID);
        }

        [TestMethod]
        public void TestFirstOrDefaultPredicateNoMatch() {
            var northwind = this.GetContext();
            var first = northwind.Customers.OrderBy(c => c.ContactName).FirstOrDefault(c => c.Address.City == "SpongeBob");
            Assert.AreEqual(null, first);
        }

        [TestMethod]
        public void TestSingleFails() {
            var northwind = this.GetContext();
            try {
                var single = northwind.Customers.OrderBy(c => c.CustomerID).Single();
            }
            catch (InvalidOperationException ex) {
                if (ex.Message.Contains("Sequence contains more than one element")) {
                    return;
                }

                throw;
            }
            throw new Exception("The following Exception was not thrown.\rInvalidOperationException: Sequence contains more than one element ");
        }

        [TestMethod]
        public void TestSinglePredicate() {
            var northwind = this.GetContext();
            var single = northwind.Customers.OrderBy(c => c.CustomerID).Single(c => c.CustomerID == "ALFKI");
            Assert.AreNotEqual(null, single);
            Assert.AreEqual("ALFKI", single.CustomerID);
        }

        [TestMethod]
        public void TestWhereSingle() {
            var northwind = this.GetContext();
            var single = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).Single();
            Assert.AreNotEqual(null, single);
            Assert.AreEqual("ALFKI", single.CustomerID);
        }

        [TestMethod]
        public void TestSingleOrDefaultFails() {
            var northwind = this.GetContext();
            try {
                var single = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault();
            }
            catch (InvalidOperationException ex) {
                if (ex.Message.Contains("Sequence contains more than one element")) {
                    return;
                }

                throw;
            }
            throw new Exception("The following Exception was not thrown.\rInvalidOperationException: Sequence contains more than one element ");
        }

        [TestMethod]
        public void TestSingleOrDefaultPredicate() {
            var northwind = this.GetContext();
            var single = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI");
            Assert.AreNotEqual(null, single);
            Assert.AreEqual("ALFKI", single.CustomerID);
        }

        [TestMethod]
        public void TestWhereSingleOrDefault() {
            var northwind = this.GetContext();
            var single = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault();
            Assert.AreNotEqual(null, single);
            Assert.AreEqual("ALFKI", single.CustomerID);
        }

        [TestMethod]
        public void TestSingleOrDefaultNoMatches() {
            var northwind = this.GetContext();
            var single = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "SpongeBob");
            Assert.AreEqual(null, single);
        }

        [TestMethod]
        public void TestAnyTopLevel() {
            var northwind = this.GetContext();
            var any = northwind.Customers.Any();
            Assert.IsTrue(any);
        }

        [TestMethod]
        public void TestAnyWithSubquery() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Orders.Any(o => o.Customer.CustomerID == "ALFKI")).ToList();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestAnyWithSubqueryNoPredicate() {
            var northwind = this.GetContext();
            // customers with at least one order
            var list = northwind.Customers.Where(c => northwind.Orders.Where(o => o.Customer.CustomerID == c.CustomerID).Any()).ToList();
            Assert.AreEqual(89, list.Count);
        }

        [TestMethod]
        public void TestAnyWithLocalCollection() {
            var northwind = this.GetContext();
            // get customers for any one of these IDs
            string[] ids = new[] { "ALFKI", "WOLZA", "NOONE" };
            var list = northwind.Customers.Where(c => ids.Any(id => c.CustomerID == id)).ToList();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod, Ignore]
        public void TestAllWithSubquery() {
            Assert.Inconclusive("No ready for this test yet.");
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Orders.All(o => o.Customer.CustomerID == "ALFKI")).ToList();
            // includes customers w/ no orders
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod, Ignore]
        public void TestAllWithLocalCollection() {
            Assert.Inconclusive("No ready for this test yet.");
            var northwind = this.GetContext();
            // get all customers with a name that contains both 'm' and 'd'  (don't use vowels since these often depend on collation)
            string[] patterns = new[] { "m", "d" };

            var list = northwind.Customers.Where(c => patterns.All(p => c.ContactName.ToLower().Contains(p))).Select(c => c.ContactName).ToList();
            var local = northwind.Customers.AsEnumerable().Where(c => patterns.All(p => c.ContactName.ToLower().Contains(p))).Select(c => c.ContactName).ToList();


            Assert.AreEqual(local.Count, list.Count);
        }

        [TestMethod]
        public void TestAllTopLevel() {
            var northwind = this.GetContext();
            // all customers have name length > 0?
            var all = northwind.Customers.All(c => c.ContactName.Length > 0);
            Assert.IsTrue(all);
        }

        [TestMethod]
        public void TestAllTopLevelNoMatches() {
            var northwind = this.GetContext();
            // all customers have name with 'a'
            var all = northwind.Customers.All(c => c.ContactName.Contains("a"));
            Assert.IsFalse(all);
        }

        [TestMethod]
        public void TestContainsWithSubquery() {
            var northwind = this.GetContext();
            // this is the long-way to determine all customers that have at least one order
            var list = northwind.Customers.Where(c => northwind.Orders.Select(o => o.Customer.CustomerID).Contains(c.CustomerID)).ToList();
            Assert.AreEqual(89, list.Count);
        }

        [TestMethod]
        public void TestContainsWithLocalCollection() {
            var northwind = this.GetContext();
            string[] ids = new[] { "ALFKI", "WOLZA", "NOONE" };
            var list = northwind.Customers.Where(c => ids.Contains(c.CustomerID)).ToList();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestContainsTopLevel() {
            var northwind = this.GetContext();
            var contains = northwind.Customers.Select(c => c.CustomerID).Contains("ALFKI");
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void TestSkipTake() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderBy(c => c.CustomerID).Select(c => c.CustomerID).Skip(5).Take(10).ToList();
            Assert.AreEqual(10, list.Count);
            Assert.AreEqual("BLAUS", list[0]);
            Assert.AreEqual("COMMI", list[9]);
        }

        [TestMethod]
        public void TestDistinctSkipTake() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(c => c.Address.City).Distinct().OrderBy(c => c).Skip(5).Take(10).ToList();
            Assert.AreEqual(10, list.Count);
            var hs = new HashSet<string>(list);
            Assert.AreEqual(10, hs.Count);
        }

        [TestMethod]
        public void TestCoalesce() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(c => new { City = (c.Address.City == "London" ? null : c.Address.City), Country = (c.CustomerID == "EASTC" ? null : c.Address.Country) })
                         .Where(x => (x.City ?? "NoCity") == "NoCity").ToList();
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(null, list[0].City);
        }

        [TestMethod]
        public void TestCoalesce2() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(c => new { City = (c.Address.City == "London" ? null : c.Address.City), Country = (c.CustomerID == "EASTC" ? null : c.Address.Country) })
                         .Where(x => (x.City ?? x.Country ?? "NoCityOrCountry") == "NoCityOrCountry").ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(null, list[0].City);
            Assert.AreEqual(null, list[0].Country);
        }

        // framework function tests
        [TestMethod]
        public void TestStringLength() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City.Trim().Length == 7).ToList();
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        public void TestStringStartsWithLiteral() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.ContactName.StartsWith("M")).ToList();
            Assert.AreEqual(12, list.Count);
        }

        [TestMethod]
        public void TestStringStartsWithColumn() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.ContactName.StartsWith(c.ContactName)).ToList();
            Assert.AreEqual(91, list.Count);
        }

        [TestMethod]
        public void TestStringEndsWithLiteral() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.ContactName.EndsWith("s")).ToList();
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        public void TestStringEndsWithColumn() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.ContactName.EndsWith(c.ContactName)).ToList();
            Assert.AreEqual(91, list.Count);
        }

        [TestMethod]
        public void TestStringContainsLiteral() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.ContactName.Contains("nd")).Select(c => c.ContactName).ToList();
            var local = northwind.Customers.AsEnumerable().Where(c => c.ContactName.ToLower().Contains("nd")).Select(c => c.ContactName).ToList();
            Assert.AreEqual(local.Count, list.Count);
        }

        [TestMethod]
        public void TestStringContainsColumn() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.ContactName.Contains(c.ContactName)).ToList();
            Assert.AreEqual(91, list.Count);
        }

        [TestMethod]
        public void TestStringConcatImplicit2Args() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.ContactName.Trim() + "X" == "Maria AndersX").ToList();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestStringConcatExplicit2Args() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => string.Concat(c.ContactName.Trim(), "X") == "Maria AndersX").ToList();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestStringConcatExplicit3Args() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => string.Concat(c.ContactName.Trim(), "X", c.Address.Country.Trim()) == "Maria AndersXGermany").ToList();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void TestStringIsNullOrEmpty() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(c => c.Address.City == "London" ? null : c.CustomerID).Where(x => string.IsNullOrEmpty(x)).ToList();
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void TestStringToUpper() {
            var northwind = this.GetContext();
            var str = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Max(c => (c.CustomerID == "ALFKI" ? "abc" : "abc").ToUpper());
            Assert.AreEqual("ABC", str);
        }

        [TestMethod]
        public void TestStringToLower() {
            var northwind = this.GetContext();
            var str = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Max(c => (c.CustomerID == "ALFKI" ? "ABC" : "ABC").ToLower());
            Assert.AreEqual("abc", str);
        }

        [TestMethod]
        public void TestStringSubstring() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City.Substring(0, 4) == "Seat").ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Seattle", list[0].Address.City);
        }

        [TestMethod]
        public void TestStringSubstringNoLength() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(c => c.Address.City.Substring(4) == "tle").ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Seattle", list[0].Address.City);
        }

        [TestMethod]
        public void TestStringIndexOf() {
            var northwind = this.GetContext();
            var n = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => c.ContactName.IndexOf("ar"));
            Assert.AreEqual(1, n);
        }

        [TestMethod]
        public void TestStringTrim() {
            var northwind = this.GetContext();
            var notrim = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Max(c => ("  " + c.Address.City + " "));
            var trim = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Max(c => ("  " + c.Address.City + " ").Trim());
            Assert.AreNotEqual(notrim, trim);
            Assert.AreEqual(notrim.Trim(), trim);
        }

        [TestMethod]
        public void TestMathAbs() {
            var northwind = this.GetContext();
            var neg1 = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Abs((c.CustomerID == "ALFKI") ? -1 : 0));
            var pos1 = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Abs((c.CustomerID == "ALFKI") ? 1 : 0));
            Assert.AreEqual(Math.Abs(-1), neg1);
            Assert.AreEqual(Math.Abs(1), pos1);
        }

        [TestMethod]
        public void TestMathPow() {
            var northwind = this.GetContext();
            // 2^n
            var zero = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Pow((c.CustomerID == "ALFKI") ? 2.0 : 2.0, 0.0));
            var one = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Pow((c.CustomerID == "ALFKI") ? 2.0 : 2.0, 1.0));
            var two = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Pow((c.CustomerID == "ALFKI") ? 2.0 : 2.0, 2.0));
            var three = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Pow((c.CustomerID == "ALFKI") ? 2.0 : 2.0, 3.0));
            Assert.AreEqual(1.0, zero);
            Assert.AreEqual(2.0, one);
            Assert.AreEqual(4.0, two);
            Assert.AreEqual(8.0, three);
        }

        [TestMethod]
        public void TestMathRoundDefault() {
            var northwind = this.GetContext();
            var four = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Round((c.CustomerID == "ALFKI") ? 3.4 : 3.4));
            var six = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Round((c.CustomerID == "ALFKI") ? 3.6 : 3.6));
            Assert.AreEqual(3.0, four);
            Assert.AreEqual(4.0, six);
        }

        [TestMethod]
        public void TestMathFloor() {
            var northwind = this.GetContext();
            // The difference between floor and truncate is how negatives are handled.  Floor drops the decimals and moves the
            // value to the more negative, so Floor(-3.4) is -4.0 and Floor(3.4) is 3.0.
            var four = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Floor((c.CustomerID == "ALFKI" ? 3.4 : 3.4)));
            var six = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Floor((c.CustomerID == "ALFKI" ? 3.6 : 3.6)));
            var nfour = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => Math.Floor((c.CustomerID == "ALFKI" ? -3.4 : -3.4)));
            Assert.AreEqual(Math.Floor(3.4), four);
            Assert.AreEqual(Math.Floor(3.6), six);
            Assert.AreEqual(Math.Floor(-3.4), nfour);
        }

        [TestMethod]
        public void TestDecimalFloor() {
            var northwind = this.GetContext();
            var four = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Floor((c.CustomerID == "ALFKI" ? 3.4m : 3.4m)));
            var six = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Floor((c.CustomerID == "ALFKI" ? 3.6m : 3.6m)));
            var nfour = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Floor((c.CustomerID == "ALFKI" ? -3.4m : -3.4m)));
            Assert.AreEqual(decimal.Floor(3.4m), four);
            Assert.AreEqual(decimal.Floor(3.6m), six);
            Assert.AreEqual(decimal.Floor(-3.4m), nfour);
        }

        [TestMethod]
        public void TestStringCompareTo() {
            var northwind = this.GetContext();
            var lt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => c.Address.City.CompareTo("Seattle"));
            var gt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => c.Address.City.CompareTo("Aaa"));
            var eq = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => c.Address.City.CompareTo("Berlin"));
            Assert.AreEqual(-1, lt);
            Assert.AreEqual(1, gt);
            Assert.AreEqual(0, eq);
        }

        [TestMethod]
        public void TestStringCompareToLT() {
            var northwind = this.GetContext();
            var cmpLT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Seattle") < 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Berlin") < 0);
            Assert.AreNotEqual(null, cmpLT);
            Assert.AreEqual(null, cmpEQ);
        }

        [TestMethod]
        public void TestStringCompareToLE() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Seattle") <= 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Berlin") <= 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Aaa") <= 0);
            Assert.AreNotEqual(null, cmpLE);
            Assert.AreNotEqual(null, cmpEQ);
            Assert.AreEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestStringCompareToGT() {
            var northwind = this.GetContext();
            var cmpLT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Aaa") > 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Berlin") > 0);
            Assert.AreNotEqual(null, cmpLT);
            Assert.AreEqual(null, cmpEQ);
        }

        [TestMethod]
        public void TestStringCompareToGE() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Seattle") >= 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Berlin") >= 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Aaa") >= 0);
            Assert.AreEqual(null, cmpLE);
            Assert.AreNotEqual(null, cmpEQ);
            Assert.AreNotEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestStringCompareToEQ() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Seattle") == 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Berlin") == 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Aaa") == 0);
            Assert.AreEqual(null, cmpLE);
            Assert.AreNotEqual(null, cmpEQ);
            Assert.AreEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestStringCompareToNE() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Seattle") != 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Berlin") != 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => c.Address.City.CompareTo("Aaa") != 0);
            Assert.AreNotEqual(null, cmpLE);
            Assert.AreEqual(null, cmpEQ);
            Assert.AreNotEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestStringCompare() {
            var northwind = this.GetContext();
            var lt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => string.Compare(c.Address.City, "Seattle"));
            var gt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => string.Compare(c.Address.City, "Aaa"));
            var eq = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => string.Compare(c.Address.City, "Berlin"));
            Assert.AreEqual(-1, lt);
            Assert.AreEqual(1, gt);
            Assert.AreEqual(0, eq);
        }

        [TestMethod]
        public void TestStringCompareLT() {
            var northwind = this.GetContext();
            var cmpLT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Seattle") < 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Berlin") < 0);
            Assert.AreNotEqual(null, cmpLT);
            Assert.AreEqual(null, cmpEQ);
        }

        [TestMethod]
        public void TestStringCompareLE() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Seattle") <= 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Berlin") <= 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Aaa") <= 0);
            Assert.AreNotEqual(null, cmpLE);
            Assert.AreNotEqual(null, cmpEQ);
            Assert.AreEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestStringCompareGT() {
            var northwind = this.GetContext();
            var cmpLT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Aaa") > 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Berlin") > 0);
            Assert.AreNotEqual(null, cmpLT);
            Assert.AreEqual(null, cmpEQ);
        }

        [TestMethod]
        public void TestStringCompareGE() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Seattle") >= 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Berlin") >= 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Aaa") >= 0);
            Assert.AreEqual(null, cmpLE);
            Assert.AreNotEqual(null, cmpEQ);
            Assert.AreNotEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestStringCompareEQ() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Seattle") == 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Berlin") == 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Aaa") == 0);
            Assert.AreEqual(null, cmpLE);
            Assert.AreNotEqual(null, cmpEQ);
            Assert.AreEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestStringCompareNE() {
            var northwind = this.GetContext();
            var cmpLE = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Seattle") != 0);
            var cmpEQ = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Berlin") != 0);
            var cmpGT = northwind.Customers.Where(c => c.CustomerID == "ALFKI").OrderBy(c => c.CustomerID).SingleOrDefault(c => string.Compare(c.Address.City, "Aaa") != 0);
            Assert.AreNotEqual(null, cmpLE);
            Assert.AreEqual(null, cmpEQ);
            Assert.AreNotEqual(null, cmpGT);
        }

        [TestMethod]
        public void TestIntCompareTo() {
            var northwind = this.GetContext();
            // prove that x.CompareTo(y) works for types other than string
            var eq = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => (c.CustomerID == "ALFKI" ? 10 : 10).CompareTo(10));
            var gt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => (c.CustomerID == "ALFKI" ? 10 : 10).CompareTo(9));
            var lt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => (c.CustomerID == "ALFKI" ? 10 : 10).CompareTo(11));
            Assert.AreEqual(0, eq);
            Assert.AreEqual(1, gt);
            Assert.AreEqual(-1, lt);
        }

        [TestMethod]
        public void TestDecimalCompare() {
            var northwind = this.GetContext();
            // prove that type.Compare(x,y) works with decimal
            var eq = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Compare((c.CustomerID == "ALFKI" ? 10m : 10m), 10m));
            var gt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Compare((c.CustomerID == "ALFKI" ? 10m : 10m), 9m));
            var lt = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Compare((c.CustomerID == "ALFKI" ? 10m : 10m), 11m));
            Assert.AreEqual(0, eq);
            Assert.AreEqual(1, gt);
            Assert.AreEqual(-1, lt);
        }

        [TestMethod]
        public void TestDecimalRoundDefault() {
            var northwind = this.GetContext();
            var four = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Round((c.CustomerID == "ALFKI" ? 3.4m : 3.4m)));
            var six = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => decimal.Round((c.CustomerID == "ALFKI" ? 3.5m : 3.5m)));
            Assert.AreEqual(3.0m, four);
            Assert.AreEqual(4.0m, six);
        }

        [TestMethod]
        public void TestDecimalLT() {
            var northwind = this.GetContext();
            // prove that decimals are treated normally with respect to normal comparison operators
            var alfki = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 1.0m : 3.0m) < 2.0m);
            Assert.AreNotEqual(null, alfki);
        }

        [TestMethod]
        public void TestIntLessThan() {
            var northwind = this.GetContext();
            var alfki = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 1 : 3) < 2);
            var alfkiN = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 3 : 1) < 2);
            Assert.AreNotEqual(null, alfki);
            Assert.AreEqual(null, alfkiN);
        }

        [TestMethod]
        public void TestIntLessThanOrEqual() {
            var northwind = this.GetContext();
            var alfki = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 1 : 3) <= 2);
            var alfki2 = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 2 : 3) <= 2);
            var alfkiN = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 3 : 1) <= 2);
            Assert.AreNotEqual(null, alfki);
            Assert.AreNotEqual(null, alfki2);
            Assert.AreEqual(null, alfkiN);
        }

        [TestMethod]
        public void TestIntGreaterThan() {
            var northwind = this.GetContext();
            var alfki = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 3 : 1) > 2);
            var alfkiN = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 1 : 3) > 2);
            Assert.AreNotEqual(null, alfki);
            Assert.AreEqual(null, alfkiN);
        }

        [TestMethod]
        public void TestIntGreaterThanOrEqual() {
            var northwind = this.GetContext();
            var alfki = northwind.Customers.OrderBy(c => c.CustomerID).Single(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 3 : 1) >= 2);
            var alfki2 = northwind.Customers.OrderBy(c => c.CustomerID).Single(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 3 : 2) >= 2);
            var alfkiN = northwind.Customers.OrderBy(c => c.CustomerID).OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 1 : 3) > 2);
            Assert.AreNotEqual(null, alfki);
            Assert.AreNotEqual(null, alfki2);
            Assert.AreEqual(null, alfkiN);
        }

        [TestMethod]
        public void TestIntEqual() {
            var northwind = this.GetContext();
            var alfki = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 1 : 1) == 1);
            var alfkiN = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 1 : 1) == 2);
            Assert.AreNotEqual(null, alfki);
            Assert.AreEqual(null, alfkiN);
        }

        [TestMethod]
        public void TestIntNotEqual() {
            var northwind = this.GetContext();
            var alfki = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 2 : 2) != 1);
            var alfkiN = northwind.Customers.OrderBy(c => c.CustomerID).SingleOrDefault(c => c.CustomerID == "ALFKI" && (c.CustomerID == "ALFKI" ? 2 : 2) != 2);
            Assert.AreNotEqual(null, alfki);
            Assert.AreEqual(null, alfkiN);
        }

        [TestMethod]
        public void TestIntAdd() {
            var northwind = this.GetContext();
            var three = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 1 : 1) + 2);
            Assert.AreEqual(3, three);
        }

        [TestMethod]
        public void TestIntSubtract() {
            var northwind = this.GetContext();
            var negone = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 1 : 1) - 2);
            Assert.AreEqual(-1, negone);
        }

        [TestMethod]
        public void TestIntMultiply() {
            var northwind = this.GetContext();
            var six = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 2 : 2) * 3);
            Assert.AreEqual(6, six);
        }

        [TestMethod]
        public void TestIntDivide() {
            var northwind = this.GetContext();
            var one = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 3.0 : 3.0) / 2);
            Assert.AreEqual(1.5, one);
        }

        [TestMethod]
        public void TestIntModulo() {
            var northwind = this.GetContext();
            var three = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 7 : 7) % 4);
            Assert.AreEqual(3, three);
        }

        [TestMethod]
        public void TestIntBitwiseAnd() {
            var northwind = this.GetContext();
            var band = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 6 : 6) & 3);
            Assert.AreEqual(2, band);
        }

        [TestMethod]
        public void TestIntBitwiseOr() {
            var northwind = this.GetContext();
            var eleven = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 10 : 10) | 3);
            Assert.AreEqual(11, eleven);
        }

        [TestMethod]
        public void TestIntBitwiseExclusiveOr() {
            var northwind = this.GetContext();
            var zero = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ((c.CustomerID == "ALFKI") ? 1 : 1) ^ 1);
            Assert.AreEqual(0, zero);
        }

        [TestMethod]
        public void TestIntBitwiseNot() {
            var northwind = this.GetContext();
            var bneg = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => ~((c.CustomerID == "ALFKI") ? -1 : -1));
            Assert.AreEqual(~-1, bneg);
        }

        [TestMethod]
        public void TestIntNegate() {
            var northwind = this.GetContext();
            var neg = northwind.Customers.Where(c => c.CustomerID == "ALFKI").Sum(c => -((c.CustomerID == "ALFKI") ? 1 : 1));
            Assert.AreEqual(-1, neg);
        }

        [TestMethod]
        public void TestAnd() {
            var northwind = this.GetContext();

            var custs = northwind.Customers.Where(c => c.Address.Country == "USA" && c.Address.City.StartsWith("A")).Select(c => c.Address.City).ToList();
            Assert.AreEqual(2, custs.Count);
            Assert.IsTrue(custs.All(c => c.StartsWith("A")));
        }

        [TestMethod]
        public void TestOr() {
            var northwind = this.GetContext();
            var custs = northwind.Customers.Where(c => c.Address.Country == "USA" || c.Address.City.StartsWith("A")).Select(c => c.Address.City).ToList();
            Assert.AreEqual(14, custs.Count);
        }

        [TestMethod]
        public void TestNot() {
            var northwind = this.GetContext();
            var custs = northwind.Customers.Where(c => !(c.Address.Country == "USA")).Select(c => c.Address.Country).ToList();
            Assert.AreEqual(78, custs.Count);
        }

        [TestMethod]
        public void TestEqualLiteralNull() {
            var northwind = GetContext();
            var query = northwind.Customers.Select(c => c.CustomerID == "ALFKI" ? null : c.CustomerID).Where(x => x == null);
            var queryText = query.ToString();
            
            TestContext.WriteLine(queryText);
            
            Assert.IsTrue(queryText.Contains("IS NULL"));
            Assert.AreEqual(1, query.Count());
        }

        [TestMethod]
        public void TestEqualLiteralNullReversed() {
            var northwind = GetContext();
            var query = northwind.Customers.Select(c => c.CustomerID == "ALFKI" ? null : c.CustomerID).Where(x => null == x);
            var queryText = query.ToString();

            Assert.IsTrue(queryText.Contains("IS NULL"));
            Assert.AreEqual(1, query.Count());
        }

        [TestMethod]
        public void TestNotEqualLiteralNull() {
            var northwind = GetContext();
            var query = northwind.Customers.Select(c => c.CustomerID == "ALFKI" ? null : c.CustomerID).Where(x => x != null);
            var queryText = query.ToString();

            Assert.IsTrue(queryText.Contains("IS NOT NULL"));
            Assert.AreEqual(90, query.Count());
        }

        [TestMethod]
        public void TestNotEqualLiteralNullReversed() {
            var northwind = GetContext();
            var query = northwind.Customers.Select(c => c.CustomerID == "ALFKI" ? null : c.CustomerID).Where(x => null != x);
            var queryText = query.ToString();

            Assert.IsTrue(queryText.Contains("IS NOT NULL"));
            Assert.AreEqual(90, query.Count());
        }

        [TestMethod]
        public void TestSelectManyJoined() {
            var northwind = this.GetContext();

            var cods =
                (from c in northwind.Customers
                 from o in northwind.Orders.Where(o => o.Customer.CustomerID == c.CustomerID)
                 select new { c.ContactName, o.OrderDate }).ToList();
            Assert.AreEqual(830, cods.Count);
        }

        [TestMethod]
        public void TestSelectManyJoinedDefaultIfEmpty() {
            var northwind = this.GetContext();

            var cods = (
                from c in northwind.Customers
                from o in northwind.Orders.Where(o => o.Customer.CustomerID == c.CustomerID).DefaultIfEmpty()
                select new { ContactName = c.ContactName, OrderDate = (o == null ? (DateTime?)null : o.OrderDate) }
                ).ToList();
            Assert.AreEqual(832, cods.Count);
        }

        [TestMethod]
        public void TestSelectWhereAssociation() {
            var northwind = this.GetContext();

            var ords = (
                from o in northwind.Orders
                where o.Customer.Address.City == "Seattle"
                select o
                ).ToList();

            Assert.AreEqual(14, ords.Count);
        }

        [TestMethod]
        public void TestSelectWhereAssociationTwice() {
            var northwind = this.GetContext();

            var n = northwind.Orders.Where(c => c.Customer.CustomerID == "WHITC").Count();
            var ords = (
                from o in northwind.Orders
                where o.Customer.Address.Country == "USA" && o.Customer.Address.City == "Seattle"
                select o
                ).ToList();

            Assert.AreEqual(n, ords.Count);
        }

        [TestMethod]
        public void TestSelectAssociation() {
            var northwind = this.GetContext();

            var custs = (
                from o in northwind.Orders
                where o.Customer.CustomerID == "ALFKI"
                select o.Customer
                ).ToList();

            Assert.AreEqual(6, custs.Count);
            Assert.IsTrue(custs.All(c => c.CustomerID == "ALFKI"));
        }

        [TestMethod]
        public void TestSelectAssociations() {
            var northwind = this.GetContext();

            var doubleCusts = (
                from o in northwind.Orders
                where o.Customer.CustomerID == "ALFKI"
                select new { A = o.Customer, B = o.Customer }
                ).ToList();

            Assert.AreEqual(6, doubleCusts.Count);
            Assert.IsTrue(doubleCusts.All(c => c.A.CustomerID == "ALFKI" && c.B.CustomerID == "ALFKI"));
        }

        [TestMethod]
        public void TestSelectAssociationsWhereAssociations() {
            var northwind = this.GetContext();

            var stuff = (
                from o in northwind.Orders
                where o.Customer.Address.Country == "USA"
                where o.Customer.Address.City != "Seattle"
                select new { A = o.Customer, B = o.Customer }
                ).ToList();

            Assert.AreEqual(108, stuff.Count);
        }

        [TestMethod]
        public void TestCustomersIncludeOrders() {
            var northwind = this.GetContext();

            var custs = northwind.Customers.Include("Orders").Where(c => c.CustomerID == "ALFKI").ToList();
            Assert.AreEqual(1, custs.Count);
            Assert.AreNotEqual(null, custs[0].Orders);
            Assert.AreEqual(6, custs[0].Orders.Count);
        }

        [TestMethod]
        public void TestCustomersIncludeOrdersAndDetails() {
            var northwind = this.GetContext();

            var custs = northwind.Customers.Include("Orders").Include("Orders.OrderDetails").Where(c => c.CustomerID == "ALFKI").ToList();
            Assert.AreEqual(1, custs.Count);
            Assert.AreNotEqual(null, custs[0].Orders);
            Assert.AreEqual(6, custs[0].Orders.Count);
            Assert.IsTrue(custs[0].Orders.Any(o => o.OrderID == 10643));
            Assert.AreNotEqual(null, custs[0].Orders.Single(o => o.OrderID == 10643).OrderDetails);
            Assert.AreEqual(3, custs[0].Orders.Single(o => o.OrderID == 10643).OrderDetails.Count);
        }
    }
}
