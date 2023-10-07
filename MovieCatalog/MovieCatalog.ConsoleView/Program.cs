using MovieCatalog.Application.Actors;
using MovieCatalog.Application.Categories;
using MovieCatalog.Application.Movies;
using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;
using MovieCatalog.MemoryPersistense.Repositories;

var movieService = new MovieService(new MovieRepository());
var categoryService = new CategoryService(new CategoryRepository());
var actorService = new ActorService(new ActorRepository());

var movies = await movieService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
foreach (var movie in movies)
{
	Console.WriteLine(movie);
}

Console.WriteLine();
var newMovie = new Movie { Title = "New Movie", ReleaseDate = new DateTime(2021, 2, 2), Description = "New Movie Description" };
await movieService.CreateAsync(newMovie, CancellationToken.None);
movies = await movieService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
foreach (var movie in movies)
{
	Console.WriteLine(movie);
}


Console.WriteLine();
var categories = categoryService.GetAllAsync(new FilterPaginationDto("y", 1, 1, "Name", SortOrder.Desc), CancellationToken.None).Result;
foreach (var category in categories)
{
	Console.WriteLine(category);
}
