using AdobeMPClient.Models.Benefits;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Transfers;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Memberships;

public sealed record PreviewOffer
{
    [JsonPropertyName("offerId")]
    public int TotalCount { get; init; }
    [JsonPropertyName("items")]
    public IEnumerable<ItemTransfer> Items { get; init; } = [];
    [JsonPropertyName("benefits")]
    public IEnumerable<Benefit> Benefits { get; init; } = [];
    [JsonPropertyName("discounts")]
    public IEnumerable<Discount> Discounts { get; init; } = [];
    [JsonPropertyName("renewalStatus")]
    public string RenewalStatus { get; init; } = string.Empty;
}
