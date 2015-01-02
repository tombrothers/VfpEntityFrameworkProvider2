using System.ComponentModel.DataAnnotations.Schema;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class OrderDetail {
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}