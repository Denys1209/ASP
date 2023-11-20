using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain.Models;
using NovelCatalog.EfPersistence.Data;

namespace NovelCatalog.MemoryPersistense.Repositories;

public sealed class NovelistRepository : CrudRepository<Novelist>, INovelistRepository
{
    public NovelistRepository(NovelCatalogDbContext dbContext) : base(dbContext)
    {
    }

    public Task<IReadOnlyCollection<Novelist>> GetNovelistByNovelIdAsync(int novelistId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override IQueryable<Novelist> Sort(IQueryable<Novelist> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "firstname" => isAscending ? query.OrderBy(m => m.FirstName) : query.OrderByDescending(m => m.FirstName),
            "dateofbirth" => isAscending ? query.OrderBy(m => m.DateOfBirthForDb) : query.OrderByDescending(m => m.DateOfBirthForDb),
            "lastname" => isAscending ? query.OrderBy(m => m.LastName) : query.OrderByDescending(m => m.LastName),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override IQueryable<Novelist> Filter(IQueryable<Novelist> query, string filter)
    {
        return query.Where(m => m.LastName.Contains(filter));
    }


    protected override void Update(Novelist model, Novelist entity)
    {
        entity.DateOfBirth = model.DateOfBirth;
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
        entity.ImageUrl = model.ImageUrl;
        entity.Novels = model.Novels;
    }
   }
