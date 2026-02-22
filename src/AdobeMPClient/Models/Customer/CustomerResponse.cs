using AdobeMPClient.Models.Benefits;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Customer;

public class CustomerResponse
{
    [JsonPropertyName("externalReferenceId")]
    public string? ExternalReferenceId { get; set; }
    [JsonPropertyName("customerId")]
    public string CustomerId { get; set; } = string.Empty;
    [JsonPropertyName("resellerId")]
    public string ResellerId { get; set; } = string.Empty;
    [JsonPropertyName("globalSalesEnabled")]
    public bool GlobalSalesEnabled { get; set; }
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = [];
    [JsonPropertyName("companyProfile")]
    public CompanyProfile CompanyProfile { get; set; } = new();
    [JsonPropertyName("discounts")]
    public List<Discount>? Discounts { get; set; }

    [JsonPropertyName("benefits")]
    public List<Benefit>? Benefits { get; set; }

    [JsonPropertyName("cotermDate")]
    public string? CotermDate { get; set; }
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}
