using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Application.Categories;

namespace MovieCatalog.RazorPagesView.Pages.Categories
{
    public class CreateCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string name, CancellationToken cancellationToken)
        {
            await _categoryService.CreateAsync(new Domain.Models.Category { Name = name }, cancellationToken);
            return Redirect("/Categories/CategoriesIndex");
        }
    }
}
