using System.Data.Entity;

namespace VfpEntityFrameworkProvider.Tests.Dal.AllTypes {
    [DbConfigurationType(typeof(VfpDbConfiguration))]
    public class AllTypesDataContext : DbContext {
        public IDbSet<AllTypesTable> AllTypes { get; set; }

        public AllTypesDataContext(VfpConnection connection)
            : base(connection, true) {
        }

        static AllTypesDataContext() {
            Database.SetInitializer<AllTypesDataContext>(null);
        }
    }
}