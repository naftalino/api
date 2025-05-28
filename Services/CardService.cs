using gacha.Database;
using gacha.Dto;
using gacha.Models;
using Microsoft.EntityFrameworkCore;

namespace gacha.Services
{
    public class CardService
    {
        private readonly AppDbContext _db;

        public CardService(AppDbContext db)
        {
            _db = db;
        }

        public List<GetAllDto> GetAll()
        {
            return _db.Cards
                .Include(c => c.Serie)
                .Select(c => new GetAllDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ThumbUrl = c.ThumbUrl,
                    Rarity = c.Rarity,
                    Value = c.Value,
                    SerieId = c.SerieId,
                    SerieName = c.Serie.Name
                })
                .ToList();
        }
        public CardDto? GetById(int id)
        {
            var card = _db.Cards.Include(c => c.Serie).FirstOrDefault(c => c.Id == id);
            if (card == null) return null;

            return new CardDto
            {
                Id = card.Id,
                Name = card.Name,
                ThumbUrl = card.ThumbUrl,
                Rarity = card.Rarity,
                Value = card.Value,
                Serie = new SerieBasicDto
                {
                    Id = card.Serie.Id,
                    Name = card.Serie.Name,
                    Genre = card.Serie.Genre,
                    ThumbUrl = card.Serie.ThumbUrl
                }
            };
        }

        public CardDto? Create(CreateCardDto dto)
        {
            var serie = _db.Series.Find(dto.SerieId);
            if (serie == null) return null;

            var card = new Card
            {
                Name = dto.Name,
                Rarity = dto.Rarity,
                ThumbUrl = dto.ThumbUrl,
                Serie = serie
            };

            _db.Cards.Add(card);
            _db.SaveChanges();

            return new CardDto
            {
                Id = card.Id,
                Name = card.Name,
                Rarity = card.Rarity,
                Value = card.Value,
                ThumbUrl = card.ThumbUrl,
                Serie = new SerieBasicDto
                {
                    Id = serie.Id,
                    Name = serie.Name,
                    Genre = serie.Genre,
                    ThumbUrl = serie.ThumbUrl
                }
            };
        }

        public CardDto? Update(int id, UpdateCardDto dto)
        {
            var card = _db.Cards.Include(c => c.Serie).FirstOrDefault(c => c.Id == id);
            if (card == null) return null;

            if (dto.Name != null) card.Name = dto.Name;
            if (dto.ThumbUrl != null) card.ThumbUrl = dto.ThumbUrl;
            if (dto.Rarity != null) card.Rarity = dto.Rarity;
            if (dto.Value.HasValue) card.Value = dto.Value.Value;
            if (dto.SerieId.HasValue)
            {
                var serie = _db.Series.Find(dto.SerieId.Value);
                if (serie == null) return null;
                card.Serie = serie;
            }

            _db.SaveChanges();

            return new CardDto
            {
                Id = card.Id,
                Name = card.Name,
                ThumbUrl = card.ThumbUrl,
                Rarity = card.Rarity,
                Value = card.Value,
                Serie = new SerieBasicDto
                {
                    Id = card.Serie.Id,
                    Name = card.Serie.Name,
                    Genre = card.Serie.Genre,
                    ThumbUrl = card.Serie.ThumbUrl
                }
            };
        }

        public bool Delete(int id)
        {
            var card = _db.Cards.Find(id);
            if (card == null) return false;

            _db.Cards.Remove(card);
            _db.SaveChanges();
            return true;
        }
    }
}
