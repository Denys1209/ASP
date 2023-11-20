using NovelCatalog.Application.Novel;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.MemoryPersistense.Repositories;

public sealed class NovelRepository : CrudRepository<Novel>, INovelRepository
{
	

    public Task<IReadOnlyCollection<Novel>> GetByNovelistAsync(int actorId, CancellationToken cancellationToken)
	{
		var novels = Models.Values
			.Where(movie => movie.Novelists?.Any(actor => actor.Id == actorId) is true)
			.ToArray();
		return Task.FromResult<IReadOnlyCollection<Novel>>(novels);
	}

	public Task<IReadOnlyCollection<Novel>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken)
	{
		var novels = Models.Values
			.Where(movie => movie.Categories?.Any(category => category.Id == categoryId) is true)
			.ToArray();
		return Task.FromResult<IReadOnlyCollection<Novel>>(novels);
	}

    public override Task DeleteAsync(int id, CancellationToken cancellationToken)
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
   	public override Task<int> AddAsync(Novel model, CancellationToken cancellationToken)
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
    public override Task UpdateAsync(Novel model, CancellationToken cancellationToken)
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
