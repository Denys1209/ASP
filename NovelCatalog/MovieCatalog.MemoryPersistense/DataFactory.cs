using NovelCatalog.Domain.Models;
using System.Net.Http.Headers;

namespace NovelCatalog.MemoryPersistense;

internal sealed class DataFactory
{
	// Singleton implementation
	private static DataFactory? instanse;
	public static DataFactory Instanse => instanse ??= new DataFactory(true);

	private readonly IReadOnlyCollection<Novelist> _actors;
	private readonly IReadOnlyCollection<Category> _categories;
	private readonly IReadOnlyCollection<Novel> _movies;

	public DataFactory(bool seedData = false)
	{
		_actors = new List<Novelist>();
		_categories = new List<Category>();
		_movies = new List<Novel>();

		if (!seedData)
			return;

		(_actors, _categories, _movies) = CreateData();
	}

	public IReadOnlyCollection<TModel> GetModels<TModel>()
	{
		if (typeof(TModel) == typeof(Novelist))
			return (IReadOnlyCollection<TModel>)_actors;
		if (typeof(TModel) == typeof(Category))
			return (IReadOnlyCollection<TModel>)_categories;
		if (typeof(TModel) == typeof(Novel))
			return (IReadOnlyCollection<TModel>)_movies;
		return Array.Empty<TModel>();
	}

	private static (IReadOnlyCollection<Novelist> Actors, IReadOnlyCollection<Category> Categories, IReadOnlyCollection<Novel> Movies) CreateData()
	{
		var actors = new List<Novelist>
		{
			new() { Id = 1, FirstName = "Tom", LastName = "Hanks", DateOfBirth = new DateOnly(1956, 7, 9), ImageUrl = "https://www.shutterstock.com/image-photo/young-female-novelist-baggy-sweater-260nw-1864435981.jpg" },
			new() { Id = 2, FirstName = "Tom", LastName = "Hanks", DateOfBirth = new DateOnly(1956, 7, 9), ImageUrl = "https://www.shutterstock.com/image-photo/young-female-novelist-baggy-sweater-260nw-1864435981.jpg" },
			new() { Id = 3, FirstName = "Tom", LastName = "Hanks", DateOfBirth = new DateOnly(1956, 7, 9), ImageUrl = "https://www.shutterstock.com/image-photo/young-female-novelist-baggy-sweater-260nw-1864435981.jpg" },
		};

		var categories = new List<Category>
		{
			new() { Id = 1, Name = "Animation" },
			new() { Id = 2, Name = "test" },
				};

		var movies = new List<Novel>
		{
			new() { Id = 1, Title = "Toy Story", Description = "Description 1", ReleaseDate = new DateOnly(1995, 11, 22), Categories =new List<Category> {categories[0], categories[1] }, HowManyPages = 100, },
			new() { Id = 2, Title = "Toy Story 2", Description = "Description 2", ReleaseDate = new DateOnly(1999, 11, 24), Categories =new List<Category> {categories[0], categories[1] }, HowManyPages = 100,  },
			new() { Id = 3, Title = "Toy Story 3", Description = "Description 3", ReleaseDate = new DateOnly(2010, 6, 18), Categories =new List<Category> {categories[0], categories[1] }, HowManyPages = 100, },
		};

	
		return (actors, categories, movies);
	}

	private static IReadOnlyCollection<Novelist> CreateActors()
	{
		return new List<Novelist>
		{

		};
	}

	private static IReadOnlyCollection<Category> CreateCategories()
	{
		return new List<Category>
		{

		};
	}

	private static IReadOnlyCollection<Novel> CreateMovies()
	{
		return new List<Novel>
		{

		};
	}
}
