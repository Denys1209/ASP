namespace MovieCatalog.MVCView.Dtos.Shared;

public class PaginatedFilteredDto
{
    protected const string DefaultSortColumn = "Id";
    protected const int FirstPage = 1;
    protected const int DefaultPageSize = 10;
    
    public string SearchTerm { get; set; } = string.Empty;
    public string Sorting { get; set; } = string.Empty;
    public string SortColumn { get; set; } = DefaultSortColumn;
    public string SortDirection { get; set; } = string.Empty;
    public int Page { get; set; } = FirstPage;
    public int PageSize { get; set; } = DefaultPageSize;
    public bool IsSortChanged { get; set; } = false;
}