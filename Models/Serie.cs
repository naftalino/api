using System.ComponentModel.DataAnnotations;

namespace gacha.Models
{
    public class Serie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string ThumbUrl { get; set; } = "https://placehold.co/600x400/png";

        public ICollection<Card> Card { get; set; } = new List<Card>();
    }
}
