using gacha.Models;
using System.ComponentModel.DataAnnotations;

namespace gacha.Dto
{
    public class UpdateUserDto
    {
        public string? Linktr { get; set; }
        public int? Spins { get; set; }
        public bool? Banned { get; set; }
        public int? Coins { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsDonator { get; set; }
        public string? Username { get; set; }
        public int? FavoriteCardId { get; set; }
        public string? CollectionName { get; set; }
    }

    public class CreateUserDto
    {
        [Required]
        public long Id { get; set; }
        public string? Linktr { get; set; } = "Sem linktr.";
        public int Spins { get; set; } = 0;
        public bool IsAdmin { get; set; } = false;
        public bool IsDonator { get; set; } = false;
        public bool Banned { get; set; } = false;
        public int Coins { get; set; } = 100;
        public string Username { get; set; } = string.Empty;
        public int TradesMade { get; set; } = 0;
        public int? FavoriteCardId { get; set; }
        public string CollectionName { get; set; } = "Minha incrível coleção!";
    }

    public class GetUserDto
    {
        public long Id { get; set; }
        public string? Linktr { get; set; } = "Sem linktr.";
        public int Spins { get; set; } = 0;
        public bool IsAdmin { get; set; } = false;
        public bool IsDonator { get; set; } = false;
        public bool Banned { get; set; } = false;
        public int Coins { get; set; } = 100;
        public string Username { get; set; } = string.Empty;
        public int TradesMade { get; set; } = 0;
        public Card? FavoriteCard { get; set; }
        public string CollectionName { get; set; } = "Minha incrível coleção!";
    }
}
