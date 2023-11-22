using Microsoft.AspNetCore.Mvc.Filters;
using NovelCatalog.WebApi.Filters.Validation;
namespace NovelCatalog.WebApi.Extensions;

public static class ValidationFilterExtensions
{
    public static FilterCollection AddValidationFilter(this FilterCollection filters)
    {
        filters.Add<ValidationFeatureFilter>();
        return filters;
    }
}
