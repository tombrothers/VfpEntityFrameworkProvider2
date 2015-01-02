using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider {
    internal static class TypeUsageExtensions {
        internal static bool IsNullable(this TypeUsage type) {
            var temp = type.GetFacetValueOrDefault<bool?>(FacetInfo.NullableFacetName, null);

            return temp.HasValue && temp.Value;
        }

        internal static bool TryGetIsFixedLength(this TypeUsage type, out bool isFixedLength) {
            isFixedLength = false;

            if (!type.IsPrimitiveType(PrimitiveTypeKind.String) && !type.IsPrimitiveType(PrimitiveTypeKind.Binary)) {
                return false;
            }

            var temp = type.GetFacetValueOrDefault<bool?>(FacetInfo.FixedLengthFacetName, null);

            if (temp.HasValue) {
                isFixedLength = temp.Value;
            }

            return temp.HasValue;
        }

        internal static T GetFacetValueOrDefault<T>(this TypeUsage typeUsage, string facetName, T defaultValue) {
            Facet facet;

            if (typeUsage.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null && !facet.IsUnbounded) {
                return (T)facet.Value;
            }
            
            return defaultValue;
        }

        internal static IList<EdmProperty> GetProperties(this TypeUsage typeUsage) {
            return typeUsage.EdmType.GetProperties();
        }

        internal static TEdmType GetEdmType<TEdmType>(this TypeUsage typeUsage) 
            where TEdmType : EdmType {
            return (TEdmType)typeUsage.EdmType;
        }

        internal static TypeUsage ToElementTypeUsage(this TypeUsage typeUsage) {
            return typeUsage.IsCollectionType() ? ((CollectionType)typeUsage.EdmType).TypeUsage : null;
        }

        public static bool IsRowType(this TypeUsage typeUsage) {
            return typeUsage.EdmType.IsRowType();
        }

        public static bool IsPrimitiveType(this TypeUsage typeUsage, PrimitiveTypeKind primitiveType) {
            PrimitiveTypeKind typeKind;

            if (typeUsage.EdmType.TryGetPrimitiveTypeKind(out typeKind)) {
                return (typeKind == primitiveType);
            }

            return false;
        }

        public static bool IsPrimitiveType(this TypeUsage typeUsage) {
            return typeUsage.EdmType.IsPrimitiveType();
        }

        public static bool IsCollectionType(this TypeUsage typeUsage) {
            return typeUsage.EdmType.IsCollectionType();
        }

        public static byte GetPrecision(this TypeUsage type) {
            byte value;

            if (type.TryGetPrecision(out value)) {
                return value;
            }

            throw new ApplicationException("Cannot get the precision.");
        }

        public static bool TryGetPrecision(this TypeUsage typeUsage, out byte precision) {
            Facet f;
            precision = 0;

            if (typeUsage.Facets.TryGetValue(FacetInfo.PrecisionFacetName, false, out f)) {
                if (!f.IsUnbounded && f.Value != null) {
                    precision = (byte)f.Value;
                    return true;
                }
            }

            return false;
        }

        public static int GetMaxLength(this TypeUsage type) {
            int value;

            if (type.TryGetMaxLength(out value)) {
                return value;
                
            }

            throw new ApplicationException("Cannot get the max length.");
        }

        public static bool TryGetMaxLength(this TypeUsage typeUsage, out int maxLength) {
            Facet f;

            maxLength = 0;

            if (typeUsage.Facets.TryGetValue(FacetInfo.MaxLengthFacetName, false, out f)) {
                if (!f.IsUnbounded && f.Value != null) {
                    maxLength = (int)f.Value;
                    return true;
                }
            }

            return false;
        }

        public static byte GetScale(this TypeUsage type) {
            byte value;

            if (type.TryGetScale(out value)) {
                return value;
            }

            throw new ApplicationException("Cannot get the scale.");
        }
        
        public static bool TryGetScale(this TypeUsage typeUsage, out byte scale) {
            Facet f;

            scale = 0;
            if (typeUsage.Facets.TryGetValue(FacetInfo.ScaleFacetName, false, out f)) {
                if (!f.IsUnbounded && f.Value != null) {
                    scale = (byte)f.Value;
                    return true;
                }
            }

            return false;
        }

        internal static bool IsBoolean(this TypeUsage typeUsage) {
            return typeUsage.ToPrimitiveTypeKind() == PrimitiveTypeKind.Boolean;
        }

        internal static PrimitiveTypeKind ToPrimitiveTypeKind(this TypeUsage typeUsage) {
            PrimitiveTypeKind returnValue;

            if (!typeUsage.TryGetPrimitiveTypeKind(out returnValue)) {
                throw new NotSupportedException("Cannot create parameter of non-primitive type");
            }

            return returnValue;
        }

        internal static bool TryGetPrimitiveTypeKind(this TypeUsage typeUsage, out PrimitiveTypeKind typeKind) {
            if (typeUsage.EdmType != null && typeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType) {
                typeKind = ((PrimitiveType)typeUsage.EdmType).PrimitiveTypeKind;

                return true;
            }

            typeKind = default(PrimitiveTypeKind);

            return false;
        }
    }
}