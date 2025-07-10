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

        // rota que cria obra para um gênero comum.
        [HttpPost("genre")]
        public IActionResult CreateSerieWithGenre([FromBody] SerieGenreDto dto)
        {
            var serie = _serieService.CreateSerieWithGenre(dto.Name, dto.Description, dto.ThumbUrl, dto.GenreId);
            var serieDto = new {
                Id = serie.Id,
                Name = serie.Name,
                GenreId = serie.GenreId,
                ThumbUrl = serie.ThumbUrl
            };
            return Ok(serieDto);    
        }

        // rota que cria obra para um subgênero.
        [HttpPost("subgenre")]
        public IActionResult CreateSerieWithSubgenre([FromBody] SerieSubgenreDto dto)
        {
            var serie = _serieService.CreateSerieWithSubgenre(dto.Name, dto.Description, dto.ThumbUrl, dto.SubgenreId);
            var serieDto = new
            {
                Id = serie.Id,
                Name = serie.Name,
                ThumbUrl = serie.ThumbUrl,
                Type = "subgenre",
                SubgenreId = serie.SubgenreId
            };
            return Ok(serieDto);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var series = _serieService.GetAllB(page, pageSize, search);
            return Ok(series);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var serie = _serieService.GetById(id);
                return Ok(serie);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SerieDto dto)
        {
            try
            {
                _serieService.Update(id, dto);
                return Ok(new { message = "Série atualizada com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _serieService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DTOs jogados aqui mesmo, depois arrumo rsrsrs (nunca irei)
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
            public string Type { get; set; } = "subgenre";
            public int SubgenreId { get; set; }
        }
    }

}
