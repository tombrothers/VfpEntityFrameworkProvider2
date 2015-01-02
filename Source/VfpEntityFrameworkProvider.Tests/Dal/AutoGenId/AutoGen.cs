using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VfpEntityFrameworkProvider.Tests.Dal.AutoGenId {
    [Table("AutoGenIdTest")]
    public class AutoGen {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pk")]
        public string Id { get; set; }

        [Column("cValue")]
        public string Value { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("Created")]
        public DateTime Created { get; set; }
    }
}