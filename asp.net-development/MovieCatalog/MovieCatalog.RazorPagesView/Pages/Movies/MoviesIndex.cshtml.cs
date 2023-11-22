using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieCatalog.Application.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.RazorPagesView.Pages.Movies
{
    public class MoviesIndexModel : PageModel
    {
        private readonly IMovieService _movieService;

        public IReadOnlyCollection<Movie> Movies { get; set; } = new List<Movie>();

        public MoviesIndexModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var movies = await _movieService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            Movies = movies.Models;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _movieService.DeleteAsync(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
