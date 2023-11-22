using MovieCatalog.Application.Shared;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Movies;

public interface IMovieService : ICrudService<Movie>
{
	Task<IReadOnlyCollection<Movie>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken);
	Task<IReadOnlyCollection<Movie>> GetByActorAsync(int actorId, CancellationToken cancellationToken);
}
