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

        public Serie CreateSerieWithGenre(string name, string description, string thumbUrl, int genreId)
        {
            var genre = _db.Genres.Find(genreId);
            if (genre == null)
                throw new Exception("Gênero não encontrado.");

            var serie = new Serie
            {
                Name = name,
                Description = description,
                ThumbUrl = thumbUrl,
                GenreId = genreId
            };

            _db.Series.Add(serie);
            _db.SaveChanges();
            return serie;
        }

        public Serie CreateSerieWithSubgenre(string name, string description, string thumbUrl, int subgenreId)
        {
            var sub = _db.Subgenres.Find(subgenreId);
            if (sub == null)
                throw new Exception("Subgênero não encontrado.");

            var serie = new Serie
            {
                Name = name,
                Description = description,
                ThumbUrl = thumbUrl,
                SubGenreId = subgenreId
            };

            _db.Series.Add(serie);
            _db.SaveChanges();
            return serie;
        }
    }

}
