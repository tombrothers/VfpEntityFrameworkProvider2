using System.Data.Entity.ModelConfiguration;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models.Mappings {
    public class EmployeeMap : EntityTypeConfiguration<Employee> {
        public EmployeeMap() {
            Property(x => x.Address.Address).HasColumnName("Address");
            Property(x => x.Address.City).HasColumnName("City");
            Property(x => x.Address.Country).HasColumnName("Country");
            Property(x => x.Address.Region).HasColumnName("Region");

            HasMany(x => x.Territories)
                .WithMany(x => x.Employees)
                .Map(x => {
                         x.MapLeftKey("EmployeeId");
                         x.MapRightKey("TerritoryId");
                         x.ToTable("EmployeeTerritories");
                     });
        }
    }
}