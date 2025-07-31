using gacha.Database;
using Microsoft.EntityFrameworkCore;
using pd.Dto;
using pd.Models;

namespace pd.Services
{
    public class GenreService
    {
        private readonly AppDbContext _db;

        public GenreService(AppDbContext db)
        {
            _db = db;
        }

        public Genre CreateGenre(string name)
        {
            var genre = new Genre { Name = name };
            _db.Genres.Add(genre);
            _db.SaveChanges();
            return genre;
        }

        public Genre UpdateGenre(int id, GenreDto dto)
        {
            var genre = _db.Genres.FirstOrDefault(g => g.Id == id);
            if (genre == null)
                throw new Exception("Gênero não encontrado.");

            genre.Name = dto.Name;
            _db.SaveChanges();
            return genre;
        }

        public Genre DeleteGenre(int id)
        {
            var genre = _db.Genres.Include(g => g.Subgenres).Include(g => g.Series).FirstOrDefault(g => g.Id == id);
            if (genre == null)
                throw new ArgumentException("Gênero não encontrado.");
            _db.Genres.Remove(genre);
            _db.SaveChanges();
            return genre;
        }

        public List<object> GetGenres()
        {
            return _db.Genres
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    total_series = _db.Series.Count(s => s.GenreId == g.Id),
                    total_subgenres = _db.Subgenres.Count(sg => sg.GenreId == g.Id)
                })
                .ToList<object>();
        }
    }
}
