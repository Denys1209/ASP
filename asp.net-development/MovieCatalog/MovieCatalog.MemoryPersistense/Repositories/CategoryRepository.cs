using MovieCatalog.Application.Categories;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.MemoryPersistense.Repositories;

public sealed class CategoryRepository : CrudRepository<Category>, ICategoryRepository
{
    protected override void UpdateModel(Category oldModel, Category newModel)
    {
        oldModel.Name = newModel.Name;
    }
}
