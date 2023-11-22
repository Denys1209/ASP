using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Shared;

public abstract class CrudService<TModel> : ICrudService<TModel> where TModel : Model
{
	private readonly ICrudRepository<TModel> _repository;

	public CrudService(ICrudRepository<TModel> repository)
	{
		_repository = repository;
	}

	public virtual async Task<int> CreateAsync(TModel model, CancellationToken cancellationToken)
	{
		return await _repository.AddAsync(model, cancellationToken);
	}

	public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		await _repository.DeleteAsync(id, cancellationToken);
	}

	public virtual async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
	{
		var collection = await _repository.GetAllAsync(dto, cancellationToken);
		return new PaginatedCollection<TModel>(RemoveCircularReferences(collection.Models), collection.Total);
	}

	public virtual async Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
	{
		return await _repository.GetAsync(id, cancellationToken);
	}

	public virtual async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
	{
		await _repository.UpdateAsync(model, cancellationToken);
	}

	protected abstract IReadOnlyCollection<TModel> RemoveCircularReferences(IReadOnlyCollection<TModel> models);
}
