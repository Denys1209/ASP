using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NovelCatalog.WebApi.Filters.Validation;

public record ValidationFeature(ModelStateDictionary ModelState);
