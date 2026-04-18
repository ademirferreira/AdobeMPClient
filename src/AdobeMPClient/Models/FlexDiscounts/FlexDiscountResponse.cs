using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.FlexDiscounts;

public class FlexDiscountResponse
{
    [JsonPropertyName("limit")]
    public int Limit { get; init; }

    [JsonPropertyName("offset")]
    public int Offset { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; init; }

    [JsonPropertyName("flexDiscounts")]
    public List<FlexDiscount> FlexDiscounts { get; init; } = [];

}

public class FlexDiscount
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; init; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("discountLockEndDate")]
    public DateTime? DiscountLockEndDate { get; init; }

    [JsonPropertyName("qualification")]
    public FlexDiscountQualification? Qualification { get; init; }

    [JsonPropertyName("outcomes")]
    public List<FlexDiscountOutcome> Outcomes { get; init; } = [];
}
