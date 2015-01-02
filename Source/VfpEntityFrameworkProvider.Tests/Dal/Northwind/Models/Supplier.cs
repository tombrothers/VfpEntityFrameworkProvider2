using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class Supplier {
        public int SupplierID { get; set; }
        public CommonAddress Address { get; set; }
        public ICollection<Product> Products { get; set; }
        
        public Supplier() {
            Address = new CommonAddress();
            Products = new Collection<Product>();
        }
    }
}