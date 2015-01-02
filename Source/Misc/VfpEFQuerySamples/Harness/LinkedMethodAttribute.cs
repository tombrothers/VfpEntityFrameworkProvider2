using System;

namespace SampleQueries.Harness {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class LinkedMethodAttribute : Attribute {
        public LinkedMethodAttribute(string methodName) {
            MethodName = methodName;
        }

        public string MethodName { get; set; }
    }
}