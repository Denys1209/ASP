using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain.Models;
using NovelCatalog.RazorPagesView.Classes;

namespace NovelCatalog.RazorPagesView.Pages.Novelists
{
    public class NovelistsIndexModel : PaginatedFilteredViewModel
    {
        private readonly INovelistService _novelistsService;

        public IReadOnlyCollection<Novelist> Novelists { get; set; } = new List<Novelist>();

        public NovelistsIndexModel(INovelistService novelistService) : base(typeof(Novelist))
        {
            _novelistsService = novelistService;
        }

        public override IEnumerable<string> Columns
        {
            get
            {
                return new List<string> { "FirstName", "LastName", "DateOfBirth" };
            }
        }

        public override IEnumerable<SelectListItem> SortColumns
        {
            get
            {
                return new List<SelectListItem>
            {
                new SelectListItem("FirstName", "FirstName"),
                new SelectListItem("LastName", "LastName"),
                new SelectListItem("DateOfBirth", "DateOfBirth")
            };
            }
        }

        public override string GetColumnValue(Model model, string column)
        {
            var novelist = model as Novelist;
            if (novelist == null)
            {
                throw new ArgumentException("Model should be of type Novelist");
            }

            switch (column)
            {
                case "FirstName":
                    return novelist.FirstName;
                case "LastName":
                    return novelist.LastName;
                case "DateOfBirth":
                    return novelist.DateOfBirth.ToString();
                default:
                    throw new ArgumentException($"Unknown column: {column}");
            }
        }
        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var response = (await _novelistsService.GetAllAsync(new Domain.FilterPaginationDto(
                SearchTerm: SearchTerm,
                PageNumber: CurrentPage,
                SortColumn: SortColumn,
                SortOrder: SortDirection == "Descending" ? Domain.SortOrder.Desc : Domain.SortOrder.Asc
                ), cancellationToken));
            Novelists = response.Models;
            Total = response.Total;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _novelistsService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
