using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider {
    internal static class EdmFunctionExtensions {
        public static bool IsNiladic(this EdmFunction edmFunction) {
            return edmFunction.TryGetValueForMetadataProperty<bool>("NiladicFunctionAttribute");
        }

        public static bool IsStoreFunction(this EdmFunction edmFunction) {
            return !edmFunction.IsCanonicalFunction();
        }
        
        public static bool IsCanonicalFunction(this EdmFunction edmFunction) {
            return edmFunction.NamespaceName == "Edm";
        }

        public static bool IsBuiltinFunction(this EdmFunction edmFunction) {
            return edmFunction.TryGetValueForMetadataProperty<bool>("BuiltInAttribute");
        }
    }
}