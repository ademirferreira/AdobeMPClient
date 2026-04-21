using AdobeMPClient.Models.Transfers;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Memberships;

public sealed record TransferResponse
{
    [JsonPropertyName("transferId")]
    public string TransferId { get; init; } = string.Empty;
    [JsonPropertyName("customerId")]
    public string CustomerId { get; init; } = string.Empty;
    [JsonPropertyName("membershipId")]
    public string MembershipId { get; init; } = string.Empty;
    [JsonPropertyName("resellerId")]
    public string ResellerId { get; init; } = string.Empty;
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; init; }
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
    [JsonPropertyName("lineItems")]
    public IEnumerable<ItemTransfer> Items { get; init; } = [];
}
