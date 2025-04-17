using gacha.Dto;
using gacha.Models;
using gacha.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gacha.Controllers
{
    [ApiController]
    [Route("serie")]
    public class SerieController : ControllerBase
    {
        private readonly SerieService _service;

        public SerieController(SerieService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _service.Get(id) is { } serie ? Ok(serie) : NotFound(new { error = "Série não encontrada." });

        [HttpGet("cards/{id}")]
        public IActionResult GetById(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
    _service.GetByIdPaged(id, page, pageSize) is { } result
        ? Ok(result)
        : NotFound(new { error = "Série não encontrada." });

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var result = _service.GetAll(page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSerieDto s) => Ok(_service.Create(s));

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SerieDto s) =>
            _service.Update(id, s) is { } updated ? Ok(updated) : NotFound(new { error = "Série não encontrada." });

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _service.Delete(id) ? Ok() : NotFound(new { error = "Série não encontrada." });
    }
}
