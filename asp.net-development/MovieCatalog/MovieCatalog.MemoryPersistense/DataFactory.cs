using MovieCatalog.Domain.Models;

namespace MovieCatalog.MemoryPersistense;

internal sealed class DataFactory
{
	// Singleton implementation
	private static DataFactory? instanse;
	public static DataFactory Instanse => instanse ??= new DataFactory(true);

	private readonly IReadOnlyCollection<Actor> _actors;
	private readonly IReadOnlyCollection<Category> _categories;
	private readonly IReadOnlyCollection<Movie> _movies;

	public DataFactory(bool seedData = false)
	{
		_actors = new List<Actor>();
		_categories = new List<Category>();
		_movies = new List<Movie>();

		if (!seedData)
			return;

		(_actors, _categories, _movies) = CreateData();
	}

	public IReadOnlyCollection<TModel> GetModels<TModel>()
	{
		if (typeof(TModel) == typeof(Actor))
			return (IReadOnlyCollection<TModel>)_actors;
		if (typeof(TModel) == typeof(Category))
			return (IReadOnlyCollection<TModel>)_categories;
		if (typeof(TModel) == typeof(Movie))
			return (IReadOnlyCollection<TModel>)_movies;
		return Array.Empty<TModel>();
	}

	private static (IReadOnlyCollection<Actor> Actors, IReadOnlyCollection<Category> Categories, IReadOnlyCollection<Movie> Movies) CreateData()
	{
		var actors = new List<Actor>
		{
			new() { Id = 1, FirstName = "Tom", LastName = "Hanks", DateOfBirth = new DateTime(1956, 7, 9) },
			new() { Id = 2, FirstName = "Tim", LastName = "Allen", DateOfBirth = new DateTime(1953, 6, 13) },
			new() { Id = 3, FirstName = "Annie", LastName = "Potts", DateOfBirth = new DateTime(1952, 10, 28) },
		};

		var categories = new List<Category>
		{
			new() { Id = 1, Name = "Animation" },
			new() { Id = 2, Name = "Comedy" },
			new() { Id = 3, Name = "Family" },
		};

		var movies = new List<Movie>
		{
			new() { Id = 1, Title = "Toy Story", Description = "Description 1", ReleaseDate = new DateTime(1995, 11, 22), CategoryId = 1, Category = categories[0], Actors = new List<Actor> { actors[0], actors[1], actors[2] } },
			new() { Id = 2, Title = "Toy Story 2", Description = "Description 2", ReleaseDate = new DateTime(1999, 11, 24), CategoryId = 2, Category = categories[1], Actors = new List<Actor> { actors[0], actors[1] } },
			new() { Id = 3, Title = "Toy Story 3", Description = "Description 3", ReleaseDate = new DateTime(2010, 6, 18), CategoryId = 1, Category = categories[0] },
		};

		//actors[0].Movies = new List<Movie> { movies[0], movies[1] };
		//actors[1].Movies = new List<Movie> { movies[0], movies[1] };
		//actors[2].Movies = new List<Movie> { movies[0] };

		return (actors, categories, movies);
	}

	private static IReadOnlyCollection<Actor> CreateActors()
	{
		return new List<Actor>
		{

		};
	}

	private static IReadOnlyCollection<Category> CreateCategories()
	{
		return new List<Category>
		{

		};
	}

	private static IReadOnlyCollection<Movie> CreateMovies()
	{
		return new List<Movie>
		{

		};
	}
}
