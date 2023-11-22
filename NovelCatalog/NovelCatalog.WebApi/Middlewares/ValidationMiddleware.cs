using NovelCatalog.WebApi.Filters.Validation;

namespace NovelCatalog.WebApi.Middlewares;

public class ValidationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var modelState = context.Features.Get<ValidationFeature>()?.ModelState;

        if (modelState is not null && !modelState.IsValid)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(modelState);
        }

        await next(context);
    }
}
