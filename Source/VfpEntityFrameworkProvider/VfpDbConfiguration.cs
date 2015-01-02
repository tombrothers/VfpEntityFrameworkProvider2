using System.Data.Entity;
using VfpEntityFrameworkProvider.Properties;

namespace VfpEntityFrameworkProvider {
    public class VfpDbConfiguration : DbConfiguration {
        public VfpDbConfiguration() {
            VfpProviderFactory.Register();

            SetProviderFactory(Resources.Provider_Invariant, VfpProviderFactory.Instance);
            SetProviderServices(Resources.Provider_Invariant, VfpProviderServices.Instance);
        }
    }
}