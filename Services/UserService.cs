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

        public User? Get(int id) => _db.Users.Find(id);

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

        public bool Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return false;

            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }

        public User? Update(User updated, int id)
        {
            var user = _db.Users.Find(id);
            Console.WriteLine(user);
            if (user == null) return null;

            user.Linktr = updated.Linktr;
            user.Spins = updated.Spins;
            user.Banned = updated.Banned;
            user.Coins = updated.Coins;

            _db.SaveChanges();
            return user;
        }
    }
}
