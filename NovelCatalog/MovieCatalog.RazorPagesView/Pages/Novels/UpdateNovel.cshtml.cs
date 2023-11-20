using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Novel;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Pages.Novels
{
    public class UpdateNovelModel : PageModel
    {
        private readonly INovelService _novelService;
        private readonly INovelistService _novelistService;
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public Novel novel { get; set; } = null!;
        [BindProperty]
        public List<int> CategoriesIds { get; set; }
        [BindProperty]
        public List<int> NovelistsIds { get; set; } = new List<int>();

        public IReadOnlyCollection<SelectListItem> AllCategories { get; set; } = new List<SelectListItem>();
        public IReadOnlyCollection<SelectListItem> AllNovelists { get; set; } = new List<SelectListItem>();
       

        public UpdateNovelModel(INovelService novelService, INovelistService novelistService, ICategoryService categoryService)
        {
            _novelService = novelService;
            _novelistService = novelistService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            var novel = await _novelService.GetAsync(id, cancellationToken);
            if (novel is null)
                return NotFound();

            this.novel = novel;

            var categories = await _categoryService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            var novelists = await _novelistService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            List<string> categoriesIds = novel.Categories.Select(c => c.Id.ToString()).ToList();
            List<string>  novelistsIds = novel.Novelists.Select(c => c.Id.ToString()).ToList();
            AllCategories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString(), categoriesIds.Contains(c.Id.ToString()))).ToList();
            AllNovelists = novelists.Select(c => new SelectListItem(c.ToString(), c.Id.ToString(), novelistsIds.Contains(c.Id.ToString()))).ToList();
           

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string title, string description, DateOnly releaseDate, uint howManyPages, CancellationToken cancellationToken)
        {
            var categories = CategoriesIds.Count() == 0 ? new List<Category?>() : await _categoryService.GetAllModelsByIdsAsync(CategoriesIds, cancellationToken);
            var novelists = NovelistsIds.Count() == 0 ? new List<Novelist?>() : await _novelistService.GetAllModelsByIdsAsync(NovelistsIds, cancellationToken);

            var novel = new Novel
            {
                Id = this.novel.Id,
                Title = title,
                Description = description,
                ReleaseDate = releaseDate,
                HowManyPages = howManyPages,
                Categories = (ICollection<Category>)categories,
                Novelists = (ICollection<Novelist>)novelists,
            };

            await _novelService.UpdateAsync(novel, cancellationToken);
            return Redirect("../NovelsIndex");
        }
    }
}
