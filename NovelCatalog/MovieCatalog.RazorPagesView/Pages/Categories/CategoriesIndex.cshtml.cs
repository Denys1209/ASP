using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Application.Categories;
using NovelCatalog.Domain.Models;
using NovelCatalog.RazorPagesView.Classes;

namespace NovelCatalog.RazorPagesView.Pages.Categories
{
    public class CategoriesIndexModel : PaginatedFilteredViewModel
    {

        private readonly ICategoryService _categoryService;

        public IReadOnlyCollection<Category> Categories { get; set; } = new List<Category>();

        public CategoriesIndexModel(ICategoryService categoryService) : base(typeof(Category))
        {
            _categoryService = categoryService;
        }

 public async Task OnGet(CancellationToken cancellationToken )
        {
            var responese = (await _categoryService.GetAllAsync(new Domain.FilterPaginationDto(
                SearchTerm: SearchTerm,
                PageNumber: CurrentPage,
                SortColumn: SortColumn,
                SortOrder: SortDirection == "Descending" ? Domain.SortOrder.Desc : Domain.SortOrder.Asc
                ), cancellationToken));
            Categories = responese.Models;
            Total = responese.Total;
        }

        public override IEnumerable<string> Columns
        {
            get
            {
                return new List<string> { "Name" };
            }
        }

        public override IEnumerable<SelectListItem> SortColumns
        {
            get
            {
                return new List<SelectListItem>
            {
                new SelectListItem("Name", "Name"),
            };
            }
        }

        public override string GetColumnValue(Model model, string column)
        {
            var category = model as Category;
            if (category == null)
            {
                throw new ArgumentException("Model should be of type Novel");
            }

            switch (column)
            {
                case "Name":
                    return category.Name;
                default:
                    throw new ArgumentException($"Unknown column: {column}");
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
