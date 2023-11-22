using Microsoft.AspNetCore.Mvc.Filters;
using MovieCatalog.WebApiView.Filters.Validation;

namespace MovieCatalog.WebApiView.Extensions;

public static class ValidationFilterExtensions
{
    public static FilterCollection AddValidationFilter(this FilterCollection filters)
    {
        filters.Add<ValidationFeatureFilter>();
        return filters;
    }
}
