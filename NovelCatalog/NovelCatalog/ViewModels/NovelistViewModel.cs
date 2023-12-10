using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Domain.Models;
using NovelCatalog.MVCView.ViewModels.Shared;

namespace NovelCatalog.MVCView.ViewModels;

public sealed class NovelistsViewModel : PaginatedFilteredViewModel
{
    public NovelistsViewModel() : base(typeof(Novelist))
    {
    }

    public IReadOnlyCollection<Novelist> Novelists { get; set; } = null!;
    public int TotalNovelists { get; set; }


    public override IEnumerable<SelectListItem> SortColumns => new[]
    {
        new SelectListItem("", "Id", true),
        new SelectListItem("First Name", "FirstName"),
        new SelectListItem("Last Name", "LastName"),
        new SelectListItem("Date of Birth", "DateOfBirth")
    };

    public override IEnumerable<string> Columns => throw new NotImplementedException();

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