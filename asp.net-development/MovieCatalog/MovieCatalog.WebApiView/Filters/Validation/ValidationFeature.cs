using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieCatalog.WebApiView.Filters.Validation;

public record ValidationFeature(ModelStateDictionary ModelState);
