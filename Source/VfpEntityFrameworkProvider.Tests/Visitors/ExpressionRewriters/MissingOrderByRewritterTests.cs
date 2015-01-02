using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VfpEntityFrameworkProvider.Tests.Visitors.ExpressionRewriters {
    [TestClass]
    public class MissingOrderByRewritterTests : TestBase {
        [TestMethod]
        public void CompoundKey_FindTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Users.Find("First0", "Last0");

                Assert.AreEqual("First0", entity.FirstName);
                Assert.AreEqual("Last0", entity.LastName);
            }
        }

        [TestMethod]
        public void SingleKey_FindTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.Find(1);

                Assert.AreEqual("Artist0", entity.Name);
            }
        }

        [TestMethod]
        public void CompoundKey_FirstTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Users.First();

                Assert.AreEqual("First0", entity.FirstName);
                Assert.AreEqual("Last0", entity.LastName);
            }
        }

        [TestMethod]
        public void SingleKey_FirstTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.First();

                Assert.AreEqual("Artist0", entity.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SingleTest() {
            using (var context = GetCodeFirstContext()) {
                context.Artists.SingleOrDefault();
            }
        }

        [TestMethod]
        public void CompoundKey_TakeTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Users.Take(1).ToList()[0];

                Assert.AreEqual("First0", entity.FirstName);
                Assert.AreEqual("Last0", entity.LastName);
            }
        }

        [TestMethod]
        public void SingleKey_TakeTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.Take(1).ToList()[0];

                Assert.AreEqual("Artist0", entity.Name);
            }
        }

        [TestMethod]
        public void CompoundKey_SingleFirstTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Users.First(x => x.FirstName == "First0" && x.LastName == "Last0");

                Assert.AreEqual("First0", entity.FirstName);
                Assert.AreEqual("Last0", entity.LastName);
            }
        }

        [TestMethod]
        public void SingleKey_FirstFilterTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.First(x => x.ArtistId == 1);

                Assert.AreEqual("Artist0", entity.Name);
            }
        }

        [TestMethod]
        public void CompoundKey_WhereSFirstTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Users.Where(x => x.FirstName == "First0" && x.LastName == "Last0").First();

                Assert.AreEqual("First0", entity.FirstName);
                Assert.AreEqual("Last0", entity.LastName);
            }
        }

        [TestMethod]
        public void SingleKey_WhereFirstTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.Where(x => x.ArtistId == 1).First();

                Assert.AreEqual("Artist0", entity.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void LastNotSupportedTest() {
            using (var context = GetCodeFirstContext()) {
                context.Artists.Last();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void LastFilterNotSupportedTest() {
            using (var context = GetCodeFirstContext()) {
                context.Artists.Last(x => x.ArtistId == 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void WhereLastNotSupportedTest() {
            using (var context = GetCodeFirstContext()) {
                context.Artists.Where(x => x.ArtistId == 1).Last();
            }
        }
        
        [TestMethod]
        public void CompoundKey_SingleFilterTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Users.Single(x => x.FirstName == "First0" && x.LastName == "Last0");

                Assert.AreEqual("First0", entity.FirstName);
                Assert.AreEqual("Last0", entity.LastName);
            }
        }

        [TestMethod]
        public void SingleKey_SingleFilterTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.Single(x => x.ArtistId == 1);

                Assert.AreEqual("Artist0", entity.Name);
            }
        }

        [TestMethod]
        public void CompoundKey_WhereSingleTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Users.Where(x => x.FirstName == "First0" && x.LastName == "Last0").Single();

                Assert.AreEqual("First0", entity.FirstName);
                Assert.AreEqual("Last0", entity.LastName);
            }
        }

        [TestMethod]
        public void SingleKey_WhereSingleTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.Where(x => x.ArtistId == 1).Single();

                Assert.AreEqual("Artist0", entity.Name);
            }
        }
    }
}