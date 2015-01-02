using System.Collections.Generic;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class Product {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? ReorderLevel { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}