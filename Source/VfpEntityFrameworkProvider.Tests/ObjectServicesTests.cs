using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class ObjectServicesTests : TestBase {
        [TestMethod]
        public void CRUDTest() {
            Create();
            Read();
            Update();
            Delete();
        }

        private void Delete() {
            var context = GetContext();

            foreach (var category in context.Categories.Where(c => c.CategoryName == "X")) {
                context.Categories.Remove(category);
            }
            context.SaveChanges();
        }

        private void Update() {
            var context = this.GetContext();

            Category c = (from o in context.Categories
                          where o.CategoryName == "X"
                          orderby o.CategoryID
                          select o).First();
            c.Description = "Some description " + DateTime.Now.ToString();
            context.SaveChanges();
        }

        private void Read() {
            var context = this.GetContext();

            Category c = (from o in context.Categories
                          where o.CategoryName == "X"
                          orderby o.CategoryID
                          select o).First();
        }

        private void Create() {
            var context = this.GetContext();
            Category c = new Category();

            Assert.AreEqual(0, c.CategoryID);
            c.CategoryName = "X";
            context.Categories.Add(c);
            context.SaveChanges();
            Assert.AreNotEqual(0, c.CategoryID);
        }
    }
}
