using gacha.Dto;
using gacha.Models;
using gacha.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gacha.Controllers
{
    [ApiController]
    [Route("api/series")]
    public class SeriesController : ControllerBase
    {
        private readonly SerieService _serieService;

        public SeriesController(SerieService serieService)
        {
            _serieService = serieService;
        }

        [HttpPost("genre")]
        public IActionResult CreateSerieWithGenre([FromBody] SerieGenreDto dto)
        {
            var serie = _serieService.CreateSerieWithGenre(dto.Name, dto.Description, dto.ThumbUrl, dto.GenreId);
            return Ok(serie);
        }

        [HttpPost("subgenre")]
        public IActionResult CreateSerieWithSubgenre([FromBody] SerieSubgenreDto dto)
        {
            var serie = _serieService.CreateSerieWithSubgenre(dto.Name, dto.Description, dto.ThumbUrl, dto.SubgenreId);
            return Ok(serie);
        }

        public class SerieGenreDto
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string ThumbUrl { get; set; } = "";
            public int GenreId { get; set; }
        }

        public class SerieSubgenreDto
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string ThumbUrl { get; set; } = "";
            public int SubgenreId { get; set; }
        }
    }

}
