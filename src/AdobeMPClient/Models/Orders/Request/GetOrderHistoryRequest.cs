namespace AdobeMPClient.Models.Orders.Request;

public sealed record GetOrderHistoryRequest
{
    public string? OrderType { get; init; }
    public string? ResellerId { get; init; }
    public string[]? Status { get; init; }
    public string? ReferenceOrderId { get; init; }
    public string? OfferId { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public int? Limit { get; init; }
    public int? OffSet { get; init; }
    public bool? FetchPrice { get; init; }
}
