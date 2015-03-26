using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Tests.Dal.CodeFirst;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class CodeFirstTests : TestBase {
        [TestMethod]
        public void OrderByDescFieldTest() {
            // This test is used to verify that ordering by a Table Field "desc" does not throw an exception.
            using (var context = GetCodeFirstContext()) {
                context.Album.Take(10).OrderBy(x => x.Description).Skip(1).ToList();
            }
        }

        [TestMethod]
        public void TakeSkipTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.Take(10).OrderBy(x => x.ArtistId).Skip(1).ToList()[0];

                Assert.AreEqual("Artist1", entity.Name);
            }
        }

        [TestMethod]
        public void SkipTakeTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.OrderBy(x => x.ArtistId).Skip(1).Take(1).ToList()[0];

                Assert.AreEqual("Artist1", entity.Name);
            }
        }

        [TestMethod]
        public void TakeTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.OrderBy(x => x.ArtistId).Take(1).ToList()[0];

                Assert.AreEqual("Artist0", entity.Name);
            }
        }

        [TestMethod]
        public void SkipTest() {
            using (var context = GetCodeFirstContext()) {
                var entity = context.Artists.OrderBy(x => x.ArtistId).Skip(1).ToList()[0];

                Assert.AreEqual("Artist1", entity.Name);
            }
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext) {
            var connectionString = Path.Combine(testContext.TestDeploymentDir, @"CodeFirstData\CodeFirst.dbc");
            var connection = new VfpConnection(connectionString);
            var context = new CodeFirstContext(connection);

            context.Artists.ToList();
        }
    }
}