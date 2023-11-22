using MovieCatalog.Domain.Models;
using MovieCatalog.MVCView.ViewModels.Shared;

namespace MovieCatalog.MVCView.ViewModels;

public class CategoryMovieModalViewModel : ModalViewModel
{
    private const string PartialName = "~/Views/Categories/MoviesInfo.cshtml";

    private readonly int _categoryId;
    
    public override string Id => $"view-movies-details-{_categoryId}";
    public override string HeaderId => $"view-movies-details-{_categoryId}-header";
    public override string ControllerName => "Categories";
    public override string ActionName => "Index";
    
    public CategoryMovieModalViewModel(
        int categoryId,
        string header, 
        IEnumerable<Movie> movies) 
        : base(header, PartialName, movies)
    {
        _categoryId = categoryId;
    }
}