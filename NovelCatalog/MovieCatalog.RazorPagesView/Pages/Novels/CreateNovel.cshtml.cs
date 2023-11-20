using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Novel;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Pages.Novels
{
    public class CreateNovelModel : PageModel
    {
        private readonly INovelService _novelService;
        private readonly INovelistService _novelistsService;
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public List<int> CategoriesIds { get; set; } = new List<int>();
        [BindProperty]
        public List<int> NovelistsIds { get; set; } = new List<int>();

        public IReadOnlyCollection<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IReadOnlyCollection<SelectListItem> Novelists { get; set; } = new List<SelectListItem>();

        public CreateNovelModel(INovelService novelService, INovelistService novelistService, ICategoryService categoryService)
        {
            _novelService = novelService;
            _novelistsService = novelistService;
            _categoryService = categoryService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            var novelists = await _novelistsService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);

            Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            Novelists = novelists.Select(a => new SelectListItem(a.ToString(), a.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string title, string description, DateOnly releaseDate,uint howManyPages , CancellationToken cancellationToken)
        {
            var categories = CategoriesIds.Count() == 0 ? new List<Category?>() : await _categoryService.GetAllModelsByIdsAsync(CategoriesIds, cancellationToken);
            var novelists = NovelistsIds.Count() == 0 ? new List<Novelist?>() : await _novelistsService.GetAllModelsByIdsAsync(NovelistsIds, cancellationToken);

            var movie = new Novel {
                Title = title,
                Description = description,
                ReleaseDate = releaseDate,
                Categories = (ICollection<Category>)categories,
                Novelists = (ICollection<Novelist>)novelists,
                HowManyPages = howManyPages,
            };
            
            await _novelService.CreateAsync(movie, cancellationToken);
            return Redirect("./NovelsIndex");
        }
    }
}
