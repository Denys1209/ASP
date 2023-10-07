using MovieCatalog.Application.Shared;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Movies;

public sealed class MovieService : CrudService<Movie>, IMovieService
{
	private readonly IMovieRepository _movieRepository;

	public MovieService(IMovieRepository movieRepository) : base(movieRepository)
	{
		_movieRepository = movieRepository;
	}

	public async Task<IReadOnlyCollection<Movie>> GetByActorAsync(int actorId, CancellationToken cancellationToken)
	{
		return await _movieRepository.GetByActorAsync(actorId, cancellationToken);
	}

	public async Task<IReadOnlyCollection<Movie>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken)
	{
		return await _movieRepository.GetByCategoryAsync(categoryId, dto, cancellationToken);
	}
}
