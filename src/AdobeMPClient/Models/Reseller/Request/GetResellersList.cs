namespace AdobeMPClient.Models.Reseller.Request;

public sealed record GetResellersList
{
    public string[]? Status { get; init; }
    public int? Limit { get; init; }
    public int? OffSet { get; init; }
    public string? CompanyName { get; init; }
    public string? SortBy { get; init; }
    public string? OrderBy { get; init; }
}
