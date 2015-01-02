using System;
using System.Collections.Generic;
using System.Linq;

namespace VfpEntityFrameworkProvider {
    internal static class IEnumerableExtensions {
        public static string Uniquify(this IEnumerable<string> inputStrings, string targetString) {
            ArgumentUtility.CheckNotNull("inputStrings", inputStrings);
            ArgumentUtility.CheckNotNull("targetString", targetString);

            var uniqueString = targetString;
            var i = 0;

            while (inputStrings.Any(n => string.Equals(n, uniqueString, StringComparison.Ordinal))) {
                uniqueString = targetString + ++i;
            }

            return uniqueString;
        }

        public static void Each<T>(this IEnumerable<T> ts, Action<T, int> action) {
            ArgumentUtility.CheckNotNull("ts", ts);
            ArgumentUtility.CheckNotNull("action", action);

            var i = 0;
            foreach (var t in ts) {
                action(t, i++);
            }
        }

        public static void Each<T>(this IEnumerable<T> ts, Action<T> action) {
            ArgumentUtility.CheckNotNull("ts", ts);
            ArgumentUtility.CheckNotNull("action", action);

            foreach (var t in ts) {
                action(t);
            }
        }

        public static void Each<T, S>(this IEnumerable<T> ts, Func<T, S> action) {
            ArgumentUtility.CheckNotNull("ts", ts);
            ArgumentUtility.CheckNotNull("action", action);

            foreach (var t in ts) {
                action(t);
            }
        }

        public static string Join<T>(this IEnumerable<T> ts, Func<T, string> selector = null, string separator = ", ") {
            ArgumentUtility.CheckNotNull("ts", ts);

            selector = selector ?? (t => t.ToString());

            return string.Join(separator, ts.Where(t => !ReferenceEquals(t, null)).Select(selector));
        }

        public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource value) {
            ArgumentUtility.CheckNotNull("source", source);

            yield return value;

            foreach (var element in source) {
                yield return element;
            }
        }

        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource value) {
            ArgumentUtility.CheckNotNull("source", source);

            foreach (var element in source) {
                yield return element;
            }

            yield return value;
        }
    }
}