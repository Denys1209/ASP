using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieCatalog.Application.Actors;
using MovieCatalog.Application.Categories;
using MovieCatalog.Application.Movies;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.RazorPagesView.Pages.Movies
{
    public class CreateMovieModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
        public List<int> ActorIds { get; set; } = new List<int>();

        public IReadOnlyCollection<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IReadOnlyCollection<SelectListItem> Actors { get; set; } = new List<SelectListItem>();

        public CreateMovieModel(IMovieService movieService, IActorService actorService, ICategoryService categoryService)
        {
            _movieService = movieService;
            _actorService = actorService;
            _categoryService = categoryService;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            var actors = await _actorService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);

            Categories = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            Actors = actors.Select(a => new SelectListItem(a.ToString(), a.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string title, string description, DateTime releaseDate, CancellationToken cancellationToken)
        {
            var category = CategoryId <= 0 ? null : await _categoryService.GetAsync(CategoryId, cancellationToken);

            var movie = new Movie { Title = title, Description = description, ReleaseDate = releaseDate, CategoryId = CategoryId, Category = category };

            var sourceActors = await _actorService.GetAllAsync(new Domain.FilterPaginationDto(string.Empty), cancellationToken);
            movie.Actors = sourceActors.Where(a => ActorIds.Contains(a.Id)).ToList();

            await _movieService.CreateAsync(movie, cancellationToken);
            return Redirect("./MoviesIndex");
        }
    }
}
