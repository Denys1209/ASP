using NovelCatalog.Application.Shared;
using NovelCatalog.Domain;

namespace NovelCatalog.Application.Novel;

public interface INovelService : ICrudService<NovelCatalog.Domain.Models.Novel>
{
	Task<IReadOnlyCollection<NovelCatalog.Domain.Models.Novel>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken);
	Task<IReadOnlyCollection<NovelCatalog.Domain.Models.Novel>> GetByNovelistIdAsync(int novelistId, CancellationToken cancellationToken);
}
