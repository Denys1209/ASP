using NovelCatalog.Application.Shared;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.Application.Novel;

public sealed class NovelService : CrudService<NovelCatalog.Domain.Models.Novel>, INovelService
{
	private readonly INovelRepository _novelRepository;

	public NovelService(INovelRepository novelRepository) : base(novelRepository)
	{
		_novelRepository = novelRepository;
	}

	public async Task<IReadOnlyCollection<NovelCatalog.Domain.Models.Novel>> GetByNovelistIdAsync(int novelistId, CancellationToken cancellationToken)
	{
		return await _novelRepository.GetByNovelistAsync(novelistId, cancellationToken);
	}

	public async Task<IReadOnlyCollection<NovelCatalog.Domain.Models.Novel>> GetByCategoryAsync(int categoryId, FilterPaginationDto dto, CancellationToken cancellationToken)
	{
		return await _novelRepository.GetByCategoryAsync(categoryId, dto, cancellationToken);
	}
}
