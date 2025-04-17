using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace gacha.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Linktr { get; set; } = string.Empty;

        public int Spins { get; set; } = 0;

        public bool Banned { get; set; } = false;

        public int Coins { get; set; } = 0;

        public ICollection<Collection> Collection { get; set; } = new List<Collection>();
    }

}