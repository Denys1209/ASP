using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Application.Categories;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.RazorPagesView.Pages.Categories
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
            var categories = await _categoryService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            Categories = categories.Models;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
