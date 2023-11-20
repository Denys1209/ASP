using NovelCatalog.Application.Shared;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.Application.Categories;

public sealed class CategoryService : CrudService<Category>, ICategoryService
{
	public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
	{
	}
}
