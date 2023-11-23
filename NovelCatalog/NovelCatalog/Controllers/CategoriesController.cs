using Microsoft.AspNetCore.Mvc;
using NovelCatalog.MVCView.ViewModels;
using NovelCatalog.Application.Categories;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.MVCView.Controllers
{
    public class CategoriesController : Controller
    {

        private const int InitialPageSize = 10;
        private const int FirstPageNumber = 1;

        private readonly ICategoryService _categoriesService;

        public CategoriesController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var viewModel = await FilterAsync(string.Empty, "Id", "Descending", InitialPageSize, FirstPageNumber, cancellationToken);
            return View(viewModel);
        }

        public IActionResult Create() => View(new Category { Name = string.Empty });

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category category, CancellationToken cancellationToken)
        {
            await _categoriesService.CreateAsync(category, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await _categoriesService.GetAsync(id, cancellationToken);
            if (category == null)
            {
                return NotFound();
            }

            return Json(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name")] Category category, CancellationToken cancellationToken)
        {
            await _categoriesService.UpdateAsync(category, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _categoriesService.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Filter(
       string searchTerm, string sortColumn, string sortDirection, int pageSize, int currentPage, CancellationToken cancellationToken)
        {
            var viewModel = await FilterAsync(searchTerm, sortColumn, sortDirection, pageSize, currentPage, cancellationToken);
            return View(nameof(Index), viewModel);
        }

        private async Task<CategoryViewModel> FilterAsync(
            string searchTerm, string sortColumn, string sortDirection, int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var dto = new FilterPaginationDto(searchTerm, pageNumber, pageSize, sortColumn,
                sortDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase) ? SortOrder.Desc : SortOrder.Asc);

            var categories = await _categoriesService.GetAllAsync(dto, cancellationToken);
            return new CategoryViewModel
            {
                Categories = categories.Models,
                TotalCategories = categories.Total,
                PageSize = dto.PageSize,
                CurrentPage = dto.PageNumber,
                SearchTerm = string.Empty,
                SortColumn = dto.SortColumn,
                SortDirection = dto.SortOrder == SortOrder.Asc ? "Ascending" : "Descending"
            };
        }

        public async Task<IActionResult> FilterGetJson(
            string searchTerm, CancellationToken cancellationToken) 
        {

            var dto = new FilterPaginationDto(searchTerm, 1, 10, "Name",
                "Ascending".Equals("Descending", StringComparison.OrdinalIgnoreCase) ? SortOrder.Desc : SortOrder.Asc);

            var categories = await _categoriesService.GetAllAsync(dto, cancellationToken);

            return Json(categories);

        }

        public async Task<IActionResult> GetAllByIds(string ids, CancellationToken cancellationToken)
        {
            string[] idStirngArray = ids.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var intIds = new List<int>(Array.ConvertAll(idStirngArray, int.Parse));

            var categories = await _categoriesService.GetAllModelsByIdsAsync(intIds, cancellationToken);

            return Json(categories);

        }
    }
}
