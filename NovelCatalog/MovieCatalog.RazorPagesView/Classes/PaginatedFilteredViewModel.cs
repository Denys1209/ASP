using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.RazorPagesView.Classes
{
    public abstract class PaginatedFilteredViewModel : PageModel
    {
        private readonly Type _modelType;
        private readonly ICollection<Model> _models = new List<Model>();

        public string ActionName { get; }

        public IEnumerable<Model> Models => _models.ToList();
        public int Total { get; set; }

        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "Descending";
        public string Sorting => $"{SortColumn}_{SortDirection}";

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(Total / (decimal)PageSize);

        public string SearchTerm { get; set; } = string.Empty;

        public abstract IEnumerable<string> Columns { get; }
        public abstract IEnumerable<SelectListItem> SortColumns { get; }

        public IEnumerable<SelectListItem> SortDirections { get; } = new[]
        {
        new SelectListItem("Ascending", "Ascending"),
        new SelectListItem("Descending", "Descending", true)
    };

        protected PaginatedFilteredViewModel(Type modelType)
        {
            if (!typeof(Model).IsAssignableFrom(modelType))
                throw new ArgumentException($"Type '{modelType}' is not a model.");


            _modelType = modelType;
            ActionName = "Index";
        }

        public void AddModel(Model model)
        {
            if (model.GetType() != _modelType)
                throw new ArgumentException("Model collection should include only models of the same type.");

            _models.Add(model);
        }

        public void AddModels(IEnumerable<Model> models)
        {
            foreach (var model in models)
                AddModel(model);
        }

        public IDictionary<string, string> ToDictionaryParameters(
            string? searchTerm = null,
            string? sortColumn = null,
            string? sorting = null,
            int? page = null,
            int? pageSize = null)
        {
            return new Dictionary<string, string>
        {
            { "searchTerm", searchTerm ?? SearchTerm },
            { "sorting", sorting ?? Sorting },
            { "sortColumn", sortColumn ?? SortColumn },
            { "pageSize", pageSize?.ToString() ?? PageSize.ToString() },
            { "page", page?.ToString() ?? CurrentPage.ToString() },
        };
        }

        public abstract string GetColumnValue(Model model, string column);

        public static (string, string) GetSortColumnAndDirection(string sorting, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sorting) || string.IsNullOrWhiteSpace(sortColumn))
                return ("Id", "Descending");

            if (!sorting.Contains(sortColumn))
                return (sortColumn, "Ascending");

            if (sorting.EndsWith("Ascending"))
                return (sortColumn, "Descending");

            if (sorting.EndsWith("Descending"))
                return ("Id", "Descending");

            // Never happened but...
            return ("Id", "Descending");
        }
    }
}
