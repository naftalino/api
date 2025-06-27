using pd.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gacha.Models
{
    public class Serie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? GenreId { get; set; }
        public int? SubGenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre? Genre { get; set; }

        [ForeignKey("SubGenreId")]
        public Subgenre? SubGenre { get; set; }
            
        public string ThumbUrl { get; set; } = "https://placehold.co/600x400/png";

        public ICollection<Card> Card { get; set; } = new List<Card>();
    }
}
