using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VfpEntityFrameworkProvider.Tests.Dal.CodeFirst.Models {
    public class Album {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
