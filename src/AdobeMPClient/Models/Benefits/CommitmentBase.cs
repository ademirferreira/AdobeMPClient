using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Benefits;

public class CommitmentBase
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("minimumQuantities")]
    public List<MinimumQuantity> MinimumQuantities { get; set; } = [];
}

public class Commitment : CommitmentBase
{
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }
}

public class CommitmentRequest : CommitmentBase { }

public class RecommitmentRequest : CommitmentBase { }