using MovieCatalog.Domain.Models;
using MovieCatalog.MVCView.Controllers;
using MovieCatalog.MVCView.ViewModels.Shared;

namespace MovieCatalog.MVCView.ViewModels;

public sealed class CategoriesViewModel : PaginatedFilteredViewModel
{
    public CategoriesViewModel() : base(typeof(Category), typeof(CategoriesController))
    {
    }

    public override ModalViewModel GetModal(Model model, string column)
    {
        const string header = "Movies";
        var category = (Category)model;
        return column switch
        {
            nameof(Category.Movies) => new CategoryMovieModalViewModel(
                category.Id,
                header,
                category.Movies ?? Enumerable.Empty<Movie>()),
            _ => throw new ArgumentException($"Column '{column}' has no modal.")
        };
    }

    public IEnumerable<ModalViewModel> GetModals()
    {
        const string header = "Movies";
        var categories = Models.Cast<Category>();
        foreach (var category in categories)
        {
            yield return new CategoryMovieModalViewModel(
                category.Id,
                header,
                category.Movies ?? Enumerable.Empty<Movie>());
        }
    }
}