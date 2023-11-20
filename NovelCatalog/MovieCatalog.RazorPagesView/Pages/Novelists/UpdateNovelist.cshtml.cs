using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Pages.Novelists
{
    public class UpdateNovelistModel : PageModel
    {
        private readonly INovelistService _novelistService;

        [BindProperty]
        public Novelist novelists { get; set; } = null!;

        public UpdateNovelistModel(INovelistService novelistService)
        {
            _novelistService = novelistService;
        }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            var novelist = await _novelistService.GetAsync(id, cancellationToken);
            if (novelist is null)
                return NotFound();

            this.novelists = novelist;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string firstName, string lastName, DateOnly dateOfBirth, string imageUrl, CancellationToken cancellationToken) 
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                imageUrl = "https://www.shutterstock.com/image-photo/young-female-novelist-baggy-sweater-260nw-1864435981.jpg";
            var novelist = new Novelist { Id = novelists.Id, FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth, ImageUrl = imageUrl };
            await _novelistService.UpdateAsync(novelist, cancellationToken);
            return Redirect("../NovelistsIndex");
        }
    }
}
