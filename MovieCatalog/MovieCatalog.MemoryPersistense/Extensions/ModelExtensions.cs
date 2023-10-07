using MovieCatalog.Domain;
using MovieCatalog.Domain.Models;

namespace MovieCatalog.MemoryPersistense.Extensions;

public static class ModelExtensions
{
	public static IEnumerable<TModel> Filter<TModel>(this IEnumerable<TModel> source, string searchTerm) where TModel : Model
	{
		return string.IsNullOrWhiteSpace(searchTerm) ? source : source.Where(model => model.IsMatch(searchTerm));
	}

	public static IEnumerable<TModel> SortBy<TModel>(
		this IEnumerable<TModel> source, 
		string sortColumn, 
		SortOrder sortOrder) where TModel : Model
	{
		var columns = typeof(TModel).GetProperties().Select(p => p.Name).ToArray();

		sortColumn = string.IsNullOrWhiteSpace(sortColumn) || !columns.Any(c => c.Equals(sortColumn)) ? nameof(Model.Id) : sortColumn;

		return sortOrder switch
		{
			SortOrder.Asc => source.OrderBy(model => model.SortBy(sortColumn)),
			SortOrder.Desc => source.OrderByDescending(model => model.SortBy(sortColumn)),
			_ => source
		};
	}
}
