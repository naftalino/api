using gacha.Database;
using gacha.Models;
using gacha.Dto;
using Microsoft.EntityFrameworkCore;

namespace gacha.Services
{
    public class SerieService
    {
        private readonly AppDbContext _db;

        public SerieService(AppDbContext db)
        {
            _db = db;
        }

        public object GetAll(int page = 1, int pageSize = 10)
        {
            var query = _db.Series.AsQueryable();
            var total = query.Count();

            var series = query
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Genre,
                    s.Description,
                    s.ThumbUrl
                })
                .ToList();

            return new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = series
            };
        }


        public SerieDto? Get(int id)
        {
            var serie = _db.Series
            .FirstOrDefault(s => s.Id == id);

            if (serie == null) return null;

            return new SerieDto
            {
                Id = serie.Id,
                Name = serie.Name,
                Genre = serie.Genre,
                Description = serie.Description,
                ThumbUrl = serie.ThumbUrl
            };
        }

        public SerieWithPagedCardsDto? GetByIdPaged(int id, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 1;

            var serie = _db.Series
                .Include(s => s.Card)
                .FirstOrDefault(s => s.Id == id);

            if (serie == null) return null;

            var totalCards = serie.Card.Count;
            var pagedCards = serie.Card
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(card => new CardDtoPaged
                {
                    Id = card.Id,
                    Name = card.Name,
                    Rarity = card.Rarity,
                    ThumbUrl = card.ThumbUrl,
                })
                .ToList();

            return new SerieWithPagedCardsDto
            {
                Id = serie.Id,
                Name = serie.Name,
                Genre = serie.Genre,
                Description = serie.Description,
                ThumbUrl = serie.ThumbUrl,
                Cards = pagedCards,
                Page = page,
                PageSize = pageSize,
                TotalCards = totalCards
            };
        }

        public Serie? Create(CreateSerieDto serie)
        {
            var newSerie = new Serie
            {
                Name = serie.Name,
                Genre = serie.Genre,
                Description = serie.Description,
                ThumbUrl = serie.ThumbUrl
            };

            _db.Series.Add(newSerie);
            _db.SaveChanges();

            return newSerie;
        }

        public Serie? Update(int Id, SerieDto input)
        {
            var serie = _db.Series.Find(Id);
            if (serie == null) return null;

            if (input.Name != null) serie.Name = input.Name;
            if (input.Genre != null) serie.Genre = input.Genre;
            if (input.Description != null) serie.Description = input.Description;
            if (input.ThumbUrl != null) serie.ThumbUrl = input.ThumbUrl;

            _db.SaveChanges();
            return serie;
        }

        public bool Delete(int id)
        {
            var serie = _db.Series.Find(id);
            if (serie == null) return false;

            _db.Series.Remove(serie);
            _db.SaveChanges();
            return true;
        }
    }
}
