namespace Domain.Paging;

public record PageRequest
{
    public int PageSize { get; init; } = 20;
    public int Page { get; init; } = 1;
    public string? SortBy { get; init; }
    public string? Keyword { get; init; }
    public bool IsAscending { get; init; }
}