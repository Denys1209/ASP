using NovelCatalog.Application.Shared;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;
using NovelCatalog.MemoryPersistense.Extensions;

namespace NovelCatalog.MemoryPersistense.Repositories;

public abstract class CrudRepository<TModel> : ICrudRepository<TModel> where TModel : Model
{
	protected static readonly IDictionary<int, TModel> Models = DataFactory.Instanse.GetModels<TModel>().ToDictionary(m => m.Id);
	protected static int LastId = Models.Max(x => x.Key);

	public virtual Task<int> AddAsync(TModel model, CancellationToken cancellationToken)
	{
		LastId++;
		try
		{
			model.Id = LastId;
			Models.Add(LastId, model);
			return Task.FromResult(LastId);
		}
		catch (Exception)
		{
			LastId--;
			return Task.FromResult(-1);
		}
	}

	public virtual Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		try
		{
			Models.Remove(id);
			return Task.CompletedTask;
		}
		catch (Exception ex)
		{
			return Task.FromException(ex);
		}
	}

	public virtual Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
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

	public virtual Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken)  
	{
		List<TModel?> models = new List<TModel?>();
		foreach (var id in ids) 
		{
			models.Add(Models[id]);
		}
		return Task.FromResult<IReadOnlyCollection<TModel?>>(models);

	}

	public virtual Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
	{
        return Task.FromResult<TModel?>(Models[id]);
	}

	public virtual Task UpdateAsync(TModel model, CancellationToken cancellationToken)
	{
		try
		{
			Models[model.Id] = model;
			return Task.CompletedTask;
		}
		catch (Exception ex)
		{
			return Task.FromException(ex);
		}

	}
}
