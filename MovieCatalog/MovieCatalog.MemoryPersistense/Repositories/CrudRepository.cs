using MovieCatalog.Application.Shared;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;
using MovieCatalog.MemoryPersistense.Extensions;

namespace MovieCatalog.MemoryPersistense.Repositories;

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

	public virtual Task<IReadOnlyCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
	{
		var skip = (dto.PageNumber - 1) * dto.PageSize;
		var take = dto.PageSize;

		var models = Models.Values
			.Filter(dto.SearchTerm)
			.SortBy(dto.SortColumn, dto.SortOrder)
			.Skip(skip)
			.Take(take)
			.ToArray();

		return Task.FromResult<IReadOnlyCollection<TModel>>(models);
	}

	public virtual Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public virtual Task UpdateAsync(TModel model, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
