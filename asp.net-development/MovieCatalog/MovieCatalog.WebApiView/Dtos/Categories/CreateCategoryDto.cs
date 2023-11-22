using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.WebApiView.Dtos.Categories;

public sealed class CreateCategoryDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;
}
