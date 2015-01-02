using System;

namespace SampleQueries.Harness {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class DescriptionAttribute : Attribute {
        public DescriptionAttribute(string description) {
            Description = description;
        }

        public string Description { get; set; }
    }
}