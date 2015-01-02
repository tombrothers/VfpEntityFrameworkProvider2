using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class Customer {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Phone { get; set; }
        public CommonAddress Address { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Customer() {
            Address = new CommonAddress();
            Orders = new Collection<Order>();
        }
    }
}