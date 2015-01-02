using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VfpEntityFrameworkProvider.Tests.Dal.CodeFirst.Models {
    [Table("User")]
    public class User {
        [Key, Column("FirstName", Order = 0)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Key, Column("LastName", Order = 1)]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}