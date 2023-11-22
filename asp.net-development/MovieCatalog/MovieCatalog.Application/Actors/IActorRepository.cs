using MovieCatalog.Application.Shared;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Actors;

public interface IActorRepository : ICrudRepository<Actor>
{
	Task<IReadOnlyCollection<Actor>> GetByMovieAsync(int movieId, CancellationToken cancellationToken);
}
