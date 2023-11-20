using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovelCatalog.Application.Categories;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Pages.Categories
{
    public class CategoriesIndexModel : PageModel
    {
        private int _pageNumber = 1;

        private readonly ICategoryService _categoryService;

        public IReadOnlyCollection<Category> Categories { get; set; } = new List<Category>();

        public CategoriesIndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Categories = await _categoryService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty, _pageNumber), cancellationToken);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
