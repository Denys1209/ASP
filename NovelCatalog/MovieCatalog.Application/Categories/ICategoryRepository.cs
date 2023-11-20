using NovelCatalog.Application.Shared;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.Application.Categories;

public interface ICategoryRepository : ICrudRepository<Category>
{
}
