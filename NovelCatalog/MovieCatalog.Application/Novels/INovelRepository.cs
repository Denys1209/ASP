using NovelCatalog.Application.Shared;
using NovelCatalog.Domain;

namespace NovelCatalog.Application.Novel;

public interface INovelRepository : ICrudRepository<Domain.Models.Novel>
{
	Task<IReadOnlyCollection<Domain.Models.Novel>> GetByCategoryAsync(
		int categoryId, 
		FilterPaginationDto dto, 
		CancellationToken cancellationToken);

	Task<IReadOnlyCollection<Domain.Models.Novel>> GetByNovelistAsync(
		int novelistId, 
		CancellationToken cancellationToken);
}
