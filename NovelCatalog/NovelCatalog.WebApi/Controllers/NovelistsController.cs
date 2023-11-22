using Microsoft.AspNetCore.Mvc;
using NovelCatalog.Domain.Models;
using NovelCatalog.Domain;
using NovelCatalog.WebApi.Filters.Validation;
using NovelCatalog.WebApi.Dtos.Novelists;
using NovelCatalog.Application.Novelists;

namespace NovelCatalog.WebApi.Controllers;
public class NovelistsController : Controller
{
    private readonly INovelistService _novelistService;

    public NovelistsController(INovelistService novelistService)
    {
        _novelistService = novelistService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var novelists = await _novelistService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(novelists);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var novelist = await _novelistService.GetAsync(id, cancellationToken);
        if (novelist is null)
            return NotFound();

        return Ok(novelist);
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateNovelistDto dto, CancellationToken cancellationToken)
    {
        var novelist = new Novelist { FirstName = dto.FirstName, ImageUrl = dto.ImageUrl, DateOfBirth = dto.DateOfBirth, LastName = dto.LastName };
        var id = await _novelistService.CreateAsync(novelist, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateNovelistDto dto, CancellationToken cancellationToken)
    {
        var novelist = new Novelist { Id = dto.Id, FirstName = dto.FirstName, LastName = dto.LastName, DateOfBirth = dto.DateOfBirth, ImageUrl = dto.ImageUrl };
        await _novelistService.UpdateAsync(novelist, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _novelistService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


}
