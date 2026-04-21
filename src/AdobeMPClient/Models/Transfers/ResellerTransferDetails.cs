using AdobeMPClient.Models.Benefits;
using AdobeMPClient.Models.Customer;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Transfers;

public sealed record ResellerTransferDetails
{
    [JsonPropertyName("transferId")]
    public string TransferId { get; init; } = string.Empty;

    [JsonPropertyName("resellerId")]
    public string ResellerId { get; init; } = string.Empty;

    [JsonPropertyName("customerId")]
    public string CustomerId { get; init; } = string.Empty;

    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; init; }

    [JsonPropertyName("lineItems")]
    public IEnumerable<ItemTransfer> Items { get; init; } = [];

    [JsonPropertyName("benefits")]
    public IEnumerable<Benefit> Benefits { get; init; } = [];

    [JsonPropertyName("discounts")]
    public IEnumerable<Discount> Discounts { get; init; } = [];
}

public sealed record ItemTransfer
{
    [JsonPropertyName("lineItemNumber")]
    public int LineItemNumber { get; init; }

    [JsonPropertyName("offerId")]
    public string OfferId { get; init; } = string.Empty;

    [JsonPropertyName("subscriptionId")]
    public string SubscriptionId { get; init; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }

    [JsonPropertyName("renewalDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime RenewalDate { get; init; }

    [JsonPropertyName("currencyCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string CurrencyCode { get; init; } = string.Empty;
}