using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace VfpEntityFrameworkProvider {
    public class VfpConnectionFactory : IDbConnectionFactory {
        public DbConnection CreateConnection(string nameOrConnectionString) {
            return new VfpClient.VfpConnection(nameOrConnectionString);
        }
    }
}