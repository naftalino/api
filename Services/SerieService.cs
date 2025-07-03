using gacha.Database;
using gacha.Dto;
using gacha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pd.Models;

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

        public SerieDto CreateSerieWithSubgenre(string name, string description, string thumbUrl, int subgenreId)
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
            var serieDto = new SerieDto
            {
                Id = serie.Id,
                Name = name,
                ThumbUrl = thumbUrl,
                Type = "subgenre",
                SubgenreId = subgenreId
            };
            return serieDto;
        }

        public List<object> GetAll()
        {
            return _db.Series
                .Include(s => s.Genre)
                .Include(s => s.SubGenre)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.ThumbUrl,
                    Type = s.SubGenreId.HasValue ? "subgenre" : "genre",
                    Genre = s.Genre != null ? s.Genre.Name : null,
                    Subgenre = s.SubGenre != null ? s.SubGenre.Name : null
                })
                .ToList<object>();
        }

        public object GetById(int id)
        {
            var serie = _db.Series
                .Include(s => s.Genre)
                .Include(s => s.SubGenre)
                .FirstOrDefault(s => s.Id == id);

            if (serie == null)
                throw new KeyNotFoundException("Série não encontrada.");

            return new
            {
                serie.Id,
                serie.Name,
                serie.ThumbUrl,
                Type = serie.SubGenreId.HasValue ? "subgenre" : "genre",
                Genre = serie.Genre?.Name,
                Subgenre = serie.SubGenre?.Name
            };
        }

        public void Update(int id, SerieDto dto)
        {
            var serie = _db.Series.FirstOrDefault(s => s.Id == id);
            if (serie == null)
                throw new KeyNotFoundException("Série não encontrada.");

            if(dto.Name != "")
            {
                serie.Name = dto.Name;
            }
            
            if(dto.ThumbUrl != "")
                serie.ThumbUrl = dto.ThumbUrl;

            if (dto.Type == "genre" && dto.GenreId.HasValue)
            {
                if (!_db.Genres.Any(g => g.Id == dto.GenreId.Value))
                    throw new ArgumentException("Gênero informado não existe.");

                serie.GenreId = dto.GenreId;
                serie.SubGenreId = null;
            }
            else if (dto.Type == "subgenre" && dto.SubgenreId.HasValue)
            {
                if (!_db.Subgenres.Any(sg => sg.Id == dto.SubgenreId.Value))
                    throw new ArgumentException("Subgênero informado não existe.");

                serie.SubGenreId = dto.SubgenreId;
                serie.GenreId = null;
            }

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var serie = _db.Series.Include(s => s.Card).FirstOrDefault(s => s.Id == id);
            if (serie == null)
                throw new KeyNotFoundException("Série não encontrada.");

            _db.Series.Remove(serie);
            _db.SaveChanges();
        }
    }
}
