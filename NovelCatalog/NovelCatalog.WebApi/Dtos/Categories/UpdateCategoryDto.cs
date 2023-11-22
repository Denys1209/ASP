using System.ComponentModel.DataAnnotations;

namespace NovelCatalog.WebApi.Dtos.Categories;

public sealed class UpdateCategoryDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;
}
