using gacha.Models;
using System.ComponentModel.DataAnnotations;

namespace pd.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Subgenre> Subgenres { get; set; } = new List<Subgenre>();
        public ICollection<Serie> Series { get; set; } = new List<Serie>();
    }
}
