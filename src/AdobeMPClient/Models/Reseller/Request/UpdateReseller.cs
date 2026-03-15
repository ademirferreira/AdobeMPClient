using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Reseller.Request;

public class UpdateReseller
{
    [JsonPropertyName("externalReferenceId")]
    public string ExternalReferenceId { get; init; } = string.Empty;
    [JsonPropertyName("companyProfile")]
    public ResellerProfile CompanyProfile { get; init; } = new();
}
