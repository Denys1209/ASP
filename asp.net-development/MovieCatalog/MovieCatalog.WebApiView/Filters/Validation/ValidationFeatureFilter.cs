using Microsoft.AspNetCore.Mvc.Filters;
using MovieCatalog.WebApiView.Middlewares;

namespace MovieCatalog.WebApiView.Filters.Validation;

public class ValidationFeatureFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Features.Set(new ValidationFeature(context.ModelState));
        await next();
    }
}
