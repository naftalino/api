using gacha.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pd.Dto;
using pd.Models;

namespace pd.Services
{
    public class SubgenreService
    {
        private readonly AppDbContext _db;
            
        public SubgenreService(AppDbContext db)
        {
            _db = db;
        }

        public List<SubgenresDto> GetAllSubgenres()
        {
            return _db.Subgenres
                .Select(sg => new SubgenresDto(sg.Id, sg.Name, sg.GenreId))
                .ToList();
        }

        public List<SubgenresDto> GetSubgenresByGenre(int genreId)
        {
            var list = _db.Subgenres
                .Where(sg => sg.GenreId == genreId)
                .Select(sg => new SubgenresDto(sg.Id, sg.Name, sg.GenreId))
                .ToList();
            return list;
        }

        public Subgenre CreateSubgenre(string name, int genreId)
        {
            var genre = _db.Genres.Find(genreId);
            if (genre == null)
                throw new Exception("Gênero não encontrado.");

            var sub = new Subgenre
            {
                Name = name,
                GenreId = genreId
            };

            _db.Subgenres.Add(sub);
            _db.SaveChanges();
            return sub;
        }
    }

}
