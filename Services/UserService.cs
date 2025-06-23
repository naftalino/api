using gacha.Database;
using gacha.Dto;
using gacha.Models;

namespace gacha.Services
{
    public class UserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public User? Get(long id) => _db.Users.Find(id);

        public List<User> GetAll() => _db.Users.ToList();

        public User Create(CreateUserDto user)
        {
            var newUser = new User
            {
                Id = user.Id,
                Linktr = user.Linktr,
                Spins = user.Spins,
                Banned = user.Banned,
                Coins = user.Coins
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();
            return newUser;
        }

        public bool Delete(long id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return false;

            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }

        public User? Update(UpdateUserDto updated, long id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return null;

            // os campos serao atualizados somente se passados no corpo de req.
            if (!string.IsNullOrWhiteSpace(updated.Linktr))
                user.Linktr = updated.Linktr;

            if (updated.Spins.HasValue)
                user.Spins += updated.Spins;

            if (updated.Banned.HasValue)
                user.Banned = updated.Banned;

            if (updated.Coins.HasValue)
                user.Coins += updated.Coins;

            _db.SaveChanges();
            return user;
        }
    }
}
