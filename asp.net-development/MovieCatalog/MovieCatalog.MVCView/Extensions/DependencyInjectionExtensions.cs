using Microsoft.EntityFrameworkCore;
using MovieCatalog.Application.Actors;
using MovieCatalog.Application.Categories;
using MovieCatalog.Application.Movies;
using MovieCatalog.EfPersistence.Data;
using MovieCatalog.EfPersistence.Repositories;

namespace MovieCatalog.MVCView.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<MovieCatalogDbContext>(options => 
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly("MovieCatalog.EfPersistence")));
        
        services.AddTransient<IActorRepository, ActorsRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IMovieRepository, MovieRepository>();
        
        services.AddTransient<IActorService, ActorService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IMovieService, MovieService>();
    }
}