using Microsoft.EntityFrameworkCore;
using NovelCatalog.Application.Shared;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;
using NovelCatalog.EfPersistence.Data;

namespace NovelCatalog.MemoryPersistense.Repositories;

public abstract class CrudRepository<TModel> : ICrudRepository<TModel> where TModel : Model
{
	protected readonly NovelCatalogDbContext DbContext;
    
    protected CrudRepository(NovelCatalogDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public async Task<int> AddAsync(TModel model, CancellationToken cancellationToken)
    {
        await DbContext.Set<TModel>().AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FindAsync(model.Id);
        if (entity is null)
            return;

        Update(model, entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FindAsync(id);
        if (entity is null)
            return;

        DbContext.Set<TModel>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().FindAsync(id);
    }

    public async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var query = DbContext.Set<TModel>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
            query = Filter(query, dto.SearchTerm);
        var totalItems = await query.CountAsync(cancellationToken);

        var orderBy = string.IsNullOrWhiteSpace(dto.SortColumn) ? "Id" : dto.SortColumn;

        query = Sort(query, orderBy, dto.SortOrder == SortOrder.Asc);

        var models = await query.Skip(skip).Take(take).ToArrayAsync(cancellationToken);

        return new PaginatedCollection<TModel>(models, totalItems);
    }

    public async Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken)  
	{
        return await DbContext.Set<TModel>().Where(m => ids.Contains(m.Id)).ToArrayAsync(cancellationToken);

	}

    protected abstract void Update(TModel model, TModel entity);

    protected abstract IQueryable<TModel> Filter(IQueryable<TModel> query, string filter);

    protected abstract IQueryable<TModel> Sort(IQueryable<TModel> query, string orderBy, bool isAscending);


}
