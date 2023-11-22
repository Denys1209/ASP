using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Application.Categories;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;
using MovieCatalog.WebApiView.Dtos.Categories;
using MovieCatalog.WebApiView.Filters.Validation;

namespace MovieCatalog.WebApiView.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetAsync(id, cancellationToken);
        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = new Category { Name = dto.Name };
        var id = await _categoryService.CreateAsync(category, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = new Category { Id = dto.Id, Name = dto.Name };
        await _categoryService.UpdateAsync(category, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
