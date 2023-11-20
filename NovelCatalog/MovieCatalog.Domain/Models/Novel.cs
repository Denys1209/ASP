using System.ComponentModel.DataAnnotations.Schema;

namespace NovelCatalog.Domain.Models;

public sealed class Novel : Model
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public uint HowManyPages { get; set; }
    [NotMapped]
    public DateOnly ReleaseDate { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Novelist> Novelists { get; set; }
    
    public DateTime ReleaseDateForDb
    {
        get => ReleaseDate.ToDateTime(TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay));
        set => ReleaseDate = DateOnly.FromDateTime(value);
    }

    public Novel() 
    {
        Categories = new List<Category>();
        Novelists = new List<Novelist>();
    }
 
    public Novel(string title, string description, DateOnly releaseDate, uint howManyPages)
    {
        Title = title;
        Description = description;
        ReleaseDate = releaseDate;
        Categories = new List<Category>();
        Novelists = new List<Novelist>();
        HowManyPages = howManyPages;
    }

    public override bool IsMatch(string searchTerm)
    {
        return Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) is true ||
            IsMathCategory(searchTerm) is true ||
            Novelists?.Any(actor => actor.IsMatch(searchTerm)) is true;
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
        return $"Nove with id {Id} (Title:{Title} Description:({Description}))";
    }

    public string ToStringWithCategoryAndActors() 
    {
        return $"Nove with id {Id} (Title:{Title} Description:({Description} {Novelists.Aggregate("Novelists:(", (current, next) => current+" "+next.ToString())}) {Categories.Aggregate("Categories:", (current, next) => current+" "+next)})))";
    }

    private bool IsMathCategory(string searchTerm)
    {
        foreach (var category in Categories)
        {
            if (category.IsMatch(searchTerm))
            {
                return true;
            }
        }

        return false;
    }
}
