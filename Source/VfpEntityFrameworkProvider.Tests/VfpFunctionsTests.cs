using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public class VfpFunctionsTests : TestBase {
        [TestMethod]
        public void VfpFunctionsTests_IsDigit_True_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.IsDigit("1")).First();
            Assert.AreEqual(true, result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.IsDigit("A")).First();
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Sec_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Sec(x.OrderDate)).First();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_MonthName_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.MonthName(x.OrderDate)).First();
            Assert.AreEqual("July", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_DateTime_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.DateTime()).First();
            Assert.AreEqual(DateTime.Now.ToShortDateString(), result.Value.ToShortDateString());
        }

        [TestMethod]
        public void VfpFunctionsTests_DayOfWeek_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.DayOfWeek(x.OrderDate)).First();
            Assert.AreEqual("Thursday", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Tan_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Tan((double).5)).First();
            Assert.AreEqual(0.54630248984379, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Tan_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Tan((decimal).5)).First();
            Assert.AreEqual(0.54630248984379, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_SquareRoot_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.SquareRoot((double)4)).First();
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_SquareRoot_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.SquareRoot((decimal)4)).First();
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Sin_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Sin((decimal).5)).First();
            Assert.AreEqual(0.4794255386042, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Sin_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Sin((double).5)).First();
            Assert.AreEqual(0.4794255386042, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Sign_Long_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Sign((long)0)).First();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Sign_Int_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Sign(0)).First();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Sign_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Sign((double)-99)).First();
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Sign_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Sign((decimal)99)).First();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Rand_Seed_Test() {
            var result = (int)this.GetOrderQuery().Select(x => VfpFunctions.Rand(1) * 100).First();
            Assert.AreNotEqual(0, result);
        }     

        [TestMethod]
        public void VfpFunctionsTests_Rand_Test() {
            var result = (int) this.GetOrderQuery().Select(x => VfpFunctions.Rand() * 100).First();
            Assert.AreNotEqual(0, result);
        }        

        [TestMethod]
        public void VfpFunctionsTests_Dtor_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Dtor((double)90)).First();
            Assert.AreEqual(1.5707963267949, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Dtor_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Dtor((decimal)90)).First();
            Assert.AreEqual(1.5707963267949M, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_PI_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Pi()).First();
            Assert.AreEqual(3.14159265358979, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Log10_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Log10((double).5)).First();
            Assert.AreEqual(-0.30102999566398, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Log10_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Log10((decimal).5)).First();
            Assert.AreEqual(-0.30102999566398, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Log_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Log((double).5)).First();
            Assert.AreEqual(-0.69314718055995, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Log_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Log((decimal).5)).First();
            Assert.AreEqual(-0.69314718055995, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Exp_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Exp((double)1)).First();
            Assert.AreEqual(2.71828182845905, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Exp_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Exp((decimal)1)).First();
            Assert.AreEqual(2.71828182845905, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Rtod_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Rtod((double)1)).First();
            Assert.AreEqual(57.29577951308230, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Rtod_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Rtod((decimal)1)).First();
            Assert.AreEqual(57.29577951308230M, result);
        }
        
        [TestMethod]
        public void VfpFunctionsTests_Cos_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Cos((double).5)).First();
            Assert.AreEqual(0.87758256189037, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Cos_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Cos((decimal).5)).First();
            Assert.AreEqual(0.87758256189037, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Atn2_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Atn2((decimal).5, (decimal).5)).First();
            Assert.AreEqual(0.78539816339745, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Atn2_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Atn2((double).5, (double).5)).First();
            Assert.AreEqual(0.78539816339745, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Atan_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Atan((decimal).5)).First();
            Assert.AreEqual(0.46364760900081, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Atan_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Atan((double).5)).First();
            Assert.AreEqual(0.46364760900081, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Asin_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Asin((decimal).5)).First();
            Assert.AreEqual(0.5235987755983, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Asin_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Asin((double).5)).First();
            Assert.AreEqual(0.5235987755983, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Acos_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Acos((decimal).5)).First();
            Assert.AreEqual(1.0471975511966, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Acos_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Acos((double).5)).First();
            Assert.AreEqual(1.0471975511966, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Substr_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Substr(x.ShipName, 9, 7)).First();
            Assert.AreEqual("alcools", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Stuff_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Stuff(x.ShipName, 6, 2, "xx")).First();
            Assert.AreEqual("Vins xx alcools Chevalier", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_StringConvert_Decimal_Length__DecimalPlaces_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.StringConvert((decimal)12345.67, 15, 1)).First();
            Assert.AreEqual("        12345.7", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_StringConvert_Double_Length_DecimalPlaces_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.StringConvert((double)12345.67, 15, 1)).First();
            Assert.AreEqual("        12345.7", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_StringConvert_Decimal_Length_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.StringConvert((decimal)12345.67, 15)).First();
            Assert.AreEqual("          12346", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_StringConvert_Double_Length_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.StringConvert((double)12345.67, 15)).First();
            Assert.AreEqual("          12346", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_StringConvert_Decimal_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.StringConvert((decimal)12345.67)).First();
            Assert.AreEqual("     12346", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_StringConvert_Double_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.StringConvert((double)12345.67)).First();
            Assert.AreEqual("     12346", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Space_Test() {
            var result = this.GetOrderQuery().Select(x => "x" + VfpFunctions.Space(3) + "x").First();
            Assert.AreEqual("x   x", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_AllTrimTest() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.AllTrim("  " + x.Customer.CustomerID)).First();
            Assert.AreEqual("VINET", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Replicate_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Replicate("Z", 3)).First();
            Assert.AreEqual("ZZZ", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Strtran_CaseMatch_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "I", "X", 1, 1, 2)).First();
            Assert.AreEqual("Vins et alcools Chevalier", result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "i", "X", 1, 1, 2)).First();
            Assert.AreEqual("Vxns et alcools Chevalier", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Strtran_CaseInsensitive_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "I", "X", 1, -1, 1)).First();
            Assert.AreEqual("VXns et alcools ChevalXer", result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "i", "X", 1, -1, 1)).First();
            Assert.AreEqual("VXns et alcools ChevalXer", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Strtran_NumerOfOccurance_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "I", "X", 1, 1)).First();
            Assert.AreEqual("Vins et alcools Chevalier", result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "i", "X", 1, 1)).First();
            Assert.AreEqual("VXns et alcools Chevalier", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Strtran_StartOccurance_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "I", "X", 2)).First();
            Assert.AreEqual("Vins et alcools Chevalier", result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "i", "X", 2)).First();
            Assert.AreEqual("Vins et alcools ChevalXer", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_Strtran_Default_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "I", "X")).First();
            Assert.AreEqual("Vins et alcools Chevalier", result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Strtran(x.ShipName, "i", "X")).First();
            Assert.AreEqual("VXns et alcools ChevalXer", result);
        }

        [TestMethod]
        public void VfpFunctionsTests_ATC_LongOccurance_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Atc("I", x.ShipName, (long)2)).First();
            Assert.AreEqual(23, result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Atc("i", x.ShipName, (long)2)).First();
            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_ATC_IntOccurance_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Atc("I", x.ShipName, 2)).First();
            Assert.AreEqual(23, result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Atc("i", x.ShipName, 2)).First();
            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_ATC_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Atc("I", x.ShipName)).First();
            Assert.AreEqual(2, result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.Atc("i", x.ShipName)).First();
            Assert.AreEqual(2, result);
        }        

        [TestMethod]
        public void VfpFunctionsTests_AT_LongOccurance_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.At("I", x.ShipName, (long) 2)).First();
            Assert.AreEqual(0, result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.At("i", x.ShipName, (long) 2)).First();
            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_AT_IntOccurance_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.At("I", x.ShipName, 2)).First();
            Assert.AreEqual(0, result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.At("i", x.ShipName, 2)).First();
            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void VfpFunctionsTests_AT_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.At("I", x.ShipName)).First();
            Assert.AreEqual(0, result);
            result = this.GetOrderQuery().Select(x => VfpFunctions.At("i", x.ShipName)).First();
            Assert.AreEqual(2, result);
        }
        
        [TestMethod]
        public void VfpFunctionsTests_Chr_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Chr(65)).First();
            Assert.AreEqual("A", result);
        }

        [TestMethod, Ignore]
        public void VfpFunctionsTests_Ascii_Test() {
            var result = this.GetOrderQuery().Select(x => VfpFunctions.Ascii("A")).First();
            Assert.AreEqual(65, result);
        }
    }
}
