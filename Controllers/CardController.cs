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
        public IActionResult All([FromQuery] int page = 1, [FromQuery] int top = 10, [FromQuery] string? orderby = null)
        {
            var data = _service.GetAll().AsQueryable();
            int total = data.Count();

            if (!string.IsNullOrEmpty(orderby))
            {
                var parts = orderby.Split(' ');
                var field = parts[0];
                var direction = parts.Length > 1 ? parts[1] : "asc";

                var prop = typeof(GetAllDto).GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null)
                {
                    data = direction.ToLower() == "desc"
                        ? data.OrderByDescending(x => prop.GetValue(x, null))
                        : data.OrderBy(x => prop.GetValue(x, null));
                }
            }

            int totalPages = (int)Math.Ceiling((double)total / top);
            page = Math.Clamp(page, 1, totalPages);
            int skip = (page - 1) * top;

            var paged = data.Skip(skip).Take(top).ToList();

            return Ok(new
            {
                data = paged,
                recordsTotal = total,
                recordsFiltered = total
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

        [HttpGet("cardproxy")]
        public async Task<IActionResult> CardProxy([FromQuery] int page = 1, [FromQuery] int top = 10, [FromQuery] string? orderby = null)
        {
            var client = new HttpClient();
            var url = $"https://adorabat.squareweb.app/card?page={page}&top={top}";

            if (!string.IsNullOrEmpty(orderby))
            {
                url += $"&orderby={Uri.EscapeDataString(orderby)}";
            }

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }

    }
}
