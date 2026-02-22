using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.PendingLicenses;

public class PendingLicense
{
    [JsonPropertyName("customerId")]
    public string CustomerId { get; set; } = string.Empty;

    [JsonPropertyName("resellerId")]
    public string ResellerId { get; set; } = string.Empty;

    [JsonPropertyName("licenseCount")]
    public int LicenseCount { get; set; }

    [JsonPropertyName("unpaidLicenses")]
    public IEnumerable<UnpaidLicense> UnpaidLicenses { get; set; } = Enumerable.Empty<UnpaidLicense>();
}
