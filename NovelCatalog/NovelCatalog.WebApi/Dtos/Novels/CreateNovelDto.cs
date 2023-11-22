using NovelCatalog.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace NovelCatalog.WebApi.Dtos.Novels
{
    public class CreateNovelDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, MinimumLength = 3)]
        public required string Title { get; set; } = null!;
        [Required(ErrorMessage = "Description is required")]
        [StringLength(254, MinimumLength = 3)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "HowManyPages is required")]
        public uint HowManyPages { get; set; }
        [Required(ErrorMessage = "ReleaseDate is required")]
        public DateOnly ReleaseDate { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<Novelist> Novelists { get; set; }
    }
}
