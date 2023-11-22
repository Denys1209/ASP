using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieCatalog.WebApiView.Filters.Validation;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.HttpContext.Response.WriteAsJsonAsync(context.ModelState);
            return;
        }

        await base.OnActionExecutionAsync(context, next);
    }
}
