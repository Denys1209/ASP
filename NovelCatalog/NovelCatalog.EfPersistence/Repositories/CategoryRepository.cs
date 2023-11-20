using NovelCatalog.Application.Categories;
using NovelCatalog.Domain.Models;
using NovelCatalog.EfPersistence.Data;

namespace NovelCatalog.MemoryPersistense.Repositories;

public sealed class CategoryRepository : CrudRepository<Category>, ICategoryRepository
{
    public CategoryRepository(NovelCatalogDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Category> Sort(IQueryable<Category> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "name" => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override IQueryable<Category> Filter(IQueryable<Category> query, string filter)
    {
        return query.Where(m => m.Name.Contains(filter));
    }
    protected override void Update(Category model, Category entity)
    {
        entity.Name = model.Name;
        entity.Novels = model.Novels;
    }
}
