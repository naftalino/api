using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gacha.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = "https://placehold.co/450x700/png";
        public int Value { get; set; } = 10;
        public int SerieId { get; set; }
        [ForeignKey("SerieId")]
        public Serie Serie { get; set; } = null!;
        public string Credits { get; set; } = "t.me/padocard";
        public int TimesPulled { get; set; } = 0;
        public int PeopleOwned { get; set; } = 0;
        public ICollection<Collection> Collection { get; set; } = new List<Collection>();
    }
}
