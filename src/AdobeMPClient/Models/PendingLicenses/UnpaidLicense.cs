using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.PendingLicenses;

public class UnpaidLicense
{
    [JsonPropertyName("referenceId")]
    public string ReferenceId { get; set; } = string.Empty;

    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    [JsonPropertyName("expiryDate")]
    public DateTime ExpiryDate { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("baseOfferId")]
    public string BaseOfferId { get; set; } = string.Empty;
}