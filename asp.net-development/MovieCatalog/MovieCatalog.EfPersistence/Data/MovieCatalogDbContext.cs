using Microsoft.EntityFrameworkCore;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.EfPersistence.Data;

public class MovieCatalogDbContext : DbContext
{
    public DbSet<Actor> Actors { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;
    
    public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options) : base(options)
    {
    }
}