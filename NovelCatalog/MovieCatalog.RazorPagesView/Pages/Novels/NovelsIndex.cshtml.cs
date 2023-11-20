using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovelCatalog.Application.Novel;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Pages.Novels
{
    public class NovelIndexModel : PageModel
    {
        private readonly INovelService _novelService;

        public IReadOnlyCollection<Novel> Novels { get; set; } = new List<Novel>();

        public NovelIndexModel(INovelService novelService)
        {
            _novelService = novelService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Novels = await _novelService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _novelService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
