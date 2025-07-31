using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gacha.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Linktr { get; set; } = "Sem link definido.";
        public int Spins { get; set; } = 0;
        public bool Banned { get; set; } = false;
        public int Coins { get; set; } = 0;
        public bool IsAdmin { get; set; } = false;
        public string Username { get; set; } = string.Empty;
        public int TradesMade { get; set; } = 0;
        public bool IsDonator { get; set; } = false;
        public string CollectionName { get; set; } = "Minha incrível coleção!";
        public int? FavoriteCardId { get; set; }
        [ForeignKey("FavoriteCardId")]
        public Card? FavoriteCard { get; set; }
        public ICollection<Collection> Collection { get; set; } = new List<Collection>();
    }
}
