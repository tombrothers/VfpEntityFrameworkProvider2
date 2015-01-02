using System;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpClient;

namespace VfpEntityFrameworkProvider {
    internal class VfpParameterHelper {
        internal VfpParameter CreateVfpParameter(string name, TypeUsage type, ParameterMode mode, object value) {
            int? size;

            var result = new VfpParameter(name, value);

            result.Direction = ParameterDirection.Input;
            result.VfpType = GetVfpType(type, out size);

            if (size.HasValue && (result.Size != size.Value)) {
                result.Size = size.Value;
            }

            var isNullable = type.IsNullable();

            if (isNullable != result.IsNullable) {
                result.IsNullable = isNullable;
            }

            return result;
        }

        private static VfpType GetVfpType(TypeUsage type, out int? size) {
            var primitiveTypeKind = type.ToPrimitiveTypeKind();

            size = default(int?);

            switch (primitiveTypeKind) {
                case PrimitiveTypeKind.Binary:
                    size = GetParameterSize(type);
                    return GetBinaryVfpType(type);
                case PrimitiveTypeKind.Boolean:
                    return VfpType.Logical;
                case PrimitiveTypeKind.DateTime:
                    return VfpType.DateTime;
                case PrimitiveTypeKind.Double:
                    return VfpType.Double;
                case PrimitiveTypeKind.Guid:
                    size = VfpMapping.GuidStringWidth;
                    return VfpType.Character;
                case PrimitiveTypeKind.Byte:
                case PrimitiveTypeKind.Int16:
                case PrimitiveTypeKind.Int32:
                case PrimitiveTypeKind.SByte:
                    return VfpType.Integer;
                case PrimitiveTypeKind.Decimal:
                case PrimitiveTypeKind.Int64:
                    return VfpType.Numeric;
                case PrimitiveTypeKind.Single:
                    return VfpType.Float;
                case PrimitiveTypeKind.String:
                    size = GetParameterSize(type);

                    return GetStringVfpType(type, size);

                default:
                    throw new NotSupportedException("PrimitiveTypeKind = " + primitiveTypeKind);
            }
        }

        private static int? GetParameterSize(TypeUsage type) {
            int maxLength;

            return type.TryGetMaxLength(out maxLength) ? maxLength : default(int?);
        }

        private static VfpType GetStringVfpType(TypeUsage type, int? size) {
            Debug.Assert(type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && PrimitiveTypeKind.String == ((PrimitiveType)type.EdmType).PrimitiveTypeKind, "only valid for string type");

            if (type.EdmType.Name.ToLowerInvariant() == "xml") {
                return VfpType.Memo;
            }

            bool fixedLength;

            if (!type.TryGetIsFixedLength(out fixedLength)) {
                fixedLength = false;
            }

            return VfpMapping.GetVfpStringType(size ?? 0, fixedLength);
        }

        private static VfpType GetBinaryVfpType(TypeUsage type) {
            Debug.Assert(type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType &&
                PrimitiveTypeKind.Binary == ((PrimitiveType)type.EdmType).PrimitiveTypeKind, "only valid for binary type");

            bool fixedLength;

            if (!type.TryGetIsFixedLength(out fixedLength)) {
                fixedLength = false;
            }

            return fixedLength ? VfpType.Blob : VfpType.Varbinary;
        }
    }
}