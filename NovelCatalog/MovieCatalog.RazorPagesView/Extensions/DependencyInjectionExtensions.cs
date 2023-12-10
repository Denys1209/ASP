using Microsoft.EntityFrameworkCore;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Novel;
using NovelCatalog.Application.Novelists;
using NovelCatalog.EfPersistence.Data;
using NovelCatalog.MemoryPersistense.Repositories;

namespace NovelCatalog.MVCView.Extensions;
public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<NovelCatalogDbContext>(options =>
            options.UseSqlServer(connectionString));


        services.AddTransient<INovelistRepository, NovelistRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<INovelRepository, NovelRepository>();

        services.AddTransient<INovelistService, NovelistService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<INovelService, NovelService>();
    }

}
