using AdobeMPClient.Models.Benefits;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Customer.Request;

public class CreateCustomer
{
    [JsonPropertyName("resellerId")]
    public string ResellerId { get; set; } = string.Empty;
    [JsonPropertyName("externalReferenceId")]
    public string ExternalReferenceId { get; set; } = string.Empty;
    [JsonPropertyName("companyProfile")]
    public CompanyProfile CompanyProfile { get; set; } = new CompanyProfile();
    [JsonPropertyName("cotermDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CotermDate { get; set; } = null;
    [JsonPropertyName("benefits")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Benefit>? Benefits { get; set; }
}
