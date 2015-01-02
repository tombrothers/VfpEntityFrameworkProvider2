using System;
using System.Reflection;
using System.Text;

namespace VfpExpressionVisualizer {
    internal static class VfpExpressionTreeExtention {
        public static string ObtainOriginalName(this Type type) {
            if (!type.IsGenericType) {
                return type.Name;
            }

            return ExtractName(type.Name) + ExtractGenericArguments(type.GetGenericArguments());
        }

        public static string ObtainOriginalMethodName(this MethodInfo method) {
            if (!method.IsGenericMethod) {
                return method.Name;
            }

            return ExtractName(method.Name) + ExtractGenericArguments(method.GetGenericArguments());
        }

        private static string ExtractName(string name) {
            var i = name.LastIndexOf("`", StringComparison.Ordinal);

            if (i > 0) {
                name = name.Substring(0, i);
            }

            return name;
        }

        private static string ExtractGenericArguments(Type[] names) {
            var builder = new StringBuilder("<");
            foreach (var genericArgument in names) {
                if (builder.Length != 1) {
                    builder.Append(", ");
                }

                builder.Append(ObtainOriginalName(genericArgument));
            }

            builder.Append(">");

            return builder.ToString();
        }
    }
}