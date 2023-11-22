using Microsoft.AspNetCore.Mvc;
using NovelCatalog.Domain.Models;
using NovelCatalog.Domain;
using NovelCatalog.WebApi.Filters.Validation;
using NovelCatalog.WebApi.Dtos.Novels;
using NovelCatalog.Application.Novel;

namespace NovelCatalog.WebApi.Controllers;
public class NovelsController : Controller
{
    private readonly INovelService _novelService;

    public NovelsController(INovelService novelService)
    {
        _novelService = novelService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var novels = await _novelService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(novels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var novel = await _novelService.GetAsync(id, cancellationToken);
        if (novel is null)
            return NotFound();

        return Ok(novel);
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateNovelDto dto, CancellationToken cancellationToken)
    {
        var novel = new Novel { Title = dto.Title, ReleaseDate = dto.ReleaseDate, HowManyPages = dto.HowManyPages, Description = dto.Description, Categories = dto.Categories, Novelists = dto.Novelists };
        var id = await _novelService.CreateAsync(novel, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateNovelDto dto, CancellationToken cancellationToken)
    {
        var novel = new Novel { Id = dto.Id, Title = dto.Title, ReleaseDate = dto.ReleaseDate, HowManyPages = dto.HowManyPages, Description = dto.Description, Categories = dto.Categories, Novelists = dto.Novelists };
        await _novelService.UpdateAsync(novel, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _novelService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


}