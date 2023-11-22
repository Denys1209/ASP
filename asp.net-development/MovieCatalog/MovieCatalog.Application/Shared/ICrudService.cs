using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Shared;

public interface ICrudService<TModel> where TModel : Model
{
	Task<int> CreateAsync(TModel model, CancellationToken cancellationToken);
	Task UpdateAsync(TModel model, CancellationToken cancellationToken);
	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task<TModel?> GetAsync(int id, CancellationToken cancellationToken);
	Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken);
}
