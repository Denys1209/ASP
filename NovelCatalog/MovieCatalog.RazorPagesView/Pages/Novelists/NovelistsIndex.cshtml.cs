using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Pages.Novelists
{
    public class NovelistsIndexModel : PageModel
    {
        private readonly INovelistService _novelistsService;

        public IReadOnlyCollection<Novelist> Novelists { get; set; } = new List<Novelist>();

        public NovelistsIndexModel(INovelistService novelistService)
        {
            _novelistsService = novelistService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Novelists = await _novelistsService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _novelistsService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
