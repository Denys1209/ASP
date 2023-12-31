﻿using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;

namespace NovelCatalog.EfPersistence.Extensions;

public static class ModelExtensions
{
	public static IQueryable<TModel> Filter<TModel>(this IQueryable<TModel> source, string searchTerm) where TModel : Model
	{
		return string.IsNullOrWhiteSpace(searchTerm) ? source : source.Where(model => model.IsMatch(searchTerm));
	}

	public static IQueryable<TModel> SortBy<TModel>(
		this IQueryable<TModel> source, 
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
