using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Transfers;

public sealed class ResellerTransferRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("approvalCode")]
    public string ApprovalCode { get; set; } = string.Empty;

    [JsonPropertyName("resellerId")]
    public string ResellerId { get; set; } = string.Empty;

    [JsonPropertyName("requestedBy")]
    public string RequestedBy { get; set; } = string.Empty;
}
