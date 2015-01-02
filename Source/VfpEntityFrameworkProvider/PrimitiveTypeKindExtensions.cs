using System;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider {
    public static class PrimitiveTypeKindExtensions {
        public static Type ToClrType(this PrimitiveTypeKind primitiveTypeKind) {
            switch (primitiveTypeKind) {
                case PrimitiveTypeKind.Binary:
                    return typeof(byte[]);
                case PrimitiveTypeKind.Boolean:
                    return typeof(bool);
                case PrimitiveTypeKind.Byte:
                    return typeof(byte);
                case PrimitiveTypeKind.DateTime:
                case PrimitiveTypeKind.Time:
                    return typeof(DateTime);
                case PrimitiveTypeKind.Decimal:
                    return typeof(decimal);
                case PrimitiveTypeKind.Double:
                    return typeof(double);
                case PrimitiveTypeKind.Single:
                    return typeof(float);
                case PrimitiveTypeKind.Guid:
                    return typeof(Guid);
                case PrimitiveTypeKind.Int16:
                    return typeof(short);
                case PrimitiveTypeKind.Int32:
                    return typeof(int);
                case PrimitiveTypeKind.Int64:
                    return typeof(long);
                case PrimitiveTypeKind.SByte:
                    return typeof(sbyte);
                case PrimitiveTypeKind.String:
                    return typeof(string);
                default:
                    throw new InvalidOperationException(string.Format("Unknown PrimitiveTypeKind {0}", primitiveTypeKind));
            }
        }

        public static TypeUsage ToTypeUsage(this PrimitiveTypeKind primitiveTypeKind) {
            return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(primitiveTypeKind));
        }

        internal static DbType ToDbType(this PrimitiveTypeKind primitiveTypeKind) {
            switch (primitiveTypeKind) {
                case PrimitiveTypeKind.Binary:
                    return DbType.Binary;
                case PrimitiveTypeKind.Boolean:
                    return DbType.Boolean;
                case PrimitiveTypeKind.Byte:
                    return DbType.Byte;
                case PrimitiveTypeKind.DateTime:
                    return DbType.DateTime;
                case PrimitiveTypeKind.Decimal:
                    return DbType.Decimal;
                case PrimitiveTypeKind.Double:
                    return DbType.Double;
                case PrimitiveTypeKind.Single:
                    return DbType.Single;
                case PrimitiveTypeKind.Guid:
                    return DbType.Guid;
                case PrimitiveTypeKind.Int16:
                    return DbType.Int16;
                case PrimitiveTypeKind.Int32:
                    return DbType.Int32;
                case PrimitiveTypeKind.Int64:
                    return DbType.Int64;
                case PrimitiveTypeKind.SByte:
                    return DbType.SByte;
                case PrimitiveTypeKind.String:
                    return DbType.String;
                default:
                    throw new InvalidOperationException(string.Format("Unknown PrimitiveTypeKind {0}", primitiveTypeKind));
            }
        }
    }
}