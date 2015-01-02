using System;

namespace SampleQueries.Harness {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class PrefixAttribute : Attribute {
        public PrefixAttribute(string prefix) {
            Prefix = prefix;
        }

        public string Prefix { get; set; }
    }
}