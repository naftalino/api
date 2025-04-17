using gacha.Services;
using Gacha.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Gacha.Controllers;

[ApiController]
[Route("collection")]
public class CollectionController : ControllerBase
{
    private readonly CollectionService _service;

    public CollectionController(CollectionService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<PaginatedCollectionDto>> Get(int userId, int page = 1, int pageSize = 10)
    {
        try
        {
            var result = await _service.GetByUserIdAsync(userId, page, pageSize);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<CollectionDto>> CreateOrUpdateAsync([FromBody] CreateCollectionDto dto)
    {
        try
        {
            var result = await _service.CreateOrUpdateAsync(dto);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
