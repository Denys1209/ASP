using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Application.Actors;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.RazorPagesView.Pages.Actors
{
    public class ActorsIndexModel : PageModel
    {
        private readonly IActorService _actorService;

        public IReadOnlyCollection<Actor> Actors { get; set; } = new List<Actor>();

        public ActorsIndexModel(IActorService actorService)
        {
            _actorService = actorService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var actors = await _actorService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            Actors = actors.Models;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _actorService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
