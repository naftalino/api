using gacha.Database;
using gacha.Dto;
using gacha.Models;
using Microsoft.EntityFrameworkCore;
using pd.Dto;

namespace pd.Services
{
    public class GachaDrawService
    {
        private readonly AppDbContext _db;
        private readonly Random _rng = new();

        public GachaDrawService(AppDbContext db)
        {
            _db = db;
        }

        // Sorteia 6 itens de um gênero (séries diretas + subgêneros)
        public List<DrawItem> DrawItemsFromGenre(int genreId)
        {
            var subgenres = _db.Subgenres
                .Where(sg => sg.GenreId == genreId)
                .Select(sg => new DrawItem
                {
                    Id = sg.Id,
                    Name = sg.Name,
                    Type = "subgenre",
                    ThumbUrl = null
                });

            var series = _db.Series
                .Where(s => s.GenreId == genreId && s.SubGenreId == null)
                .Select(s => new DrawItem
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = "serie",
                    ThumbUrl = s.ThumbUrl
                });

            var pool = subgenres.Concat(series).ToList();

            return pool.OrderBy(_ => _rng.Next()).Take(6).ToList();
        }

        // Sorteia 6 séries de um subgênero
        public List<Serie> DrawSeriesFromSubgenre(int subgenreId)
        {
            var series = _db.Series
                .Where(s => s.SubGenreId == subgenreId)
                .ToList();

            return series.OrderBy(_ => _rng.Next()).Take(6).ToList();
        }

        // Sorteia 1 card aleatório de uma série
        public CardsDto DrawCardFromSerie(int serieId)
        {
            var cards = _db.Cards
                .Include(c => c.Serie)
                .Where(c => c.SerieId == serieId)
                .ToList();

            if (!cards.Any())
                throw new Exception("Essa série não possui cards.");
            var info = cards[_rng.Next(cards.Count)];
            var card = new CardsDto(info.Id, info.Name, new SerieInfoDto(info.Serie.Name));
            return card;
        }
    }

}
