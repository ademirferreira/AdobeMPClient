using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.Models.Orders.Request;

public class GetOrderHistoryRequest
{
    [FromQuery(Name ="order-type")]
    public string? OrderType { get; set; }

    [FromQuery(Name ="reseller-id")]
    public string? ResellerId { get; set; }

    [FromQuery(Name ="status")]
    public string? Status { get; set; }

    [FromQuery(Name ="reference-order-id")]
    public string? ReferenceOrderId { get; set; }

    [FromQuery(Name ="offer-id")]
    public string? OfferId { get; set; }

    [FromQuery(Name ="start-date")]
    public string? StartDate { get; set; }

    [FromQuery(Name ="end-date")]
    public string? EndDate { get; set; }

    [FromQuery(Name ="limit")]
    public int? Limit { get; set; }

    [FromQuery(Name = "offset")]
    public int? Offset { get; set; }

    [FromQuery(Name = "fetch-price")]
    public bool? FetchPrice { get; set; }
}
