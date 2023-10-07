using MovieCatalog.Application.Shared;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Actors;

public sealed class ActorService : CrudService<Actor>, IActorService
{
	private readonly IActorRepository _actorRepository;

	public ActorService(IActorRepository actorRepository) : base(actorRepository)
	{
		_actorRepository = actorRepository;
	}

	public async Task<IReadOnlyCollection<Actor>> GetByMovieAsync(int movieId, CancellationToken cancellationToken)
	{
		return await _actorRepository.GetByMovieAsync(movieId, cancellationToken);
	}
}
