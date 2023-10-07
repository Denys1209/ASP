namespace MovieCatalog.Domain.Models;

public abstract class Model
{
	public int Id { get; set; }

	public abstract bool IsMatch(string searchTerm);
	public abstract object? SortBy(string sortColumn);
}
