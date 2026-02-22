using AdobeMPClient.Models.Benefits;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Customer.Request;

public class UpdateCustomer
{
    [JsonPropertyName("companyProfile")]
    public CompanyProfile CompanyProfile { get; set; } = new();

    [JsonPropertyName("benefits")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Benefit>? Benefits { get; set; }

    [JsonPropertyName("cotermDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CotermDate { get; set; }
}