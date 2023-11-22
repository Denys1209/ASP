using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Application.Actors;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;
using MovieCatalog.MVCView.ViewModels;

namespace MovieCatalog.MVCView.Controllers;

public class ActorsController : Controller
{
    private const int InitialPageSize = 10;
    private const int FirstPageNumber = 1;
    
    private readonly IActorService _actorsService;
    
    public ActorsController(IActorService actorsService)
    {
        _actorsService = actorsService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var viewModel = await FilterAsync(string.Empty, "Id", "Descending", InitialPageSize, FirstPageNumber, cancellationToken);
        return View(viewModel);
    }

    public IActionResult Create() => View(new Actor { FirstName = string.Empty });
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind("FirstName,LastName,DateOfBirth")] Actor actor, CancellationToken cancellationToken)
    {
        await _actorsService.CreateAsync(actor, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var actor = await _actorsService.GetAsync(id, cancellationToken);
        return View(actor);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id,FirstName,LastName,DateOfBirth")] Actor actor, CancellationToken cancellationToken)
    {
        await _actorsService.UpdateAsync(actor, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _actorsService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Filter(
        string searchTerm, string sortColumn, string sortDirection, int pageSize, int currentPage, CancellationToken cancellationToken)
    {
        var viewModel = await FilterAsync(searchTerm, sortColumn, sortDirection, pageSize, currentPage, cancellationToken);
        return View(nameof(Index), viewModel);
    }

    private async Task<ActorsViewModel> FilterAsync(
        string searchTerm, string sortColumn, string sortDirection, int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        var dto = new FilterPaginationDto(searchTerm, pageNumber, pageSize, sortColumn, 
            sortDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase) ? SortOrder.Desc : SortOrder.Asc);
        
        var actors = await _actorsService.GetAllAsync(dto, cancellationToken);
        return new ActorsViewModel
        {
            Actors = actors.Models,
            TotalActors = actors.Total,
            PageSize = dto.PageSize,
            CurrentPage = dto.PageNumber,
            SearchTerm = string.Empty,
            SortColumn = dto.SortColumn,
            SortDirection = dto.SortOrder == SortOrder.Asc ? "Ascending" : "Descending"
        };
    }
}
