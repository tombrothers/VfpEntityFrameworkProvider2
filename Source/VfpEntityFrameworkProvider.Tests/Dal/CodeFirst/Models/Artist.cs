using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VfpEntityFrameworkProvider.Tests.Dal.CodeFirst.Models {
    public class Artist {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtistId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        // FavoriteAlbum1 and FavoriteAlbum2 are used to test that the DDL builder will create unique index names
        [ForeignKey("FavoriteAlbum1Id")]
        public Album FavoriteAlbum1 { get; set; }
        public int FavoriteAlbum1Id { get; set; }

        [ForeignKey("FavoriteAlbum2Id")]
        public Album FavoriteAlbum2 { get; set; }
        public int FavoriteAlbum2Id { get; set; }
    }
}