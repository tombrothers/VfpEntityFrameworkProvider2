using System.Data.Entity;

namespace VfpEntityFrameworkProvider.Tests.Dal.Decimal {
    [DbConfigurationType(typeof(VfpDbConfiguration))]
    public class DecimalContext : DbContext {
        public DbSet<DecimalEntity> Decimals { get; set; }

        public DecimalContext(VfpConnection connection)
            : base(connection, true) {
        }

        static DecimalContext() {
            Database.SetInitializer<DecimalContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<DecimalEntity>().ToTable("DecimalTable");
            modelBuilder.Entity<DecimalEntity>().Property(x => x.ID).HasColumnName("iPK");
            modelBuilder.Entity<DecimalEntity>().Property(x => x.Value).HasColumnName("iValue").HasPrecision(20, 7);
        }
    }
}