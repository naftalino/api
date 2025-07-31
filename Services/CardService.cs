using gacha.Database;
using gacha.Dto;
using gacha.Models;
using Microsoft.EntityFrameworkCore;

namespace gacha.Services
{
    public class CardService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CardService> _logger;

        public CardService(AppDbContext db, ILogger<CardService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public (object Cards, int TotalCount) GetAll(int page = 1, int top = 10, string search = "")
        {
            var query = _db.Cards.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }

            int total = query.Count();

            var items = query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * top)
                .Take(top)
                .Select(c => new ReturnAllCardsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ThumbUrl = c.ThumbUrl,
                    Credits = c.Credits,
                    Value = c.Value,
                    PeopleOwned = c.PeopleOwned,
                    TimesPulled = c.TimesPulled,
                    SerieName = c.Serie.Name
                })
                .ToList();

            return (items, total);
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
                Value = card.Value,
                Serie = new SerieBasicDto
                {
                    Id = card.Serie.Id,
                    Name = card.Serie.Name,
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
                ThumbUrl = dto.ThumbUrl,
                Value = dto.Value,
                Serie = serie
            };

            _db.Cards.Add(card);
            _db.SaveChanges();

            return new CardDto
            {
                Id = card.Id,
                Name = card.Name,
                Value = card.Value,
                ThumbUrl = card.ThumbUrl,
                Serie = new SerieBasicDto
                {
                    Id = serie.Id,
                    Name = serie.Name,
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
                Value = card.Value,
                Serie =
                {
                    Name = card.Serie.Name
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

        public int IncreaseTimesPulled(int CardId)
        {
            Card? card = _db.Cards.Find(CardId);
            if (card == null)
            {
                throw new Exception("Erro. O Card não existe.");
            }

            int updated = card.TimesPulled++;
            _db.SaveChangesAsync();
            return updated;
        }
    }
}
