using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Application.Shared;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;
using MovieCatalog.MVCView.Dtos.Shared;
using MovieCatalog.MVCView.ViewModels.Shared;

namespace MovieCatalog.MVCView.Controllers;

public abstract class FilteredPaginatedController<TViewModel, TModel, TPaginatedFilteredDto> : Controller 
    where TViewModel : PaginatedFilteredViewModel, new()
    where TModel : Model
    where TPaginatedFilteredDto : PaginatedFilteredDto
{
    private readonly static IDictionary<int, PaginatedCollection<TModel>> cachedModels = new Dictionary<int, PaginatedCollection<TModel>>();

    private readonly ICrudService<TModel> _crudService;
    
    protected FilteredPaginatedController(ICrudService<TModel> crudService)
    {
        _crudService = crudService;
    }
    
    public virtual IActionResult Index(TPaginatedFilteredDto dto)
    {
        var (sortBy, sortDirection) = PaginatedFilteredViewModel.GetSortColumnAndDirection(dto.Sorting, dto.SortColumn, dto.IsSortChanged);

        if (dto.IsSortChanged || !string.IsNullOrWhiteSpace(dto.SearchTerm))
            cachedModels.Clear();

        if (dto.Page <= 0)
            dto.Page = 1;
        
        dto.SortDirection = sortDirection;
        dto.SortColumn = sortBy;
        
        return RedirectToAction("Filter", dto);
    }
    
    public virtual async Task<IActionResult> Filter(TPaginatedFilteredDto dto, CancellationToken cancellationToken = default)
    {
        var sortOrder = dto.SortDirection.Equals("Descending") ? SortOrder.Desc : SortOrder.Asc;
        
        var domainDto = new FilterPaginationDto(dto.SearchTerm, dto.Page, dto.PageSize, dto.SortColumn, sortOrder);
        if (!cachedModels.TryGetValue(dto.Page, out var models))
        {
            models = await _crudService.GetAllAsync(domainDto, cancellationToken);
            cachedModels.Add(dto.Page, models);
        }

        var viewModel = new TViewModel
        {
            Total = models.Total,
            SortColumn = dto.SortColumn,
            SortDirection = dto.SortDirection,
            CurrentPage = dto.Page,
            PageSize = dto.PageSize,
            SearchTerm = dto.SearchTerm
        };
        viewModel.AddModels(models.Models);
        return View("Index", viewModel);
    }
}