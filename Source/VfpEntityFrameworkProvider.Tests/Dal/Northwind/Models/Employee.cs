using System;
using System.Collections.Generic;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class Employee {
        public int EmployeeID { get; set; }
        public DateTime? HireDate { get; set; }
        public ICollection<Territory> Territories { get; set; }
        public CommonAddress Address { get; set; }

        public Employee() {
            Address = new CommonAddress();
        }
    }
}