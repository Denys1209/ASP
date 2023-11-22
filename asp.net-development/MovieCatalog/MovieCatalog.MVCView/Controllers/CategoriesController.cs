using MovieCatalog.Application.Categories;
using MovieCatalog.Domain.Models;
using MovieCatalog.MVCView.Dtos.Shared;
using MovieCatalog.MVCView.ViewModels;

namespace MovieCatalog.MVCView.Controllers;

public sealed class CategoriesController : FilteredPaginatedController<CategoriesViewModel, Category, PaginatedFilteredDto>
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService) : base(categoryService)
    {
        _categoryService = categoryService;
    }
}