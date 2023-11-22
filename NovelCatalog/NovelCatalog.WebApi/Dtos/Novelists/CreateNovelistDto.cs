using System.ComponentModel.DataAnnotations;

namespace NovelCatalog.WebApi.Dtos.Novelists;
public sealed class CreateNovelistDto
{

    [Required(ErrorMessage = "Firstname is required")]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "Lastname is required")]
    [StringLength(50, MinimumLength = 3)]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "Date of birth is required")]
    public DateOnly DateOfBirth { get; set; }
    public string ImageUrl { get; set; } = null!;
}
