using Microsoft.EntityFrameworkCore;
using MovieCatalog.Application.Categories;
using MovieCatalog.Domain.Models;
using MovieCatalog.EfPersistence.Data;

namespace MovieCatalog.EfPersistence.Repositories;

public sealed class CategoryRepository : CrudRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MovieCatalogDbContext dbContext) : base(dbContext)
    {
    }
    
    protected override IQueryable<Category> Filter(IQueryable<Category> query, string filter)
    {
        return query.Where(c => c.Name.Contains(filter));
    }

    protected override IQueryable<Category> Sort(IQueryable<Category> query, string orderBy, bool isAscending)
    {
        return orderBy.ToLower() switch
        {
            "name" => isAscending ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
            _ => isAscending ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id)
        };
    }

    protected override void Update(Category model, Category entity)
    {
        entity.Name = model.Name;
    }

    protected override IQueryable<Category> Include(IQueryable<Category> query)
    {
        return query.Include(c => c.Movies);
    }
}