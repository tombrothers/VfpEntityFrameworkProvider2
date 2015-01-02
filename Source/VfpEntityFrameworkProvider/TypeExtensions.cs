using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider {
    public static class TypeExtensions {
        public static TypeUsage ToTypeUsage(this Type type) {
            return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(type.ToPrimitiveTypeKind()));
        }

        public static PrimitiveTypeKind ToPrimitiveTypeKind(this Type type) {
            if (type == typeof(byte[])) {
                return PrimitiveTypeKind.Binary;
            }

            if (type == typeof(Guid)) {
                return PrimitiveTypeKind.Guid;
            }

            var typeCode = Type.GetTypeCode(type);

            switch (typeCode) {
                case TypeCode.Boolean:
                    return PrimitiveTypeKind.Boolean;
                case TypeCode.Char:
                    return PrimitiveTypeKind.String;
                case TypeCode.SByte:
                    return PrimitiveTypeKind.SByte;
                case TypeCode.Byte:
                    return PrimitiveTypeKind.Byte;
                case TypeCode.Int16:
                    return PrimitiveTypeKind.Int16;
                case TypeCode.UInt16:
                case TypeCode.Int32:
                    return PrimitiveTypeKind.Int32;
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return PrimitiveTypeKind.Int64;
                case TypeCode.Single:
                    return PrimitiveTypeKind.Single;
                case TypeCode.Double:
                    return PrimitiveTypeKind.Double;
                case TypeCode.Decimal:
                    return PrimitiveTypeKind.Decimal;
                case TypeCode.DateTime:
                    return PrimitiveTypeKind.DateTime;
                case TypeCode.String:
                    return PrimitiveTypeKind.String;
                default:
                    throw new NotSupportedException(typeCode.ToString());
            }
        }
    }
}