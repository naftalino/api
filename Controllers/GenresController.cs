using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var genres = _genreService.GetGenres();
            return Ok(genres);
        }
    }

}
