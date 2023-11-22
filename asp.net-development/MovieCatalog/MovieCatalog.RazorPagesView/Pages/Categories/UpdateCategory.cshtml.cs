using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Application.Categories;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.RazorPagesView.Pages.Categories
{
    public class UpdateCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetAsync(id, cancellationToken);
            if (category == null)
                return NotFound();

            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateAsync(new Category { Id = Category.Id, Name = name }, cancellationToken);
            return Redirect("/Categories/CategoriesIndex");
        }
    }
}
