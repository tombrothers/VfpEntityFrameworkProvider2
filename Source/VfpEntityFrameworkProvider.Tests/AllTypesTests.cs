using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpClient;
using VfpEntityFrameworkProvider.Tests.Dal.AllTypes;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class AllTypesTests : TestBase {
        [TestMethod]
        public void QueryEmptyValues() {
            InsertEmptyValues(false);

            using (var context = GetAllTypesDataContext()) {
                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.BinaryChar) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.BinaryChar) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.BinaryMemo) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.BinaryMemo) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.BinaryVarChar) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.BinaryVarChar) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Char) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Char) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Currency) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Currency) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Date) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Date) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.DateTime) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.DateTime) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Decimal) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Decimal) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Double) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Double) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Float) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Float) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Integer) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Integer) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Logical) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Logical) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Long) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Long) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.Memo) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.Memo) == false));

                Assert.AreEqual(1, context.AllTypes.Count(x => VfpFunctions.Empty(x.VarChar) == true));
                Assert.AreEqual(0, context.AllTypes.Count(x => VfpFunctions.Empty(x.VarChar) == false));  
            }
        }

        [TestMethod]
        public void Inserted_And_Retrieved_EmptyValues_With_SetNullOn() {
            InsertEmptyValues(true);

            using (var context = GetAllTypesDataContext()) {
                var item = context.AllTypes.ToList().First();

                Assert.IsNull(item.BinaryChar);
                Assert.IsNull(item.BinaryMemo);
                Assert.IsNull(item.BinaryVarChar);
                Assert.IsNull(item.Char);
                Assert.IsNull(item.Currency);
                Assert.IsNull(item.Date);
                Assert.IsNull(item.DateTime);
                Assert.IsNull(item.Decimal);
                Assert.IsNull(item.Double);
                Assert.IsNull(item.Float);
                Assert.IsNull(item.Integer);
                Assert.IsNull(item.Logical);
                Assert.IsNull(item.Long);
                Assert.IsNull(item.Memo);
                Assert.IsNull(item.VarChar);
            }
        }

        [TestMethod]
        public void Inserted_And_Retrieved_EmptyValues_With_SetNullOff() {
            InsertEmptyValues(false);

            using (var context = GetAllTypesDataContext()) {
                var item = context.AllTypes.ToList().First();

                Assert.AreEqual(string.Empty, item.BinaryChar);
                Assert.AreEqual(string.Empty, item.BinaryMemo);
                Assert.AreEqual(string.Empty, item.BinaryVarChar);

                Assert.AreEqual(string.Empty, item.Char);
                Assert.AreEqual(0, item.Currency);
                Assert.AreEqual("12/30/1899 12:00:00 AM", item.Date.ToString());
                Assert.AreEqual("12/30/1899 12:00:00 AM", item.DateTime.ToString());
                Assert.AreEqual(0, item.Decimal);
                Assert.AreEqual(0, item.Double);
                Assert.AreEqual(0, item.Float);
                Assert.AreEqual(0, item.Integer);
                Assert.AreEqual(false, item.Logical);
                Assert.AreEqual(0, item.Long);
                Assert.AreEqual(string.Empty, item.Memo);
                Assert.AreEqual(string.Empty, item.VarChar);
            }
        }

        private void InsertEmptyValues(bool allowNulls) {
            using (var context = GetAllTypesDataContext()) {
                var item = new AllTypesTable();
                var builder = new VfpConnectionStringBuilder(context.Database.Connection.ConnectionString);

                builder.Null = allowNulls;

                context.Database.Connection.ConnectionString = builder.ConnectionString;

                item.Guid = Guid.NewGuid();

                context.AllTypes.Add(item);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Inserted_And_Retrieved_Values() {
            var item = new AllTypesTable();
            var testTime = new DateTime(2012, 2, 6, 8, 51, 33);

            using (var context = GetAllTypesDataContext()) {
                item.BinaryChar = "binarychar";
                item.BinaryMemo = "binarymemo";
                item.BinaryVarChar = "binaryvarchar";
                ////item.Blob = Encoding.Default.GetBytes("blob");
                item.Char = "char";
                item.Currency = 1.2M;
                item.Date = testTime;
                item.DateTime = testTime;
                item.Decimal = 3.45M;
                item.Double = 5.6D;
                item.Float = 7.8F;
                item.Guid = new Guid("9211FB02-0654-41B7-82DA-6A38EC0DFD9A");
                item.Integer = 199;
                item.Logical = true;
                item.Long = (long)int.MaxValue + 1;
                item.Memo = "memo";
                item.VarChar = "varchar";
                context.AllTypes.Add(item);
                context.SaveChanges();
            }

            using (var context = GetAllTypesDataContext()) {
                var item2 = context.AllTypes.ToList().First();

                Assert.AreEqual(item.BinaryChar, item2.BinaryChar);
                Assert.AreEqual(item.BinaryMemo, item2.BinaryMemo);
                Assert.AreEqual(item.BinaryVarChar, item2.BinaryVarChar);
                ////Assert.AreEqual(item.Blob, item2.Blob);
                Assert.AreEqual(item.Char, item2.Char);
                Assert.AreEqual(item.Currency, item2.Currency);
                Assert.AreEqual(item.Date.Value.Date, item2.Date);
                Assert.AreEqual(item.DateTime, item2.DateTime);
                Assert.AreEqual(item.Decimal, item2.Decimal);
                Assert.AreEqual(item.Double, item2.Double);
                Assert.AreEqual(item.Float, item2.Float);
                Assert.AreEqual(item.Guid, item2.Guid);
                Assert.AreEqual(item.Integer, item2.Integer);
                Assert.AreEqual(item.Logical, item2.Logical);
                Assert.AreEqual(item.Long, item2.Long);
                Assert.AreEqual(item.Memo, item2.Memo);
                Assert.AreEqual(item.VarChar, item2.VarChar);
            }
        }

        [TestInitialize]
        public void Initialize() {
            GetConnection().Zap("AllTypesTables");
        }

        private AllTypesDataContext GetAllTypesDataContext() {
            return new AllTypesDataContext(GetConnection());
        }

        private new VfpConnection GetConnection() {
            var connection = new VfpConnection(Path.Combine(GetTestDeploymentDir(TestContext), @"AllTypes\AllTypes.dbc"));

            EnableTracing(connection);

            return connection;
        }
    }
}