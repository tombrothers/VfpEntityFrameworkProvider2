using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace VfpEntityFrameworkProvider {
    internal class DbProviderRegistration {
        public static bool Register(string name, string invariantName, string description, Type factoryType) {
            ArgumentUtility.CheckNotNullOrEmpty("name", name);
            ArgumentUtility.CheckNotNullOrEmpty("invariantName", invariantName);
            ArgumentUtility.CheckNotNullOrEmpty("description", description);
#if NETSTANDARD2_1
            DbProviderFactories.RegisterFactory(invariantName, factoryType.AssemblyQualifiedName);
#else
            var providers = GetProviders();

            if(providers == null) {
                return false;
            }

            AddProvider(providers, name, invariantName, description, factoryType);

#endif
            return true;
        }

        private static void AddProvider(DataTable providers, string name, string invariantName, string description, Type factoryType) {
            RemoveExistingProvider(providers, invariantName);

            var row = providers.NewRow();

            row["Name"] = name;
            row["InvariantName"] = invariantName;
            row["Description"] = description;
            row["AssemblyQualifiedName"] = factoryType.AssemblyQualifiedName;

            providers.Rows.Add(row);
        }

        private static void RemoveExistingProvider(DataTable providers, string invariantName) {
            if(providers.Rows.Count == 0) {
                return;
            }

            var provider = providers.Rows
                                    .Cast<DataRow>()
                                    .FirstOrDefault(x => Convert.ToString(x["InvariantName"]) == invariantName);

            if(provider == null) {
                return;
            }

            providers.Rows.Remove(provider);
        }

        private static DataTable GetProviders() {
            // Calling GetFactoryClasses creates the _providerTable DataTable but returns a copy of the DataTable.
            DbProviderFactories.GetFactoryClasses();

            // Reflection is used to get a reference to the _providerTable DataTable.
            var fieldInfos = typeof(DbProviderFactories).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            var fieldInfo = typeof(DbProviderFactories).GetField("_providerTable", BindingFlags.Static | BindingFlags.NonPublic);
            return (DataTable)fieldInfo.GetValue(null);
        }
    }
}