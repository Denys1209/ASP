using NovelCatalog.Application.Shared;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.Application.Novelists;

public sealed class NovelistService : CrudService<Novelist>, INovelistService
{
	private readonly INovelistRepository _novelistsRepository;

	public NovelistService(INovelistRepository novelistsRepository) : base(novelistsRepository)
	{
		_novelistsRepository = novelistsRepository;
	}

	public async Task<IReadOnlyCollection<Novelist>> GetByNovelistIdAsync(int novelistId, CancellationToken cancellationToken)
	{
		return await _novelistsRepository.GetNovelistByNovelIdAsync(novelistId, cancellationToken);
	}
}
