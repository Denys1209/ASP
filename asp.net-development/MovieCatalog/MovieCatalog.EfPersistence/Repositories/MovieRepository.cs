using Microsoft.EntityFrameworkCore;
using MovieCatalog.Application.Movies;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;
using MovieCatalog.EfPersistence.Data;

namespace MovieCatalog.EfPersistence.Repositories;

public sealed class MovieRepository : CrudRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieCatalogDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Movie>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await DbContext.Movies
            .Where(m => m.CategoryId == categoryId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Movie>> GetByActorAsync(int actorId, CancellationToken cancellationToken)
    {
        return await DbContext.Movies.Include(m => m.Actors)
            .Where(m => m.Actors!.Any(a => a.Id == actorId))
            .ToArrayAsync(cancellationToken);
    }

    protected override IQueryable<Movie> Filter(IQueryable<Movie> query, string filter)
    {
        return query.Where(m => m.Title.Contains(filter) || (m.Description != null && m.Description.Contains(filter)));
    }

    protected override IQueryable<Movie> Sort(IQueryable<Movie> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "title" => isAscending ? query.OrderBy(m => m.Title) : query.OrderByDescending(m => m.Title),
            "releasedate" => isAscending ? query.OrderBy(m => m.ReleaseDate) : query.OrderByDescending(m => m.ReleaseDate),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(Movie model, Movie entity)
    {
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.ReleaseDate = model.ReleaseDate;
        entity.CategoryId = model.CategoryId;
    }
}