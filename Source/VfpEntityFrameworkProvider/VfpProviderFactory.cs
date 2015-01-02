using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Properties;
using VfpEntityFrameworkProvider.VfpOleDb;

namespace VfpEntityFrameworkProvider {
    public class VfpProviderFactory : VfpClient.VfpClientFactory, IServiceProvider {
        public new static readonly VfpProviderFactory Instance = new VfpProviderFactory();

        private static readonly Lazy<bool> _registered = new Lazy<bool>(() =>
            DbProviderRegistration.Register(
                Resources.Provider_DisplayName,
                Resources.Provider_Invariant,
                Resources.Provider_Description,
                typeof(VfpProviderFactory))
        );

#if DEBUG
        static VfpProviderFactory() {
            VfpClient.VfpClientTracing.Tracer = new TraceSource("VfpClient", SourceLevels.All);
        }
#endif

        object IServiceProvider.GetService(Type serviceType) {
            return serviceType == typeof(DbProviderServices) ? VfpProviderServices.Instance : null;
        }

        public override DbConnection CreateConnection() {
            return new VfpConnection();
        }

        public override DbCommand CreateCommand() {
            return new VfpCommand();
        }

        public static bool Register() {
            return _registered.Value;
        }
    }
}