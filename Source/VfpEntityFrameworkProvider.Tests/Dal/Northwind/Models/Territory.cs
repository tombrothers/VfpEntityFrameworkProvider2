using System.Collections.Generic;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class Territory {
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}