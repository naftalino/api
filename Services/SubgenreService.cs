using gacha.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pd.Dto;
using pd.Models;
using static pd.Controllers.SubgenresController;

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

        public SubgenresDto DeleteSubgenre(int id)
        {
            var subgenre = _db.Subgenres.Include(sg => sg.Series).FirstOrDefault(sg => sg.Id == id);
            if (subgenre == null)
                throw new Exception("Subgênero não encontrado.");

            _db.Subgenres.Remove(subgenre);
            _db.SaveChanges();
            return new SubgenresDto(subgenre.Id, subgenre.Name, subgenre.GenreId);
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

        public SubgenreDto UpdateSubgenre(int id, SubgenreDto dto)
        {
            var subgenre = _db.Subgenres.FirstOrDefault(sg => sg.Id == id);
            if (subgenre == null)
                throw new Exception("Subgênero não encontrado");

            subgenre.Name = dto.Name;

            if (dto.GenreId != 0)
            {
                var genreExists = _db.Genres.Any(g => g.Id == dto.GenreId);
                if (!genreExists)
                    throw new Exception("Subgênero não existe.");

                subgenre.GenreId = dto.GenreId;
            }
            var subgenreDto = new SubgenreDto
            {
                Name = subgenre.Name,
                GenreId = subgenre.Id
            };
            _db.SaveChanges();
            return subgenreDto;
        }

    }

}
