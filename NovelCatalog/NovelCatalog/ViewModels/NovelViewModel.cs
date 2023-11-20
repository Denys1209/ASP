using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.MVCView.ViewModels
{
    public class NovelViewModel
    {
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
        public int TotalNovelists { get; set; }

        public string SortColumn { get; set; } = null!;
        public string SortDirection { get; set; } = null!;

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalNovelists / (decimal)PageSize);

        public string SearchTerm { get; set; } = string.Empty;
        public string SearchTermForCategory { get; set; } = string.Empty;

        public IReadOnlyCollection<SelectListItem> SortColumns { get; set; } = new[]
        {
            new SelectListItem("", "Id", true),
            new SelectListItem("Title", "title"),
            new SelectListItem("Description", "description"),
            new SelectListItem("How many pages", "howManyPages"),
            new SelectListItem("Release date", "releaseDate"),
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