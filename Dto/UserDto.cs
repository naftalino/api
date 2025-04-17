using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace gacha.Dto
{
    public class UpdateUserDto
    {
        public string? Linktr { get; set; }
        public int? Spins { get; set; }
        public bool? Banned { get; set; }
        public int? Coins { get; set; }
    }

    public class CreateUserDto
    {
        [Required]
        public long Id { get; set; }
        public string? Linktr { get; set; } = "Sem linktr.";
        public int? Spins { get; set; } = 0;
        public bool? Banned { get; set; } = false;
        public int? Coins { get; set; } = 100;
    }
}