using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Customer;

public class Address
{
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;
    [JsonPropertyName("addressLine1")]
    public string AddressLine1 { get; set; } = string.Empty;
    [JsonPropertyName("addressLine2")]
    public string AddressLine2 { get; set; } = string.Empty;
    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = string.Empty;
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;
}
