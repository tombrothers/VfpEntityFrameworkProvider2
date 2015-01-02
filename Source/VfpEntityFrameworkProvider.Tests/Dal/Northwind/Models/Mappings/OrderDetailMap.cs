using System.Data.Entity.ModelConfiguration;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models.Mappings {
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetail> {
        public OrderDetailMap() {
            //ToTable("OrderDetails");
            HasKey(x => new { x.OrderID, x.ProductID });
        }
    }
}