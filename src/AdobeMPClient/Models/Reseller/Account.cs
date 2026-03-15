using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Reseller;

public sealed class Account
{
    [JsonPropertyName("externalReferenceId")]
    public string ExternalReferenceId { get; init; } = string.Empty;
    [JsonPropertyName("resellerId")]
    public string ResellerId { get; init; } = string.Empty;
    [JsonPropertyName("distributorId")]
    public string DistributorId { get; init; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; init; }
    [JsonPropertyName("companyProfile")]
    public CompanyBase CompanyProfile { get; init; } = new();

}

