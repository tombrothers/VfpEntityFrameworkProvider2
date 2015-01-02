namespace VfpEntityFrameworkProvider {
    public static class FacetInfo {
        internal static readonly int UnicodeStringMaxMaxLength = int.MaxValue / 2;
        internal static readonly int AsciiStringMaxMaxLength = int.MaxValue;
        internal static readonly int BinaryMaxMaxLength = int.MaxValue;

        public static readonly string MaxLengthFacetName = "MaxLength";
        public static readonly string UnicodeFacetName = "Unicode";
        public static readonly string FixedLengthFacetName = "FixedLength";
        public static readonly string PreserveSecondsFacetName = "PreserveSeconds";
        public static readonly string PrecisionFacetName = "Precision";
        public static readonly string ScaleFacetName = "Scale";
        public static readonly string DefaultValueFacetName = "DefaultValue";
        internal const string NullableFacetName = "Nullable";
    }
}