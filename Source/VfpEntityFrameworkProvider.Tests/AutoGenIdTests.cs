using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Tests.Dal.AutoGenId;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class AutoGenIdTests : TestBase {
        [TestMethod]
        public void AutoGenIdTests_CRUDTest() {
            Create();
            Read();
            Update();
            Delete();
        }

        private void Delete() {
            var context = GetAutoGenDataContext();
            var entity = context.AutoGens.FirstOrDefault(x => x.Value == "Y");

            context.AutoGens.Remove(entity);
            context.SaveChanges();

            Assert.IsNull(GetAutoGenDataContext().AutoGens.FirstOrDefault(x => x.Value == "Y"));
        }

        private void Update() {
            var context = GetAutoGenDataContext();
            var entity = context.AutoGens.FirstOrDefault(x => x.Value == "X");

            entity.Value = "Y";
            context.SaveChanges();

            Assert.IsNotNull(GetAutoGenDataContext().AutoGens.FirstOrDefault(x => x.Value == "Y"));
        }

        private void Read() {
            var context = GetAutoGenDataContext();

            Assert.IsNotNull(context.AutoGens.FirstOrDefault(x => x.Value == "X"));
        }

        private void Create() {
            var context = GetAutoGenDataContext();
            var entity = new AutoGen();

            Assert.IsNull(entity.Id);

            entity.Value = "X";
            context.AutoGens.Add(entity);
            context.SaveChanges();

            Assert.IsNotNull(entity.Id);
        }

        private AutoGenDataContext GetAutoGenDataContext() {
            var connectionString = Path.Combine(TestContext.TestDeploymentDir, @"AutoGenId\Data");
            var connection = new VfpConnection(connectionString);

            EnableTracing(connection);

            return new AutoGenDataContext(connection);
        }
    }
}