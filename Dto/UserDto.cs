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
        public int Id { get; set; }
        public string Linktr { get; set; }
        public int Spins { get; set; }
        public bool Banned { get; set; }
        public int Coins { get; set; }
    }
}