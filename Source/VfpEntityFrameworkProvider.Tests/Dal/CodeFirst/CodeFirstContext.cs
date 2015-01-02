using System.Data.Entity;
using VfpEntityFrameworkProvider.Tests.Dal.CodeFirst.Models;

namespace VfpEntityFrameworkProvider.Tests.Dal.CodeFirst {
    [DbConfigurationType(typeof(VfpDbConfiguration))]
    public class CodeFirstContext : DbContext {
        public IDbSet<Artist> Artists { get; set; }
        public IDbSet<Album> Album { get; set; }
        public IDbSet<User> Users { get; set; }

        public CodeFirstContext(VfpConnection connection)
            : base(connection, true) {
        }

        static CodeFirstContext() {
            Database.SetInitializer(new DataInitializer());
        }
    }
}