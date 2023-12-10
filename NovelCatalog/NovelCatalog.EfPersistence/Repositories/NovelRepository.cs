using Microsoft.EntityFrameworkCore;
using NovelCatalog.Application.Novel;
using NovelCatalog.Application.Shared;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;
using NovelCatalog.EfPersistence.Data;

namespace NovelCatalog.MemoryPersistense.Repositories;

public sealed class NovelRepository : CrudRepository<Novel>, INovelRepository
{
    public NovelRepository(NovelCatalogDbContext dbContext) : base(dbContext)
    {
    }

   
    public async Task<Novel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Novel>().Include(n => n.Categories).Include(n => n.Novelists).FirstAsync((n) => n.Id == id, cancellationToken);
    }

    public new async Task<PaginatedCollection<Novel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var query = DbContext.Set<Novel>().Include(m => m.Categories).Include(m => m.Novelists).AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
            query = Filter(query, dto.SearchTerm);
        var totalItems = await query.CountAsync(cancellationToken);

        var orderBy = string.IsNullOrWhiteSpace(dto.SortColumn) ? "Id" : dto.SortColumn;

        query = Sort(query, orderBy, dto.SortOrder == SortOrder.Asc);

        var models = await query.Skip(skip).Take(take).ToArrayAsync(cancellationToken);

        return new PaginatedCollection<Novel>(models, totalItems);
    }


    public async Task UpdateAsync(Novel model, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<Novel>().Include(n => n.Categories).Include(n => n.Novelists).FirstAsync((n) => n.Id == model.Id, cancellationToken);
        if (entity is null)
            return;

        Update(model, entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    protected override void Update(Novel model, Novel entity)
    {
		entity.Novelists = model.Novelists;
		entity.HowManyPages = model.HowManyPages;
		entity.Description = model.Description;
		entity.Title = model.Title;
		entity.ReleaseDate = model.ReleaseDate;
		entity.Categories = model.Categories;
		entity.Novelists = model.Novelists;
    }
    
    protected override IQueryable<Novel> Sort(IQueryable<Novel> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "title" => isAscending ? query.OrderBy(m => m.Title) : query.OrderByDescending(m => m.Title),
            "releasedate" => isAscending ? query.OrderBy(m => m.ReleaseDate) : query.OrderByDescending(m => m.ReleaseDateForDb),
            "howmanypages" => isAscending ? query.OrderBy(m => m.HowManyPages) : query.OrderByDescending(m => m.HowManyPages),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override IQueryable<Novel> Filter(IQueryable<Novel> query, string filter)
    {
        return query.Where(m => m.Title.Contains(filter) || (m.Description != null && m.Description.Contains(filter)));
    }

  
    public Task<IReadOnlyCollection<Novel>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Novel>> GetByNovelistAsync(int novelistId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
