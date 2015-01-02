using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Tests.Expressions {
    [TestClass]
    public class VfpConstantExpressionTests {
        [TestMethod]
        public void CreateDefaultPrimitiveTypeKindTest() {
            var primitiveTypeKinds = Enum.GetValues(typeof(PrimitiveTypeKind))
                                         .Cast<PrimitiveTypeKind>()
                                         .Where(x => x != PrimitiveTypeKind.Time)
                                         .Where(x => x != PrimitiveTypeKind.DateTimeOffset)
                                         .Where(x => x.ToString().StartsWith("Geometry"))
                                         .Where(x => x.ToString().StartsWith("Geography"));

            foreach (var primitiveTypeKind in primitiveTypeKinds) {
                Console.WriteLine(primitiveTypeKind.ToString());

                var type = primitiveTypeKind.ToClrType();
                var value = GetPrimitiveTypeKindDefaultValue(primitiveTypeKind);
                var text = GetPrimitiveTypeKindDefaultText(primitiveTypeKind);
                var expression = VfpExpressionBuilder.Constant(value, type);

                Assert.AreEqual(primitiveTypeKind, expression.ConstantKind);
                Assert.AreEqual(value, expression.Value);
                Assert.AreEqual(VfpExpressionKind.Constant, expression.ExpressionKind);
                Assert.AreEqual("Edm." + primitiveTypeKind, expression.ResultType.ToString());
            }
        }

        private static string GetPrimitiveTypeKindDefaultText(PrimitiveTypeKind primitiveTypeKind) {
            switch (primitiveTypeKind) {
                case PrimitiveTypeKind.Binary:
                case PrimitiveTypeKind.String:
                    return "null";
                case PrimitiveTypeKind.Byte:
                case PrimitiveTypeKind.Decimal:
                case PrimitiveTypeKind.Double:
                case PrimitiveTypeKind.Single:
                case PrimitiveTypeKind.SByte:
                case PrimitiveTypeKind.Int16:
                case PrimitiveTypeKind.Int32:
                case PrimitiveTypeKind.Int64:
                    return "0";
                case PrimitiveTypeKind.Boolean:
                    return "false";
                case PrimitiveTypeKind.DateTime:
                    return "1/1/0001 12:00:00 AM";
                case PrimitiveTypeKind.Guid:
                    return "00000000-0000-0000-0000-000000000000";
                default:
                    throw new ArgumentOutOfRangeException("primitiveTypeKind");
            }
        }

        private static object GetPrimitiveTypeKindDefaultValue(PrimitiveTypeKind primitiveTypeKind) {
            switch (primitiveTypeKind) {
                case PrimitiveTypeKind.Binary:
                    return default(byte[]);
                case PrimitiveTypeKind.Boolean:
                    return default(bool);
                case PrimitiveTypeKind.Byte:
                    return default(byte);
                case PrimitiveTypeKind.DateTime:
                    return default(DateTime);
                case PrimitiveTypeKind.Decimal:
                    return default(decimal);
                case PrimitiveTypeKind.Double:
                    return default(double);
                case PrimitiveTypeKind.Guid:
                    return default(Guid);
                case PrimitiveTypeKind.Single:
                    return default(float);
                case PrimitiveTypeKind.SByte:
                    return default(sbyte);
                case PrimitiveTypeKind.Int16:
                    return default(short);
                case PrimitiveTypeKind.Int32:
                    return default(int);
                case PrimitiveTypeKind.Int64:
                    return default(long);
                case PrimitiveTypeKind.String:
                    return default(string);
                default:
                    throw new ArgumentOutOfRangeException("primitiveTypeKind");
            }
        }
    }
}