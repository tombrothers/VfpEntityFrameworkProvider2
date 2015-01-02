using System;

namespace VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models {
    public class DiscontinuedProduct : Product {
        public DateTime? DiscontinuedDate { get; set; }
    }
}