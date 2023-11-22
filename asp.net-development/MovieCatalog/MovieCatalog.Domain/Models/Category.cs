namespace MovieCatalog.Domain.Models;

public sealed class Category : Model
{
	public required string Name { get; set; }
	public ICollection<Movie>? Movies { get; set; }

	public override bool IsMatch(string searchTerm)
	{
		return Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
			Movies?.Any(movie => movie.IsMatch(searchTerm)) is true;
	}

	public override object? SortBy(string sortColumn)
	{
		return sortColumn switch
		{
			nameof(Name) => Name,
			_ => Id
		};
	}

	public override string ToString()
	{
		return Name;
	}
}
