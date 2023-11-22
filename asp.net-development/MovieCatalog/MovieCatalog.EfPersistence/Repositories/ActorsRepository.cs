using Microsoft.EntityFrameworkCore;
using MovieCatalog.Application.Actors;
using MovieCatalog.Domain.Models;
using MovieCatalog.EfPersistence.Data;

namespace MovieCatalog.EfPersistence.Repositories;

public sealed class ActorsRepository : CrudRepository<Actor>, IActorRepository
{
    public ActorsRepository(MovieCatalogDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Actor>> GetByMovieAsync(int movieId, CancellationToken cancellationToken)
    {
        return await DbContext.Actors.Include(a => a.Movies)
            .Where(a => a.Movies!.Any(m => m.Id == movieId))
            .ToArrayAsync(cancellationToken);
    }

    protected override IQueryable<Actor> Filter(IQueryable<Actor> query, string filter)
    {
        return query.Where(a => a.FirstName.Contains(filter) || (a.LastName != null && a.LastName.Contains(filter)));
    }

    protected override IQueryable<Actor> Sort(IQueryable<Actor> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "firstname" => isAscending ? query.OrderBy(a => a.FirstName) : query.OrderByDescending(a => a.FirstName),
            "lastname" => isAscending ? query.OrderBy(a => a.LastName) : query.OrderByDescending(a => a.LastName),
            "dateofbirth" => isAscending ? query.OrderBy(a => a.DateOfBirth) : query.OrderByDescending(a => a.DateOfBirth),
            _ => isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id)
        };
    }

    protected override void Update(Actor model, Actor entity)
    {
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
        entity.DateOfBirth = model.DateOfBirth;
    }
}