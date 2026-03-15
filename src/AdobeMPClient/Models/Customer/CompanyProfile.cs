using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Customer;

public class CompanyProfile : CompanyBase
{
    [JsonPropertyName("preferredLanguage")]
    public string PreferredLanguage { get; set; } = string.Empty;
    [JsonPropertyName("marketSegment")]
    public string MarketSegment { get; set; } = string.Empty;
    [JsonPropertyName("marketSubSegments")]
    public IEnumerable<string>? MarketSubSegments { get; set; }
    [JsonPropertyName("address")]
    public Address Address { get; set; } = new();
    [JsonPropertyName("contacts")]
    public List<Contact> Contacts { get; set; } = [];

}
