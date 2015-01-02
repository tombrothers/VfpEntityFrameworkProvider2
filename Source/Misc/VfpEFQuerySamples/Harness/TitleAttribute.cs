using System;

namespace SampleQueries.Harness {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TitleAttribute : Attribute {
        public TitleAttribute(string title) {
            Title = title;
        }

        public string Title { get; set; }
    }
}