using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pd.Services;

namespace pd.Controllers
{
    [ApiController]
    [Route("api/draw")]
    public class DrawController : ControllerBase
    {
        private readonly GachaDrawService _drawService;

        public DrawController(GachaDrawService drawService)
        {
            _drawService = drawService;
        }

        [HttpGet("genre/{genreId}")]
        public IActionResult DrawFromGenre(int genreId)
        {
            var result = _drawService.DrawItemsFromGenre(genreId);
            return Ok(result);
        }

        [HttpGet("subgenre/{subgenreId}")]
        public IActionResult DrawSeriesFromSubgenre(int subgenreId)
        {
            var series = _drawService.DrawSeriesFromSubgenre(subgenreId);
            return Ok(series);
        }

        [HttpGet("serie/{serieId}")]
        public IActionResult DrawCardFromSerie(int serieId)
        {
            try
            {
                var card = _drawService.DrawCardFromSerie(serieId);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}
