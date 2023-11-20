using NovelCatalog.Application.Categories;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.MemoryPersistense.Repositories;

public sealed class CategoryRepository : CrudRepository<Category>, ICategoryRepository
{
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
