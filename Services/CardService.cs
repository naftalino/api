using gacha.Database;
using gacha.Dto;
using gacha.Models;
using Microsoft.EntityFrameworkCore;

namespace gacha.Services
{
    // nada
    public class CardService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CardService> _logger;

        public CardService(AppDbContext db, ILogger<CardService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public (List<Card> Cards, int TotalCount) GetAll(int page = 1, int top = 10, string? orderby = null)
        {
            var query = _db.Cards.Include(c => c.Serie).AsQueryable();

            // Ordenação dinâmica via EF.Property
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                var parts = orderby.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var field = parts[0];
                var direction = parts.Length > 1 ? parts[1].ToLower() : "asc";

                try
                {
                    query = direction == "desc"
                        ? query.OrderByDescending(x => EF.Property<object>(x, field))
                        : query.OrderBy(x => EF.Property<object>(x, field));
                    _logger.LogWarning(query.ToString());
                }catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                }
            }
                
            int total = query.Count();
            int totalPages = (int)Math.Ceiling((double)total / top);
            page = Math.Clamp(page, 1, totalPages);
            int skip = (page - 1) * top;

            var paged = query.Skip(skip).Take(top).ToList();

            return (paged, total);
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
                Value = dto.Value,
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
