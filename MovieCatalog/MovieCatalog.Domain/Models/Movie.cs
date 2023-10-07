namespace MovieCatalog.Domain.Models;

public sealed class Movie : Model
{
	public required string Title { get; set; }
	public string? Description { get; set; }
	public DateTime ReleaseDate { get; set; }
	public int? CategoryId { get; set; }
	public Category? Category { get; set; }
	public ICollection<Actor>? Actors { get; set; }

	public override bool IsMatch(string searchTerm)
	{
		return Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
			Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) is true ||
			Category?.IsMatch(searchTerm) is true ||
			Actors?.Any(actor => actor.IsMatch(searchTerm)) is true;
	}

	public override object? SortBy(string sortColumn)
	{
		return sortColumn switch
		{
			nameof(Title) => Title,
			nameof(ReleaseDate) => ReleaseDate,
			_ => Id
		};
	}

	public override string ToString()
	{
		return $"{Title} ({Description})";
	}
}
