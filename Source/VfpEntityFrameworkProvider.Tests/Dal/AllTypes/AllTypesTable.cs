using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VfpEntityFrameworkProvider.Tests.Dal.AllTypes {
    public class AllTypesTable {
        public int Id { get; set; }

        [Column(TypeName = "char")]
        public string Char { get; set; }

        [Column(TypeName = "binarychar")]
        public string BinaryChar { get; set; }

        [Column(TypeName = "varchar")]
        public string VarChar { get; set; }

        [Column(TypeName = "binaryvarchar")]
        public string BinaryVarChar { get; set; }

        public string Memo { get; set; }

        [Column(TypeName = "binarymemo")]
        public string BinaryMemo { get; set; }

        [Column(TypeName = "currency")]
        public decimal? Currency { get; set; }

        public decimal? Decimal { get; set; }

        public int? Integer { get; set; }

        public bool? Logical { get; set; }

        public long? Long { get; set; }

        public float? Float { get; set; }

        public double? Double { get; set; }
        ////public byte[] Blob { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public DateTime? DateTime { get; set; }
        public Guid? Guid { get; set; }
    }
}