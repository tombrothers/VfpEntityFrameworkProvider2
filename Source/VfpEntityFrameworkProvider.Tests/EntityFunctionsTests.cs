using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class DbFunctionsTests : TestBase {
        private const int YEAR = 2011;
        private const int MONTH = 5;
        private const int DAY = 29;
        private const int HOUR = 4;
        private const int MINUTE = 37;
        private const int SECOND = 58;
        private readonly DateTime TestDateTime;
        private readonly DateTimeOffset TestDateTimeOffset;
        private readonly TimeSpan TestTimeSpan;
        private readonly int TotalDays;
 
        public DbFunctionsTests() {
            this.TestDateTime = new DateTime(YEAR, MONTH, DAY, HOUR, MINUTE, SECOND);
            this.TestDateTimeOffset = new DateTimeOffset(this.TestDateTime);
            var timespan = this.TestDateTime.Subtract(new DateTime());
            this.TotalDays = (int)timespan.TotalDays;
            this.TestTimeSpan = new TimeSpan(this.TotalDays, HOUR, MINUTE, SECOND);
        }

        [TestMethod]
        public void DbFunctionsTests_DiffYears_DateTime_Test() {
            DateTime testDateTime2 = this.TestDateTime.AddYears(1);
            var result = this.GetOrderQuery().Select(x => DbFunctions.DiffYears(this.TestDateTime, testDateTime2)).First();

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMonths_DateTime_Test() {
            DateTime testDateTime2 = this.TestDateTime.AddMonths(1);
            var result = this.GetOrderQuery().Select(x => DbFunctions.DiffMonths(this.TestDateTime, testDateTime2)).First();

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void DbFunctionsTests_DiffDays_DateTime_Test() {
            DateTime testDateTime2 = this.TestDateTime.AddDays(1);
            var result = this.GetOrderQuery().Select(x => DbFunctions.DiffDays(this.TestDateTime, testDateTime2)).First();

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void DbFunctionsTests_DiffHours_DateTime_Test() {
            DateTime testDateTime2 = this.TestDateTime.AddHours(1);

            var result = this.GetOrderQuery().Select(x => DbFunctions.DiffHours(this.TestDateTime, testDateTime2)).First();

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMinutes_DateTime_Test() {
            DateTime testDateTime2 = this.TestDateTime.AddMinutes(1);
            var result = this.GetOrderQuery().Select(x => DbFunctions.DiffMinutes(this.TestDateTime, testDateTime2)).First();

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void DbFunctionsTests_DiffSeconds_DateTime_Test() {
            DateTime testDateTime2 = this.TestDateTime.AddSeconds(1);
            var result = this.GetOrderQuery().Select(x => DbFunctions.DiffSeconds(this.TestDateTime, testDateTime2)).First();

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void DbFunctionsTests_TruncateTime_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime)DbFunctions.TruncateTime(DbFunctions.CreateDateTime(YEAR, MONTH, DAY, HOUR, MINUTE, SECOND))).First();

            Assert.AreEqual(YEAR, result.Year);
            Assert.AreEqual(MONTH, result.Month);
            Assert.AreEqual(DAY, result.Day);
        }

        [TestMethod]
        public void DbFunctionsTests_Right_Test() {
            var result = this.GetOrderQuery().Select(x => DbFunctions.Right(x.Customer.CustomerID, 2)).First();
            Assert.AreEqual("ET", result);
        }

        [TestMethod]
        public void DbFunctionsTests_Left_Test() {
            var result = this.GetOrderQuery().Select(x => DbFunctions.Left(x.Customer.CustomerID, 2)).First();
            Assert.AreEqual("VI", result);
        }    

        [TestMethod]
        public void DbFunctionsTests_CreateDateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime) DbFunctions.CreateDateTime(YEAR, MONTH, DAY, HOUR, MINUTE, SECOND)).First();

            Assert.AreEqual(YEAR, result.Year);
            Assert.AreEqual(MONTH, result.Month);
            Assert.AreEqual(DAY, result.Day);
            Assert.AreEqual(HOUR, result.Hour);
            Assert.AreEqual(MINUTE, result.Minute);
            Assert.AreEqual(SECOND, result.Second);
        }
        
        [TestMethod]
        public void DbFunctionsTests_AddYears_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime)DbFunctions.AddYears(this.TestDateTime, 1)).First();

            Assert.AreEqual(YEAR + 1, result.Year);
            Assert.AreEqual(MONTH, result.Month);
            Assert.AreEqual(DAY, result.Day);
            Assert.AreEqual(HOUR, result.Hour);
            Assert.AreEqual(MINUTE, result.Minute);
            Assert.AreEqual(SECOND, result.Second);
        }

        [TestMethod]
        public void DbFunctionsTests_AddMonths_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime)DbFunctions.AddMonths(this.TestDateTime, 1)).First();

            Assert.AreEqual(YEAR, result.Year);
            Assert.AreEqual(MONTH + 1, result.Month);
            Assert.AreEqual(DAY, result.Day);
            Assert.AreEqual(HOUR, result.Hour);
            Assert.AreEqual(MINUTE, result.Minute);
            Assert.AreEqual(SECOND, result.Second);
        }
        
        [TestMethod]
        public void DbFunctionsTests_AddDays_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime)DbFunctions.AddDays(this.TestDateTime, 1)).First();

            Assert.AreEqual(YEAR, result.Year);
            Assert.AreEqual(MONTH, result.Month);
            Assert.AreEqual(DAY + 1, result.Day);
            Assert.AreEqual(HOUR, result.Hour);
            Assert.AreEqual(MINUTE, result.Minute);
            Assert.AreEqual(SECOND, result.Second);
        }

        [TestMethod]
        public void DbFunctionsTests_AddHours_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime)DbFunctions.AddHours(this.TestDateTime, 1)).First();

            Assert.AreEqual(YEAR, result.Year);
            Assert.AreEqual(MONTH, result.Month);
            Assert.AreEqual(DAY, result.Day);
            Assert.AreEqual(HOUR + 1, result.Hour);
            Assert.AreEqual(MINUTE, result.Minute);
            Assert.AreEqual(SECOND, result.Second);
        }

        [TestMethod]
        public void DbFunctionsTests_AddMinutes_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime)DbFunctions.AddMinutes(this.TestDateTime, 1)).First();

            Assert.AreEqual(YEAR, result.Year);
            Assert.AreEqual(MONTH, result.Month);
            Assert.AreEqual(DAY, result.Day);
            Assert.AreEqual(HOUR, result.Hour);
            Assert.AreEqual(MINUTE + 1, result.Minute);
            Assert.AreEqual(SECOND, result.Second);
        }

        [TestMethod]
        public void DbFunctionsTests_AddSeconds_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => (DateTime)DbFunctions.AddSeconds(this.TestDateTime, 1)).First();

            Assert.AreEqual(YEAR, result.Year);
            Assert.AreEqual(MONTH, result.Month);
            Assert.AreEqual(DAY, result.Day);
            Assert.AreEqual(HOUR, result.Hour);
            Assert.AreEqual(MINUTE, result.Minute);
            Assert.AreEqual(SECOND + 1, result.Second);
        }

        #region NotSupported

        // I don't believe that some of these Not Supported functions can be implemented due to VFP limitations.  However, there are some that I think that could
        // be implemented but figured the effort was too much for a first attempt of creating the EF provider.

        [TestMethod]
        public void DbFunctionsTests_DiffNanoseconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffNanoseconds(this.TestTimeSpan, this.TestTimeSpan)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffNanoseconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffNanoseconds(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffNanoseconds_DateTime_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffNanoseconds(this.TestDateTime, this.TestDateTime)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMicroseconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMilliseconds(this.TestTimeSpan, this.TestTimeSpan)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMicroseconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMicroseconds(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMicroseconds_DateTime_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMicroseconds(this.TestDateTime, this.TestDateTime)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMilliseconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMilliseconds(this.TestTimeSpan, this.TestTimeSpan)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMilliseconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMilliseconds(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMilliseconds_DateTime_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMilliseconds(this.TestDateTime, this.TestDateTime)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffYears_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffYears(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMonths_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMonths(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffDays_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffDays(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffHours_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffHours(this.TestTimeSpan, this.TestTimeSpan)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffHours_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffHours(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMinutes_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMinutes(this.TestTimeSpan, this.TestTimeSpan)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffMinutes_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffMinutes(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffSeconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffSeconds(this.TestTimeSpan, this.TestTimeSpan)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_DiffSeconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.DiffSeconds(this.TestDateTimeOffset, this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_Truncate_Double_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.Truncate((double)123.45, 2)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_Truncate_Decimal_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.Truncate((decimal)123.45, 2)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_TruncateTime_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMilliseconds(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_StandardDeviation_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.StandardDeviation(x.OrderDetails.Select(d => d.UnitPrice))).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_VarP_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.VarP(x.OrderDetails.Select(d => d.UnitPrice))).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_Var_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.Var(x.OrderDetails.Select(d => d.UnitPrice))).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_Reverse_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.Reverse("test")).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_GetTotalOffsetMinutes_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.GetTotalOffsetMinutes(this.TestDateTimeOffset)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_CreateTime_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.CreateTime(HOUR, MINUTE, SECOND)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_CreateDateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.CreateDateTimeOffset(YEAR, MONTH, DAY, HOUR, MINUTE, SECOND, -5)).First();
            });
        }

        /// Not sure what is going on with these two functions.  I'm not able to hook into the expression tree to throw a NotSupportedException.
        //[TestMethod]
        //public void DbFunctionsTests_AsUnicode_Test() {
        //    this.AssertException<NotSupportedException>(() => {
        //        this.GetOrderQuery().Select(x => DbFunctions.AsUnicode("test")).First();
        //    });
        //}

        //[TestMethod]
        //public void DbFunctionsTests_AsNonUnicode_Test() {
        //    this.AssertException<NotSupportedException>(() => {
        //        this.GetOrderQuery().Select(x => DbFunctions.AsNonUnicode("test")).First();
        //    });
        //}

        [TestMethod]
        public void DbFunctionsTests_AddNanoseconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddNanoseconds(this.TestTimeSpan, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddNanoseconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddNanoseconds(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddNanoseconds_DateTime_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddNanoseconds(this.TestDateTime, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMilliseconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMilliseconds(this.TestTimeSpan, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMilliseconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMilliseconds(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMilliseconds_DateTime_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMilliseconds(this.TestDateTime, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMicroseconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMicroseconds(this.TestTimeSpan, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMicroseconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMicroseconds(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMicroseconds_DateTime_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMicroseconds(this.TestDateTime, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddYears_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddYears(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMonths_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMonths(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddDays_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddDays(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddHours_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddHours(this.TestTimeSpan, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddHours_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddHours(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMinutes_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMinutes(this.TestTimeSpan, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddMinutes_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddMinutes(this.TestDateTimeOffset, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddSeconds_TimeSpan_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddSeconds(this.TestTimeSpan, 1)).First();
            });
        }

        [TestMethod]
        public void DbFunctionsTests_AddSeconds_DateTimeOffset_Test() {
            this.AssertException<NotSupportedException>(() => {
                this.GetOrderQuery().Select(x => DbFunctions.AddSeconds(this.TestDateTimeOffset, 1)).First();
            });
        }

        #endregion
    }
}
