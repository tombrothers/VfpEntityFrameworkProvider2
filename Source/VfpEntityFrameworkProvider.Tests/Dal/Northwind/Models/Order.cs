using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class Order {
        public int OrderID { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public string ShipName { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Freight { get; set; }
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public int EmployeeID { get; set; }

        public Order() {
            OrderDetails = new Collection<OrderDetail>();
        }
    }
}