using System;

namespace VfpEntityFrameworkProvider {
    internal static class ArgumentUtility {
        public static T CheckIsDefined<T>(string argumentName, T value) where T : struct {
            if (!Enum.IsDefined(typeof(T), value)) {
                throw new ArgumentOutOfRangeException(argumentName);
            }

            return value;
        }

        public static T CheckNotNull<T>(string argumentName, T value) where T : class {
            if (value == null) {
                throw new ArgumentNullException(argumentName);
            }

            return value;
        }

        public static T CheckNotNullAndType<T>(string argumentName, object actualValue) {
            if (actualValue == null) {
                throw new ArgumentNullException(argumentName);
            }

            if (!(actualValue is T)) {
                throw new ArgumentException(string.Format("Type mismatch for '{0}.'  Expected Type:  {1} - Actual Type:  {2}", argumentName, typeof(T), actualValue.GetType()));
            }

            return (T)actualValue;
        }

        public static string CheckNotNullOrEmpty(string argumentName, string value) {
            if (string.IsNullOrWhiteSpace(value)) {
                throw new ArgumentNullException(argumentName);
            }

            return value;
        }
    }
}