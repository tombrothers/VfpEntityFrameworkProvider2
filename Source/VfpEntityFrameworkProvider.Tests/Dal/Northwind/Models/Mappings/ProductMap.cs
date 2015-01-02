using System.Data.Entity.ModelConfiguration;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models.Mappings {
    public class ProductMap : EntityTypeConfiguration<Product> {
        public ProductMap() {
            HasKey(x => x.ProductID);
            Map(x => {
                x.ToTable("Products");
                x.Requires("Discontinued").HasValue(false);
            })
            .Map<DiscontinuedProduct>(x => x.Requires("Discontinued").HasValue(true));
        }
    }
}