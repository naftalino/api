using gacha.Dto;
using gacha.Models;
using gacha.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gacha.Controllers
{
    [ApiController]
    [Route("card")]
    public class CardController : ControllerBase
    {
        private readonly CardService _service;

        public CardController(CardService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _service.GetById(id) is { } c ? Ok(c) : NotFound(new { error = "Card não encontrado." });

        [HttpGet]
        public IActionResult All(
            [FromQuery(Name = "$skip")] int skip = 0,
            [FromQuery(Name = "$top")] int take = 10,
            [FromQuery(Name = "$orderby")] string? orderBy = null)
        {
            var data = _service.GetAll();

            if (!string.IsNullOrEmpty(orderBy))
            {
                var parts = orderBy.Split(' ');
                var field = parts[0];
                var direction = parts.Length > 1 ? parts[1] : "asc";

                data = direction.ToLower() == "desc"
                    ? data.OrderByDescending(x => EF.Property<object>(x, field))
                    : data.OrderBy(x => EF.Property<object>(x, field));
            }

            var total = data.Count();
            var paginated = data.Skip(skip).Take(take).ToList();

            return Ok(new
            {
                result = paginated,
                count = data.Count()
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCardDto dto)
        {
            var created = _service.Create(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCardDto dto)
        {
            var updated = _service.Update(id, dto);
            return updated != null ? Ok(updated) : NotFound(new { error = "Carta não encontrada." });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _service.Delete(id) ? Ok() : NotFound(new { error = "Card não encontrado." });
    }
}
