using AdobeMPClient.Models.Customer;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Reseller;

public class ResellerProfile
{
    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; } = string.Empty;

    [JsonPropertyName("preferredLanguage")]
    public string PreferredLanguage { get; set; } = string.Empty;

    [JsonPropertyName("marketSegments")]
    public List<string> MarketSegments { get; set; } = [];

    [JsonPropertyName("address")]
    public Address Address { get; set; } = new();

    [JsonPropertyName("contacts")]
    public List<Contact> Contacts { get; set; } = [];
}
