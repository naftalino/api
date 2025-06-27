using gacha.Database;
using Microsoft.AspNetCore.Mvc;
using pd.Models;
using static pd.Dto.GenreDto;

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

        public List<GenresDto> GetGenres()
        {
            return _db.Genres
                .Select(g => new GenresDto(g.Id, g.Name, g.Series.Count()))
                .ToList();
        }
    }
}
