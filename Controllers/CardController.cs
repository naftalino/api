using gacha.Dto;
using gacha.Models;
using gacha.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult All() => Ok(_service.GetAll());

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
