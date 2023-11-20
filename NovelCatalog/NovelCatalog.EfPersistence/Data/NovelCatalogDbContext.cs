using Microsoft.EntityFrameworkCore;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.EfPersistence.Data
{
    public class NovelCatalogDbContext : DbContext
    {
        public DbSet<Novelist> Novelists { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Novel> Novels { get; set; } = null!;

        public NovelCatalogDbContext(DbContextOptions<NovelCatalogDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Novelist>().Property(n => n.DateOfBirthForDb);
            modelBuilder.Entity<Novelist>().Ignore(n => n.DateOfBirth);

            modelBuilder.Entity<Novel>().Property(n => n.ReleaseDateForDb);
            modelBuilder.Entity<Novel>().Ignore(n => n.ReleaseDate);

        }

    }
}
