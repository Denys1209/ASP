using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Application.Novel;
using NovelCatalog.Domain.Models;
using NovelCatalog.RazorPagesView.Classes;

namespace NovelCatalog.RazorPagesView.Pages.Novels
{
    public class NovelIndexModel : PaginatedFilteredViewModel
    {
        private readonly INovelService _novelService;

        public IReadOnlyCollection<Novel> Novels { get; set; } = new List<Novel>();

        public NovelIndexModel(INovelService novelService) : base(typeof(Novel))
        {
            _novelService = novelService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var response = (await _novelService.GetAllAsync(new Domain.FilterPaginationDto(
                SearchTerm: SearchTerm,
                PageNumber: CurrentPage,
                SortColumn: SortColumn,
                SortOrder: SortDirection == "Descending" ? Domain.SortOrder.Desc : Domain.SortOrder.Asc
                ), cancellationToken));
            Novels = response.Models;
            Total = response.Total;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _novelService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }

        public override IEnumerable<string> Columns
        {
            get
            {
                return new List<string> { "Title", "Description", "HowManyPages", "ReleaseDate" };
            }
        }

        public override IEnumerable<SelectListItem> SortColumns
        {
            get
            {
                return new List<SelectListItem>
            {
                new SelectListItem("Title", "Title"),
                new SelectListItem("Description", "Description"),
                new SelectListItem("HowManyPages", "HowManyPages"),
                new SelectListItem("ReleaseDate", "ReleaseDate")
            };
            }
        }

        public override string GetColumnValue(Model model, string column)
        {
            var novel = model as Novel;
            NotNull(novel, "Model should be of type Novel");

            return column switch
            {
                "Title" => novel!.Title,
                "Description" => novel!.Description!,
                "HowManyPages" => novel!.HowManyPages.ToString(),
                "ReleaseDate" => novel!.ReleaseDate.ToString(),
                _ => throw new ArgumentException($"Unknown column: {column}"),
            };
        }

        private static void NotNull<T>(T? value, string message = "Value should not be null") where T : class
        {
            if (value == null)
                throw new ArgumentException(message);
        }
    }
}
