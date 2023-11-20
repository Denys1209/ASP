using NovelCatalog.Application.Shared;
using NovelCatalog.Domain;

namespace NovelCatalog.Application.Novel;

public interface INovelRepository : ICrudRepository<NovelCatalog.Domain.Models.Novel>
{
	Task<IReadOnlyCollection<NovelCatalog.Domain.Models.Novel>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken);
	Task<IReadOnlyCollection<NovelCatalog.Domain.Models.Novel>> GetByNovelistAsync(int novelistId, CancellationToken cancellationToken);
}
