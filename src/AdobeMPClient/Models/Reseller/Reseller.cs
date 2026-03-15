using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Reseller;

public sealed class Reseller
{
    [JsonPropertyName("distributorId")]
    public string DistributorId { get; init; } = string.Empty;
    [JsonPropertyName("externalReferenceId")]
    public string ExternalReferenceId { get; init; } = string.Empty;
    [JsonPropertyName("resellerId")]
    public string ResellerId { get; init; } = string.Empty;
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; init; }
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
    [JsonPropertyName("companyProfile")]
    public ResellerProfile ResellerProfile { get; init; } = new();
}
