using System;

namespace SampleQueries.Harness {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CategoryAttribute : Attribute {
        public CategoryAttribute(string category) {
            this.Category = category;
        }

        public string Category { get; set; }
    }
}