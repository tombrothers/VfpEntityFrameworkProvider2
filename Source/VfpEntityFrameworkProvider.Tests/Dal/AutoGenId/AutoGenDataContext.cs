using System.Data.Entity;

namespace VfpEntityFrameworkProvider.Tests.Dal.AutoGenId {
    [DbConfigurationType(typeof(VfpDbConfiguration))]
    public class AutoGenDataContext : DbContext {
        public IDbSet<AutoGen> AutoGens { get; set; }

        static AutoGenDataContext() {
            Database.SetInitializer<AutoGenDataContext>(null);
        }

        public AutoGenDataContext(VfpConnection connection)
            : base(connection, true) {
        }
    }
}