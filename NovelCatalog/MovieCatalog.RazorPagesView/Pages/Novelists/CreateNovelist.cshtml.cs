using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Pages.Novelists
{
    public class CreateNovelistsModel : PageModel
    {
        private readonly INovelistService _actorService;

        public CreateNovelistsModel(INovelistService actorService)
        {
            _actorService = actorService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string firstName, string lastName, DateOnly dateOfBirth, string imageUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                imageUrl = "https://www.shutterstock.com/image-photo/young-female-novelist-baggy-sweater-260nw-1864435981.jpg";
            var actor = new Novelist { FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth, ImageUrl = imageUrl };
            await _actorService.CreateAsync(actor, cancellationToken);
            return Redirect("./NovelistsIndex");
        }
    }
}
