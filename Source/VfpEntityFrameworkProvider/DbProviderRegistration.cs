using System;
using System.Configuration;
using System.Data;
using System.Linq;

namespace VfpEntityFrameworkProvider {
    internal class DbProviderRegistration {
        public static bool Register(string name, string invariantName, string description, Type factoryType) {
            ArgumentUtility.CheckNotNullOrEmpty("name", name);
            ArgumentUtility.CheckNotNullOrEmpty("invariantName", invariantName);
            ArgumentUtility.CheckNotNullOrEmpty("description", description);

            var providers = GetProviders();

            if (providers == null) {
                return false;
            }

            AddProvider(providers, name, invariantName, description, factoryType);

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
            if (providers.Rows.Count == 0) {
                return;
            }

            var provider = providers.Rows
                                    .Cast<DataRow>()
                                    .FirstOrDefault(x => Convert.ToString(x["InvariantName"]) == invariantName);

            if (provider == null) {
                return;
            }

            providers.Rows.Remove(provider);
        }

        private static DataTable GetProviders() {
            var systemData = ConfigurationManager.GetSection("system.data") as DataSet;

            if (systemData == null) {
                return null;
            }

            if (!systemData.Tables.Contains("DbProviderFactories")) {
                return null;
            }

            return systemData.Tables["DbProviderFactories"];
        }
    }
}