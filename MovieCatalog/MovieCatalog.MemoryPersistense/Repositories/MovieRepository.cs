﻿using MovieCatalog.Application.Movies;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.MemoryPersistense.Repositories;

public sealed class MovieRepository : CrudRepository<Movie>, IMovieRepository
{
	public Task<IReadOnlyCollection<Movie>> GetByActorAsync(int actorId, CancellationToken cancellationToken)
	{
		var movies = Models.Values
			.Where(movie => movie.Actors?.Any(actor => actor.Id == actorId) is true)
			.ToArray();
		return Task.FromResult<IReadOnlyCollection<Movie>>(movies);
	}

	public Task<IReadOnlyCollection<Movie>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken)
	{
		var movies = Models.Values
			.Where(movie => movie.CategoryId.HasValue && movie.CategoryId.Value == categoryId)
			.ToArray();
		return Task.FromResult<IReadOnlyCollection<Movie>>(movies);
	}
}