using gacha.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pd.Models
{
    public class Subgenre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; } = null!;
        public ICollection<Serie> Series { get; set; } = new List<Serie>();
    }
}
