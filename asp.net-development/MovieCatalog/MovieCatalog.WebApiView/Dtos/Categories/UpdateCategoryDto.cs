using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.WebApiView.Dtos.Categories;

public sealed class UpdateCategoryDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;
}
