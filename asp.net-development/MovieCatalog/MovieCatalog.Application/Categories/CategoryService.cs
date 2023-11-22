using MovieCatalog.Application.Shared;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.Application.Categories;

public sealed class CategoryService : CrudService<Category>, ICategoryService
{
	public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
	{
	}

    public override Task<int> CreateAsync(Category model, CancellationToken cancellationToken)
    {
        Validate(model);
        return base.CreateAsync(model, cancellationToken);
    }

    public override Task UpdateAsync(Category model, CancellationToken cancellationToken)
    {
        Validate(model);
        return base.UpdateAsync(model, cancellationToken);
    }

    protected override IReadOnlyCollection<Category> RemoveCircularReferences(IReadOnlyCollection<Category> categories)
    {
        return categories.Select(c =>
        {
            if (c.Movies is null || !c.Movies.Any())
                return c;

            foreach (var movie in c.Movies)
                movie.Category = null;

            return c;
        }).ToList();
    }

    private static void Validate(Category model)
    {
        if (string.IsNullOrEmpty(model.Name))
            throw new ArgumentException("Category name cannot be null or empty.");
    }
}
