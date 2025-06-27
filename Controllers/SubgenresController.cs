using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pd.Services;

namespace pd.Controllers
{
    [ApiController]
    [Route("api/subgenres")]
    public class SubgenresController : ControllerBase
    {
        private readonly SubgenreService _subgenreService;

        public SubgenresController(SubgenreService subgenreService)
        {
            _subgenreService = subgenreService;
        }

        [HttpGet]
        public IActionResult GetAllSubgenres()
        {
            var list = _subgenreService.GetAllSubgenres();
            return Ok(list);
        }

        [HttpGet("by-genre/{genreId}")]
        public IActionResult GetSubgenresByGenre(int genreId)
        {
            var list = _subgenreService.GetSubgenresByGenre(genreId);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateSubgenre([FromBody] SubgenreDto dto)
        {
            var sub = _subgenreService.CreateSubgenre(dto.Name, dto.GenreId);
            return Ok(sub);
        }

        public class SubgenreDto
        {
            public string Name { get; set; } = "";
            public int GenreId { get; set; }
        }
    }

}
