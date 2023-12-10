using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Domain.Models;
using NovelCatalog.MVCView.ViewModels.Shared;

namespace NovelCatalog.MVCView.ViewModels;
public sealed class NovelViewModel : PaginatedFilteredViewModel
{
    public NovelViewModel() : base(typeof(Novel))
    {
    }

    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public uint HowManyPages { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public IReadOnlyCollection<Novel> Novels { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<Novelist> Novelists { get; set; } = new List<Novelist>();
    public List<int> SelectedCategoryIds { get; set; } = new List<int>();
    public List<int> SelectedNovelistIds { get; set; } = new List<int>();


    public override IEnumerable<SelectListItem> SortColumns { get; } = new[]
    {
            new SelectListItem("", "Id", true),
            new SelectListItem("Title", "title"),
            new SelectListItem("Description", "description"),
            new SelectListItem("How many pages", "howManyPages"),
            new SelectListItem("Release date", "releaseDate"),
    };

    public override IEnumerable<string> Columns
    {
        get
        {
            return new List<string> { "Title", "Description", "HowManyPages", "ReleaseDate" };
        }
    }
    public override string GetColumnValue(Model model, string column)
    {
        throw new NotImplementedException();
    }

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