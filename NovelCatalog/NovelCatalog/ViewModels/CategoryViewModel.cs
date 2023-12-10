using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Domain.Models;
using NovelCatalog.MVCView.ViewModels.Shared;

namespace NovelCatalog.MVCView.ViewModels;
public sealed class CategoryViewModel : PaginatedFilteredViewModel
{
    public IReadOnlyCollection<Category> Categories { get; set; } = null!;
    public int TotalCategories { get; set; }

    public override IEnumerable<SelectListItem> SortColumns { get; } = new[]
    {
            new SelectListItem("", "Id", true),
            new SelectListItem("Name", "Name"),
        };

    public override IEnumerable<string> Columns
    {
        get
        {
            return new List<string> { "Name" };
        }
    }

    public CategoryViewModel() : base(typeof(Category))
    { }
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
}
