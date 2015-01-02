using System.Data.Entity;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models.Mappings;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind {
    [DbConfigurationType(typeof(VfpDbConfiguration))]
    public class NorthwindContext : DbContext {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PreviousEmployee> PreviousEmployees { get; set; }

        public NorthwindContext(VfpConnection connection)
            : base(connection, true) {
        }
        
        static NorthwindContext() {
            Database.SetInitializer<NorthwindContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new PreviousEmployeeMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new SupplierMap());

            modelBuilder.ComplexType<CommonAddress>();
        }
    }
}