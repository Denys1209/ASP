using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Application.Actors;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.RazorPagesView.Pages.Actors
{
    public class UpdateActorModel : PageModel
    {
        private readonly IActorService _actorService;

        [BindProperty]
        public Actor Actor { get; set; } = null!;

        public UpdateActorModel(IActorService actorService)
        {
            _actorService = actorService;
        }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            var actor = await _actorService.GetAsync(id, cancellationToken);
            if (actor is null)
                return NotFound();

            Actor = actor;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken) 
        {
            var actor = new Actor { Id = Actor.Id, FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth };
            await _actorService.UpdateAsync(actor, cancellationToken);
            return Redirect("../ActorsIndex");
        }
    }
}
