using gacha.Dto;
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
            int total = list.Count;
            return Ok(new {totalItems = total, items = list});
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
            var subDto = new {
                Id = sub.Id,
                Name = sub.Name,
            };
            return Ok(subDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubgenre(int id)
        {
            try
            {
                var subgenre = _subgenreService.DeleteSubgenre(id);
            } catch (Exception ex){
                return NotFound(new { message = ex.Message});
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSubgenre(int id, [FromBody] SubgenreDto dto)
        {
            var subgenre = _subgenreService.UpdateSubgenre(id, dto);
            return Ok(new { message = "Subgênero atualizado com sucesso.", data = subgenre });
        }


        public class SubgenreDto
        {
            public string Name { get; set; } = "";
            public int GenreId { get; set; }
        }

        public class ReturnSubgenreDto
        {
            public string Name { get; set; } = "";
            public int SubgenreId { get; set; }
        }
    }

}
