using MovieCatalog.Application.Shared;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Actors;

public interface IActorService : ICrudService<Actor>
{
	Task<IReadOnlyCollection<Actor>> GetByMovieAsync(int movieId, CancellationToken cancellationToken);
}
