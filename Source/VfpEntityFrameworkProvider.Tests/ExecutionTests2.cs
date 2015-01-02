using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class ExecutionTests2 : TestBase {
        [TestMethod]
        public void TestInsert() {
            var northwind = this.GetContext();
            Product product = new Product {
                ProductName = "Test" + Environment.NewLine + "1",
            };

            northwind.Products.Add(product);
            northwind.SaveChanges();
            Assert.AreNotEqual(0, product.ProductID);

            product.ProductName = "Test" + Environment.NewLine + "2";
            northwind.SaveChanges();
            product = northwind.Products.OrderBy(x => x.ProductID).SingleOrDefault(x => x.ProductID == product.ProductID);
            Assert.AreEqual("Test" + Environment.NewLine + "2", product.ProductName);

            northwind.Products.Remove(product);
            northwind.SaveChanges();
            product = northwind.Products.OrderBy(x => x.ProductID).SingleOrDefault(x => x.ProductID == product.ProductID);
            Assert.IsNull(product);
        }

        [TestMethod]
        public void TestWhereTrue() {
            var northwind = this.GetContext();
            var x = northwind.Customers.Where(c => true);

            Assert.AreEqual(91, northwind.Customers.Where(c => true).Count());
        }

        [TestMethod]
        public void TestGroupBy() {
            var northwind = this.GetContext();
            Assert.AreEqual(69, northwind.Customers.GroupBy(c => c.Address.City).ToList().Count);
        }

        [TestMethod]
        public void SelectTakeTest() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderBy(x => x.Address.Address).Take(1).ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("1 rue Alsace-Lorraine", list[0].Address.Address);
        }

        [TestMethod]
        public void SelectDistinctTest() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Select(x => x.Address.Country).Distinct().ToList();
            Assert.AreEqual(21, list.Count);
        }

        [TestMethod]
        public void SelectOrderByTest() {
            var northwind = this.GetContext();
            var list = northwind.Customers.OrderBy(x => x.Address.Address).ToList();
            Assert.AreEqual(91, list.Count);
            Assert.AreEqual("1 rue Alsace-Lorraine", list[0].Address.Address);
        }

        [TestMethod]
        public void SelectWhereCustomerIdTest() {
            var northwind = this.GetContext();
            var list = northwind.Customers.Where(item => item.CustomerID == "ALFKI").ToList();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void SelectTest() {
            var northwind = this.GetContext();
            var list = northwind.Customers.ToList();
            Assert.AreEqual(91, list.Count);
        }
    }
}