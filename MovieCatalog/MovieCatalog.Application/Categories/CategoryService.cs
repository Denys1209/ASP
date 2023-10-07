using MovieCatalog.Application.Shared;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Categories;

public sealed class CategoryService : CrudService<Category>, ICategoryService
{
	public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
	{
	}
}
