using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gacha.Models
{
    public class Collection
    {
        [Key]
        public long UserId { get; set; }

        public int CardId { get; set; }

        public int Quantity { get; set; } = 1;

        [ForeignKey("CardId")]
        public Card Card { get; set; } = null!;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }

}