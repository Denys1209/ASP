using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.MVCView.ViewModels.Shared;

public abstract class PaginatedFilteredViewModel
{
    private readonly Type _modelType;
    private readonly ICollection<Model> _models = new List<Model>();
    private readonly PropertyInfo[] _modelProperties;

    public string ControllerName { get; }
    public string ActionName { get; }
    
    public IEnumerable<Model> Models => _models.ToList();
    public int Total { get; set; }

    public string SortColumn { get; set; } = "Id";
    public string SortDirection { get; set; } = "Descending";
    public string Sorting => $"{SortColumn}_{SortDirection}";

    public int CurrentPage { get; set; }
    public int PageSize { get; set; } = 50;
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(Total / (decimal)PageSize);

    public string SearchTerm { get; set; } = string.Empty;
    public IDictionary<int, IDictionary<string, ValueType>> ColumnValues { get; } = new Dictionary<int, IDictionary<string, ValueType>>();

    public IEnumerable<string> Columns => _modelProperties.Select(p => p.Name);
    
    protected virtual IEnumerable<string> IgnoreColumns => new[] { nameof(Model.Id) };

    protected PaginatedFilteredViewModel(Type modelType, Type controllerType)
    {
        if (!typeof(Model).IsAssignableFrom(modelType))
            throw new ArgumentException($"Type '{modelType}' is not a model.");
        
        if (!typeof(Controller).IsAssignableFrom(controllerType))
            throw new ArgumentException($"Type '{controllerType}' is not a controller.");
        
        _modelType = modelType;
        ControllerName = controllerType.Name.Replace("Controller", string.Empty);
        ActionName = "Index";
        
        _modelProperties = _modelType.GetProperties()
            .Where(p => p.CanWrite && p.CanRead && !IgnoreColumns.Contains(p.Name))
            .ToArray();
    }

    public void AddModel(Model model)
    {
        if (model.GetType() != _modelType)
            throw new ArgumentException("Data collection should include only models of the same type.");
        
        _models.Add(model);
        ColumnValues.Add(model.Id, _modelProperties.ToDictionary(
            p => p.Name,
            p => GetColumnValue(model, p)));
    }
    
    public void AddModels(IEnumerable<Model> models)
    {
        foreach (var model in models)
            AddModel(model);
    }
    
    public IDictionary<string, string> GetRouteValues(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sorting = null,
        bool isSortChanged = false,
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
            { "isSortChanged", isSortChanged.ToString() }
        };
    }
    
    public static (string, string) GetSortColumnAndDirection(string sorting, string sortColumn, bool isSortChanged)
    {
        if (string.IsNullOrWhiteSpace(sorting) || string.IsNullOrWhiteSpace(sortColumn))
            return ("Id", "Descending");

        if (!isSortChanged)
        {
            var parts = sorting.Split('_');
            return (parts[0], parts[1]);
        }

        if (!sorting.Contains(sortColumn))
            return (sortColumn, "Ascending");
        
        if (sorting.EndsWith("Ascending"))
            return (sortColumn, "Descending");
        
        if (sorting.EndsWith("Descending"))
            return ("Id", "Descending");
        
        // Never happened but...
        return ("Id", "Descending");
    }
    
    protected virtual ValueType GetColumnValue(Model model, PropertyInfo property)
    {
        var value = property.GetValue(model);
        
        // getting value from IEnumerable<Data> property
        var genericArguments = property.PropertyType.GetGenericArguments();
        Type? enumerableType = null;
        
        if (genericArguments.Length == 1)
            enumerableType = typeof(IEnumerable<>).MakeGenericType(genericArguments);
        
        if (enumerableType is not null && enumerableType.IsAssignableFrom(property.PropertyType))
        {
            var enumerable = value as IEnumerable<Model> ?? Enumerable.Empty<Model>();
            return new ValueType(enumerable, "select");
        }

        return new ValueType(value?.ToString() ?? string.Empty, "text");
    }
    
    public abstract ModalViewModel GetModal(Model model, string column);

    public sealed record ValueType(object Value, string Type);
}
