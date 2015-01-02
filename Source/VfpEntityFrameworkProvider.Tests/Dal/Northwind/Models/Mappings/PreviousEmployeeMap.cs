using System.Data.Entity.ModelConfiguration;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models.Mappings {
    public class PreviousEmployeeMap : EntityTypeConfiguration<PreviousEmployee> {
        public PreviousEmployeeMap() {
            ToTable("PreviousEmployees");
            HasKey(x => x.EmployeeID);
        }
    }
}