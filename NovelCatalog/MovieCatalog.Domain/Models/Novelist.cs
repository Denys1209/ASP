using System.ComponentModel.DataAnnotations.Schema;

namespace NovelCatalog.Domain.Models;

public sealed class Novelist : Model
{
	public required string FirstName { get; set; }
	public string? LastName { get; set; }
    [NotMapped]
    public DateOnly DateOfBirth { get; set; }
	public string? ImageUrl { get; set; }
	public ICollection<Novel> Novels { get; set; }
    public DateTime DateOfBirthForDb
    {
        get => DateOfBirth.ToDateTime(TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay));
        set => DateOfBirth = DateOnly.FromDateTime(value);
    }

    public Novelist() 
	{
		Novels = new List<Novel>();
	}
	public Novelist(string firstName, string lastName, DateOnly dateOfBirht, string image = "https://www.amacad.org/sites/default/files/person/headshots/tom-hanks.jpg")
	{
		DateOfBirth = dateOfBirht;
		FirstName = firstName;
		LastName = lastName;
		Novels = new List<Novel>();
		this.ImageUrl = image;
	}


	public override bool IsMatch(string searchTerm)
	{
		return FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
			LastName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) is true ||
			Novels?.Any(movie => movie.IsMatch(searchTerm)) is true;
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
		return $"{this.FirstName} {this.LastName}";
	}

	public string ToStringWithNovels() 
	{
		return $"Novelist with id {this.Id} (Name: {this.FirstName} {this.LastName} DateOfBirth: {this.DateOfBirth} {this.Novels.Aggregate("Movies: ", (current, next) => current+$" {next}" )})";
	}
}
