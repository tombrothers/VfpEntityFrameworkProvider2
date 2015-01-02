using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Design;
using SampleQueries.Harness;

namespace SampleQueries.Samples {
    class SchemaInformationBasedSample : SampleSuite {
        protected Store.SchemaInformation context;
        private EntityConnection schemaConnection;

        public override void InitSample(string connectionString) {
            // parse connection string to get provider name and provider connection string

            var csb = new EntityConnectionStringBuilder();
            csb.ConnectionString = connectionString;

            // create schema connection
            //schemaConnection = EntityStoreSchemaGenerator.CreateStoreSchemaConnection(csb.Provider, csb.ProviderConnectionString);
            //schemaConnection.Open();

            context = new Store.SchemaInformation(schemaConnection);
        }

        public override void TearDownSample() {
            if (context != null) {
                context.Dispose();
                context = null;
            }

            if (schemaConnection != null) {
                schemaConnection.Dispose();
                schemaConnection = null;
            }

            base.TearDownSample();
        }
    }
}