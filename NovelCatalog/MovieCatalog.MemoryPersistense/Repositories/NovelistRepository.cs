using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.MemoryPersistense.Repositories;

public sealed class NovelistRepository : CrudRepository<Novelist>, INovelistRepository
{
	public Task<IReadOnlyCollection<Novelist>> GetNovelistByNovelIdAsync(int movieId, CancellationToken cancellationToken)
	{
		var movies = Models.Values
			.Where(actor => actor.Novels?.Any(movie => movie.Id == movieId) is true)
			.ToArray();
		return Task.FromResult<IReadOnlyCollection<Novelist>>(movies);
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

}
