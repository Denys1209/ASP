namespace MovieCatalog.Domain;

public sealed record FilterPaginationDto(
	string SearchTerm,
	int PageNumber = 1,
	int PageSize = 50,
	string SortColumn = "Id",
	SortOrder SortOrder = SortOrder.Asc);
