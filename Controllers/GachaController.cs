using Microsoft.AspNetCore.Mvc;
using pd.Services;

namespace pd.Controllers
{
    [Route("draw")]
    [ApiController]
    public class GachaController : ControllerBase
    {
        //[HttpGet]
        //[Route("card")]
        //public IActionResult DrawCard(int serieId, [FromServices] GachaService gachaService)
        //{
        //    try
        //    {
        //        var card = gachaService.DrawCard(serieId);
        //        return Ok(card);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("serie")]
        //public IActionResult DrawSerie(string genre, [FromServices] GachaService gachaService)
        //{
        //    try
        //    {
        //        var serie = gachaService.DrawSerie(genre);
        //        return Ok(serie);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
    }
}
