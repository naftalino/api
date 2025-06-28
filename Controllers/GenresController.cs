using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pd.Dto;
using pd.Models;
using pd.Services;

namespace pd.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenresController : ControllerBase
    {
        private readonly GenreService _genreService;

        public GenresController(GenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody] string name)
        {
            var genre = _genreService.CreateGenre(name);
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            try
            {
                var genre = _genreService.DeleteGenre(id);
            } catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao deletar gênero: " + ex.Message });
            }

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var genres = _genreService.GetGenres();
            return Ok(genres);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] GenreDto dto)
        {
            var genre = _genreService.UpdateGenre(id, dto);
            return Ok(new { message = "Gênero atualizado com sucesso." });
        }

    }

}
