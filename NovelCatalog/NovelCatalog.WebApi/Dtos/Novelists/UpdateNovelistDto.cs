using System.ComponentModel.DataAnnotations;

namespace NovelCatalog.WebApi.Dtos.Novelists;
public sealed class UpdateNovelistDto
{
    public int Id { get; set; }

    [Required()]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; set; } = null!;
    [Required()]
    [StringLength(50, MinimumLength = 3)]
    public string LastName { get; set; } = null!;
    [Required()]
    public DateOnly DateOfBirth { get; set; }
    [Required()]
    public string ImageUrl { get; set; } = null!;
}