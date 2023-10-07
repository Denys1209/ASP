using MovieCatalog.Application.Shared;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Categories;

public interface ICategoryRepository : ICrudRepository<Category>
{
}
