using Microsoft.AspNetCore.Mvc;
using NovelCatalog.MVCView.ViewModels;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.MVCView.Controllers
{
    public class NovelistsController : Controller
    {

        private const int InitialPageSize = 10;
        private const int FirstPageNumber = 1;

        private readonly INovelistService _novelistsService;

        public NovelistsController(INovelistService novelistsService)
        {
            _novelistsService = novelistsService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var viewModel = await FilterAsync(string.Empty, "Id", "Descending", InitialPageSize, FirstPageNumber, cancellationToken);
            return View(viewModel);
        }

        public IActionResult Create() => View(new Novelist {
            FirstName = string.Empty,
        });

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,DateOfBirth,ImageUrl")] Novelist novelist, CancellationToken cancellationToken)
        {
            await _novelistsService.CreateAsync(novelist, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var novelist = await _novelistsService.GetAsync(id, cancellationToken);
            if (novelist == null)
            {
                return NotFound();
            }

            return Json(novelist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,FirstName,LastName,DateOfBirth,ImageUrl")] Novelist novelist, CancellationToken cancellationToken)
        {
            await _novelistsService.UpdateAsync(novelist, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _novelistsService.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Filter(
       string searchTerm, string sortColumn, string sortDirection, int pageSize, int currentPage, CancellationToken cancellationToken)
        {
            var viewModel = await FilterAsync(searchTerm, sortColumn, sortDirection, pageSize, currentPage, cancellationToken);
            return View(nameof(Index), viewModel);
        }

        private async Task<NovelistsViewModel> FilterAsync(
            string searchTerm, string sortColumn, string sortDirection, int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var dto = new FilterPaginationDto(searchTerm, pageNumber, pageSize, sortColumn,
                sortDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase) ? SortOrder.Desc : SortOrder.Asc);

            var novelists = await _novelistsService.GetAllAsync(dto, cancellationToken);
            return new NovelistsViewModel
            {
                Novelists = novelists.Models,
                TotalNovelists = novelists.Total,
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

            var dto = new FilterPaginationDto(searchTerm, 1, 10, "lastname",
                "Ascending".Equals("Descending", StringComparison.OrdinalIgnoreCase) ? SortOrder.Desc : SortOrder.Asc);

            var novelists = await _novelistsService.GetAllAsync(dto, cancellationToken);

            return Json(novelists);

        }
    }
}
