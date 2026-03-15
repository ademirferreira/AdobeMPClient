using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.Models.Reseller.Request;

public sealed record GetResellersList
{
    [FromQuery(Name = "status")]
    public string[]? Status { get; init; }

    [FromQuery(Name = "limit")]
    public int? Limit { get; init; }

    [FromQuery(Name = "offset")]
    public int? OffSet { get; init; }

    [FromQuery(Name = "company-name")]
    public string? CompanyName { get; init; }

    [FromQuery(Name = "sort-by")]
    public string? SortBy { get; init; }
    [FromQuery(Name = "order-by")]
    public string? OrderBy { get; init; }
}
