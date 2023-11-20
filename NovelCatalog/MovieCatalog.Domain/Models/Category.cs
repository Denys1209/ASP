using NovelCatalog.Domain.Models;

namespace NovelCatalog.Domain.Models;

public sealed class Category : Model
{
	public required string Name { get; set; }
	public ICollection<Novel> Novels { get; set; }

	public Category(string name)
	{
		this.Name = name;
		Novels = new List<Novel>();
	}

    public Category()
    {
    }

    public override bool IsMatch(string searchTerm)
	{
		return Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
			Novels?.Any(movie => movie.IsMatch(searchTerm)) is true;
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
		return $"{Name}";
	}
}
