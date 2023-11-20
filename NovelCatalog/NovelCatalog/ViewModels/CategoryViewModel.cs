using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.MVCView.ViewModels
{

    public sealed class CategoryViewModel
    {
        public IReadOnlyCollection<Category> Categories { get; set; } = null!;
        public int TotalCategories { get; set; }

        public string SortColumn { get; set; } = null!;
        public string SortDirection { get; set; } = null!;

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCategories / (decimal)PageSize);

        public string SearchTerm { get; set; } = string.Empty;

        public IReadOnlyCollection<SelectListItem> SortColumns { get; set; } = new[]
        {
            new SelectListItem("", "Id", true),
            new SelectListItem("Name", "Name"),
        };

        public IReadOnlyCollection<SelectListItem> SortDirections { get; set; } = new[]
        {
            new SelectListItem("Ascending", "Ascending"),
            new SelectListItem("Descending", "Descending", true)
        };

        public IDictionary<string, string> ToDictionaryParameters()
        {
            return new Dictionary<string, string>
        {
            { nameof(SearchTerm), SearchTerm },
            { nameof(SortColumn), SortColumn },
            { nameof(SortDirection), SortDirection },
            { nameof(PageSize), PageSize.ToString() },
            { nameof(CurrentPage), CurrentPage.ToString() }
        };
        }
    }
}
