using NovelCatalog.Application.Shared;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.Application.Novelists;

public interface INovelistRepository : ICrudRepository<Novelist>
{
    Task<IReadOnlyCollection<Novelist>> GetNovelistByNovelIdAsync(int novelistId, CancellationToken cancellationToken);
}
