using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Novel;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;
using NovelCatalog.MVCView.ViewModels;

namespace NovelCatalog.MVCView.Controllers
{
    public class NovelsController : Controller
    {
        private const int InitialPageSize = 10;
        private const int FirstPageNumber = 1;

        private readonly INovelService _novelsService;
        private readonly ICategoryService _categoriesService;
        private readonly INovelistService _novelistsService;

        public NovelsController(INovelService novelsService, ICategoryService categoriesService, INovelistService novelistsService)
        {
            _novelsService = novelsService;
            _categoriesService = categoriesService;
            _novelistsService = novelistsService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var viewModel = await FilterAsync(string.Empty, "Id", "Descending", InitialPageSize, FirstPageNumber, cancellationToken);
            return View(viewModel);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {

            var dto = new FilterPaginationDto();
            var categories = (await _categoriesService.GetAllAsync(dto, cancellationToken)).ToList();
            var novelists = (await _novelistsService.GetAllAsync(dto, cancellationToken)).ToList();

            var model = new NovelViewModel
            {
                Title = string.Empty,
                Categories = categories,
                Novelists = novelists,
                SelectedCategoryIds = new List<int> { },
                SelectedNovelistIds = new List<int> { },
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,ReleaseDate,HowManyPages,SelectedCategoryIds,SelectedNovelistIds")] NovelViewModel viewModel, CancellationToken cancellationToken)
        {
            var dto = new FilterPaginationDto();
            var categories = (await _categoriesService.GetAllModelsByIdsAsync(viewModel.SelectedCategoryIds, cancellationToken)).ToList();
            var novelists = (await _novelistsService.GetAllModelsByIdsAsync(viewModel.SelectedNovelistIds, cancellationToken)).ToList();
            Novel novel = new()
            {
                Categories = categories,
                Novelists = novelists,
                Title = viewModel.Title,
                Description = viewModel.Description,
                HowManyPages = viewModel.HowManyPages,
                ReleaseDate = viewModel.ReleaseDate,
            };
            await _novelsService.CreateAsync(novel, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var dto = new FilterPaginationDto();
            var novel = await _novelsService.GetAsync(id, cancellationToken);

            var categories = (await _categoriesService.GetAllAsync(dto, cancellationToken)).ToList();
            var novelists = (await _novelistsService.GetAllAsync(dto, cancellationToken)).ToList();
            var viewModel = new NovelViewModel
            {
                Id = novel.Id,
                Title = novel.Title,
                Description = novel.Description,
                HowManyPages = novel.HowManyPages,
                ReleaseDate = novel.ReleaseDate,
                Categories = categories,
                Novelists = novelists,
                SelectedCategoryIds = novel.Categories.Select(c => c.Id).ToList(),
                SelectedNovelistIds = novel.Novelists.Select(n => n.Id).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,ReleaseDate,HowManyPages,SelectedCategoryIds,SelectedNovelistIds")] NovelViewModel viewModel, CancellationToken cancellationToken)
        {
            
            var dto = new FilterPaginationDto();
            var categories = (await _categoriesService.GetAllModelsByIdsAsync(viewModel.SelectedCategoryIds, cancellationToken)).ToList();
            var novelists = (await _novelistsService.GetAllModelsByIdsAsync(viewModel.SelectedNovelistIds, cancellationToken)).ToList();
            Novel novel = new Novel
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Description = viewModel.Description,
                HowManyPages = viewModel.HowManyPages,
                ReleaseDate = viewModel.ReleaseDate,
                Categories = categories,
                Novelists = novelists,
            };

            await _novelsService.UpdateAsync(novel, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _novelsService.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Filter(
     string searchTerm, string sortColumn, string sortDirection, int pageSize, int currentPage, CancellationToken cancellationToken)
        {
            var viewModel = await FilterAsync(searchTerm, sortColumn, sortDirection, pageSize, currentPage, cancellationToken);
            return View(nameof(Index), viewModel);
        }

        private async Task<NovelViewModel> FilterAsync(
            string searchTerm, string sortColumn, string sortDirection, int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var dto = new FilterPaginationDto(searchTerm, pageNumber, pageSize, sortColumn,
                sortDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase) ? SortOrder.Desc : SortOrder.Asc);


            var novels = await _novelsService.GetAllAsync(dto, cancellationToken);
            var novelists =  await _novelistsService.GetAllAsync(dto, cancellationToken);
            var categoreis = await _categoriesService.GetAllAsync(dto, cancellationToken);
            return new NovelViewModel
            {
                Title = "",
                Novels = novels.Models,
                TotalNovelists = novels.Total,
                PageSize = dto.PageSize,
                CurrentPage = dto.PageNumber,
                SearchTerm = string.Empty,
                SortColumn = dto.SortColumn,
                SortDirection = dto.SortOrder == SortOrder.Asc ? "Ascending" : "Descending"
            };
        }

      
    }
}
