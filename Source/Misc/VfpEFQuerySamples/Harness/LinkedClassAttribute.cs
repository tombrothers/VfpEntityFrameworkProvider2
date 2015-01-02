using System;

namespace SampleQueries.Harness {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class LinkedClassAttribute : Attribute {
        public LinkedClassAttribute(string className) {
            ClassName = className;
        }

        public string ClassName { get; set; }
    }
}