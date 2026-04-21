using System.Text.Json.Serialization;
using AdobeMPClient.Models.Benefits;
using AdobeMPClient.Models.Customer;

namespace AdobeMPClient.Models.Transfers;

public class ResellerTransferResponse
{
    [JsonPropertyName("transferId")]
    public string TransferId { get; set; } = string.Empty;

    [JsonPropertyName("customerId")]
    public string CustomerId { get; set; } = string.Empty;

    [JsonPropertyName("resellerId")]
    public string ResellerId { get; set; } = string.Empty;

    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("approval")]
    public Approval Approval { get; set; } = new();

    [JsonPropertyName("discounts")]
    public IEnumerable<Discount> Discounts { get; set; } = [];

    [JsonPropertyName("benefits")]
    public IEnumerable<Benefit> Benefits { get; set; } = [];
}

public class Approval
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("expiry")]
    public DateTime Expiry { get; set; }
}