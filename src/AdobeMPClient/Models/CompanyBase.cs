using System.Text.Json.Serialization;

namespace AdobeMPClient.Models;

public class CompanyBase
{
    [JsonPropertyName("companyName")]
    public string CompanyName { get; init; } = string.Empty;

}
