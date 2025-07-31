using gacha.Services;
using Microsoft.AspNetCore.Mvc;
using pd.Services;

namespace pd.Controllers
{
    [ApiController]
    [Route("api/draw")]
    public class DrawController : ControllerBase
    {
        private readonly GachaDrawService _drawService;
        private readonly CardService _cardService;

        public DrawController(GachaDrawService drawService, CardService cardService)
        {
            _drawService = drawService;
            _cardService = cardService;
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
                _cardService.IncreaseTimesPulled(card.Id);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
