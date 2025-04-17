using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace gacha.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        public string? Linktr { get; set; }

        public int? Spins { get; set; } = 0;

        public bool? Banned { get; set; } = false;

        public int? Coins { get; set; } = 0;

        public ICollection<Collection> Collection { get; set; } = new List<Collection>();
    }

}