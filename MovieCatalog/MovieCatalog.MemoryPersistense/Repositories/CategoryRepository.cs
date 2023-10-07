using MovieCatalog.Application.Categories;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.MemoryPersistense.Repositories;

public sealed class CategoryRepository : CrudRepository<Category>, ICategoryRepository
{
}
