using Microsoft.EntityFrameworkCore;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Novel;
using NovelCatalog.Application.Novelists;
using NovelCatalog.EfPersistence.Data;
using NovelCatalog.MemoryPersistense.Repositories;

namespace NovelCatalog.WebApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<NovelCatalogDbContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly("NovelCatalog.EfPersistence")));

            services.AddTransient<INovelRepository, NovelRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<INovelistRepository, NovelistRepository>();

            services.AddTransient<INovelService, NovelService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<INovelistService, NovelistService>();
        }

    }
}
