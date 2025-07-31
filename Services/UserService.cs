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
                Coins = user.Coins,
                IsAdmin = user.IsAdmin,
                CollectionName = user.CollectionName,
                IsDonator = user.IsDonator,
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
                user.Spins = (int)updated.Spins;

            if (updated.Banned.HasValue)
                user.Banned = (bool)updated.Banned;

            if (updated.Coins.HasValue)
                user.Coins = (int)updated.Coins;

            if (updated.IsDonator.HasValue)
                user.IsDonator = updated.IsDonator.Value;

            if (!string.IsNullOrEmpty(updated.CollectionName))
                user.CollectionName = updated.CollectionName;

            if (updated.IsAdmin.HasValue)
                user.IsAdmin = updated.IsAdmin.Value;

            if (updated.IsAdmin.HasValue)
                user.IsAdmin = updated.IsAdmin.Value;

            if (!string.IsNullOrEmpty(updated.Username))
                user.Username = updated.Username;

            if (updated.FavoriteCardId.HasValue)
            {
                var result = SetFavoriteCard(user.Id, (int)updated.FavoriteCardId);

                if (result.Item1)
                {
                    user.FavoriteCardId = updated.FavoriteCardId;
                }
                else
                {
                    throw new Exception($"Usuário não pode definir a carta de ID {updated.FavoriteCardId} como favorita. Motivo: {result.Item2}");
                }
            }
            _db.SaveChanges();
            return user;
        }

        public (bool, string) SetFavoriteCard(long UserId, int CardId)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == UserId);
            var card = _db.Cards.FirstOrDefault(x => x.Id == CardId);
            var cardInCollection = _db.Collections.FirstOrDefault(x => x.UserId == UserId && x.CardId == CardId);

            if (user == null || card == null)
            {
                return (false, "O usuário/carta não foram encontrados.");
            }

            if (cardInCollection == null)
            {
                return (false, "A carta não está na coleção do usuário.");
            }

            user.FavoriteCardId = CardId;

            _db.SaveChanges();

            return (true, "Carta adicionada com sucesso.");
        }
    }
}
