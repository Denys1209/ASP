using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Application.Actors;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.RazorPagesView.Pages.Actors
{
    public class CreateActorModel : PageModel
    {
        private readonly IActorService _actorService;

        public CreateActorModel(IActorService actorService)
        {
            _actorService = actorService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken)
        {
            var actor = new Actor { FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth };
            await _actorService.CreateAsync(actor, cancellationToken);
            return Redirect("./ActorsIndex");
        }
    }
}
