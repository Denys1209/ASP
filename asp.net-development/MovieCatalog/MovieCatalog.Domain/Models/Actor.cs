namespace MovieCatalog.Domain.Models;

public sealed class Actor : Model
{
	private DateTimeOffset _dateOfBirth;
	
	public required string FirstName { get; set; }
	public string? LastName { get; set; }
	public DateTimeOffset DateOfBirth 
	{ 
		get => _dateOfBirth;
		set => _dateOfBirth = value.Date.ToUniversalTime();
	}
	public ICollection<Movie>? Movies { get; set; }

	public override bool IsMatch(string searchTerm)
	{
		return FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
			LastName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) is true ||
			Movies?.Any(movie => movie.IsMatch(searchTerm)) is true;
	}

	public override object? SortBy(string sortColumn)
	{
		return sortColumn switch
		{
			nameof(FirstName) => FirstName,
			nameof(LastName) => LastName,
			nameof(DateOfBirth) => DateOfBirth,
			_ => Id
		};
	}

	public override string ToString()
	{
		return $"{FirstName} {LastName}";
	}
}
