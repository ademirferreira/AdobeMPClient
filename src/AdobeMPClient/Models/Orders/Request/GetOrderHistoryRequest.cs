using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.Models.Orders.Request;

public sealed record GetOrderHistoryRequest
{
    [FromQuery(Name ="order-type")]
    public string? OrderType { get; init; }

    [FromQuery(Name ="reseller-id")]
    public string? ResellerId { get; init; }

    [FromQuery(Name ="status")]
    public string[]? Status { get; init; }

    [FromQuery(Name ="reference-order-id")]
    public string? ReferenceOrderId { get; init; }

    [FromQuery(Name ="offer-id")]
    public string? OfferId { get; init; }

    [FromQuery(Name ="start-date")]
    public string? StartDate { get; init; }

    [FromQuery(Name ="end-date")]
    public string? EndDate { get; init; }

    [FromQuery(Name ="limit")]
    public int? Limit { get; init; }

    [FromQuery(Name = "offinit")]
    public int? OffSet { get; init; }

    [FromQuery(Name = "fetch-price")]
    public bool? FetchPrice { get; init; }
}
