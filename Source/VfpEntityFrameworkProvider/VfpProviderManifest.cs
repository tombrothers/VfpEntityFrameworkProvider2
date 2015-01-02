using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Xml;
using VfpClient;

namespace VfpEntityFrameworkProvider {
    internal class VfpProviderManifest : DbXmlEnabledProviderManifest {
        private ReadOnlyCollection<PrimitiveType> primitiveTypes;
        private ReadOnlyCollection<EdmFunction> functions;

        public VfpProviderManifest()
            : base(GetProviderManifest()) {
        }

        public override ReadOnlyCollection<PrimitiveType> GetStoreTypes() {
            if (primitiveTypes == null) {
                primitiveTypes = base.GetStoreTypes();
            }

            return primitiveTypes;
        }

        public override ReadOnlyCollection<EdmFunction> GetStoreFunctions() {
            if (functions == null) {
                functions = base.GetStoreFunctions();
            }

            return functions;
        }

        protected override XmlReader GetDbInformation(string informationType) {
            if (informationType.StartsWith(StoreSchemaDefinition)) {
                return GetStoreSchemaDescription();
            }

            if (informationType.StartsWith(StoreSchemaMapping)) {
                return GetStoreSchemaMapping();
            }

            throw new ProviderIncompatibleException(string.Format("The provider returned null for the informationType '{0}'.", informationType));
        }

        private static XmlReader GetProviderManifest() {
            return XmlReader.Create(new StringReader(Properties.Resources.ProviderManifest));
        }

        private static XmlReader GetStoreSchemaMapping() {
            return XmlReader.Create(new StringReader(Properties.Resources.StoreSchemaMapping));
        }

        private static XmlReader GetStoreSchemaDescription() {
            return XmlReader.Create(new StringReader(Properties.Resources.StoreSchemaDefinition));
        }

        public override TypeUsage GetEdmType(TypeUsage storeType) {
            ArgumentUtility.CheckNotNull("storeType", storeType);

            var storeTypeName = storeType.EdmType.Name.ToLowerInvariant();

            if (!StoreTypeNameToEdmPrimitiveType.ContainsKey(storeTypeName)) {
                throw new ArgumentException(string.Format("The underlying provider does not support the type '{0}'.", storeTypeName));
            }

            var primitiveType = StoreTypeNameToEdmPrimitiveType[storeTypeName];
            var maxLength = 0;
            var isFixedLen = false;
            var isUnbounded = true;

            PrimitiveTypeKind newPrimitiveTypeKind;

            switch (storeTypeName) {
                case "logical":
                case "int":
                case "integer":
                case "float":
                case "double":
                    return TypeUsage.CreateDefaultTypeUsage(primitiveType);
                case "date":
                case "datetime":
                    return TypeUsage.CreateDateTimeTypeUsage(primitiveType, null);
                case "numeric":
                    byte precision;
                    byte scale;

                    if (storeType.TryGetPrecision(out precision) && storeType.TryGetScale(out scale)) {
                        return TypeUsage.CreateDecimalTypeUsage(primitiveType, precision, scale);
                    }

                    return TypeUsage.CreateDecimalTypeUsage(primitiveType);
                case "currency":
                    return TypeUsage.CreateDecimalTypeUsage(primitiveType, 19, 4);
                case "varchar":
                case "binaryvarchar":
                    newPrimitiveTypeKind = PrimitiveTypeKind.String;
                    isUnbounded = !storeType.TryGetMaxLength(out maxLength);
                    isFixedLen = false;
                    break;
                case "character":
                case "char":
                case "binarychar":
                    newPrimitiveTypeKind = PrimitiveTypeKind.String;
                    isUnbounded = !storeType.TryGetMaxLength(out maxLength);
                    isFixedLen = true;
                    break;
                case "guid":
                    newPrimitiveTypeKind = PrimitiveTypeKind.Guid;
                    isUnbounded = false;
                    isFixedLen = true;
                    break;
                case "memo":
                case "binarymemo":
                    newPrimitiveTypeKind = PrimitiveTypeKind.String;
                    isUnbounded = true;
                    isFixedLen = false;
                    break;
                case "blob":
                case "general":
                    newPrimitiveTypeKind = PrimitiveTypeKind.Binary;
                    isUnbounded = true;
                    isFixedLen = false;
                    break;
                default:
                    throw new NotSupportedException(string.Format("The underlying provider does not support the type '{0}'.", storeTypeName));
            }

            switch (newPrimitiveTypeKind) {
                case PrimitiveTypeKind.String:
                    if (!isUnbounded) {
                        return TypeUsage.CreateStringTypeUsage(primitiveType, false, isFixedLen, maxLength);
                    }

                    return TypeUsage.CreateStringTypeUsage(primitiveType, false, isFixedLen);
                case PrimitiveTypeKind.Binary:
                    if (!isUnbounded) {
                        return TypeUsage.CreateBinaryTypeUsage(primitiveType, false, maxLength);
                    }

                    return TypeUsage.CreateBinaryTypeUsage(primitiveType, isFixedLen);
                case PrimitiveTypeKind.Guid:
                    return TypeUsage.CreateDefaultTypeUsage(primitiveType);
                default:
                    throw new NotSupportedException(string.Format("The underlying provider does not support the type '{0}'.", storeTypeName));
            }
        }

        internal TypeUsage GetDecimalTypeUsage(byte precision, byte scale) {
            return TypeUsage.CreateDecimalTypeUsage(StoreTypeNameToStorePrimitiveType["numeric"], precision, scale);
        }

        public override TypeUsage GetStoreType(TypeUsage edmType) {
            ArgumentUtility.CheckNotNull("edmType", edmType);

            var primitiveType = edmType.EdmType as PrimitiveType;

            if (primitiveType == null) {
                throw new ArgumentException(string.Format("The underlying provider does not support the type '{0}'.", edmType));
            }

            var facets = edmType.Facets;

            switch (primitiveType.PrimitiveTypeKind) {
                case PrimitiveTypeKind.Binary:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["blob"]);
                case PrimitiveTypeKind.Boolean:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["logical"]);
                case PrimitiveTypeKind.Byte:
                case PrimitiveTypeKind.SByte:
                case PrimitiveTypeKind.Int16:
                case PrimitiveTypeKind.Int32:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["integer"]);
                case PrimitiveTypeKind.Double:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["double"]);
                case PrimitiveTypeKind.Int64:
                    return TypeUsage.CreateDecimalTypeUsage(StoreTypeNameToStorePrimitiveType["numeric"], 38, 0);
                case PrimitiveTypeKind.Decimal:
                    byte precision;
                    if (!edmType.TryGetPrecision(out precision)) {
                        precision = 18;
                    }

                    byte scale;
                    if (!edmType.TryGetScale(out scale)) {
                        scale = 0;
                    }

                    return TypeUsage.CreateDecimalTypeUsage(StoreTypeNameToStorePrimitiveType["numeric"], precision, scale);
                case PrimitiveTypeKind.Single:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["float"]);
                case PrimitiveTypeKind.Guid:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["guid"]);
                case PrimitiveTypeKind.String:
                    var isFixedLength = null != facets["FixedLength"].Value && (bool)facets["FixedLength"].Value;
                    var f = facets["MaxLength"];
                    var isMaxLength = f.IsUnbounded || null == f.Value || (int)f.Value > VfpMapping.MaximumCharacterFieldSize;

                    if (isFixedLength) {
                        return TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["char"], false, true, VfpMapping.MaximumCharacterFieldSize);
                    }

                    if (isMaxLength) {
                        return TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["memo"], false, false);
                    }

                    return TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["varchar"], false, false, VfpMapping.MaximumCharacterFieldSize);

                case PrimitiveTypeKind.Time:
                case PrimitiveTypeKind.DateTime:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["datetime"]);

                default:
                    throw new NotSupportedException(string.Format("There is no store type corresponding to the EDM type '{0}' of primitive type '{1}'.", edmType, primitiveType.PrimitiveTypeKind));
            }
        }
    }
}