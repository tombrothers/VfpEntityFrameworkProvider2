using System.Data.Entity.ModelConfiguration;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models.Mappings {
    public class CustomerMap : EntityTypeConfiguration<Customer> {
        public CustomerMap() {
            Property(x => x.Address.Address).HasColumnName("Address");
            Property(x => x.Address.City).HasColumnName("City");
            Property(x => x.Address.Country).HasColumnName("Country");
            Property(x => x.Address.Region).HasColumnName("Region");
        }
    }
}