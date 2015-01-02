using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider {
    public static class EdmTypeExtensions {
        internal static bool TryGetTypeFacetDescriptionByName(this EdmType edmType, string facetName, out FacetDescription facetDescription) {
            facetDescription = null;

            if (edmType.IsPrimitiveType()) {
                var primitiveType = (PrimitiveType)edmType;

                foreach (var fd in primitiveType.FacetDescriptions) {
                    if (facetName.Equals(fd.FacetName, StringComparison.OrdinalIgnoreCase)) {
                        facetDescription = fd;

                        return true;
                    }
                }
            }

            return false;
        }

        internal static IList<EdmProperty> GetProperties(this EdmType edmType) {
            switch (edmType.BuiltInTypeKind) {
                case BuiltInTypeKind.ComplexType:
                    return ((ComplexType)edmType).Properties;
                case BuiltInTypeKind.EntityType:
                    return ((EntityType)edmType).Properties;
                case BuiltInTypeKind.RowType:
                    return ((RowType)edmType).Properties;
                default:
                    return new List<EdmProperty>();
            }
        }

        public static bool IsRowType(this EdmType edmType) {
            return BuiltInTypeKind.RowType == edmType.BuiltInTypeKind;
        }

        public static bool IsPrimitiveType(this EdmType edmType) {
            return BuiltInTypeKind.PrimitiveType == edmType.BuiltInTypeKind;
        }

        public static bool TryGetPrimitiveTypeKind(this EdmType edmType, out PrimitiveTypeKind typeKind) {
            if (edmType.IsPrimitiveType()) {
                typeKind = ((PrimitiveType)edmType).PrimitiveTypeKind;

                return true;
            }

            typeKind = default(PrimitiveTypeKind);

            return false;
        }

        public static bool IsCollectionType(this EdmType edmType) {
            return BuiltInTypeKind.CollectionType == edmType.BuiltInTypeKind;
        }
    }
}