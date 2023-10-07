using MovieCatalog.Application.Actors;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.MemoryPersistense.Repositories;

public sealed class ActorRepository : CrudRepository<Actor>, IActorRepository
{
	public Task<IReadOnlyCollection<Actor>> GetByMovieAsync(int movieId, CancellationToken cancellationToken)
	{
		var movies = Models.Values
			.Where(actor => actor.Movies?.Any(movie => movie.Id == movieId) is true)
			.ToArray();
		return Task.FromResult<IReadOnlyCollection<Actor>>(movies);
	}
}
