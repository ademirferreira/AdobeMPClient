using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.PriceList;

public sealed record Offer
{
    [JsonPropertyName("offerId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OfferId { get; init; }

    [JsonPropertyName("productFamily")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProductFamily { get; init; }

    [JsonPropertyName("productType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProductType { get; init; }

    [JsonPropertyName("productTypeDetail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProductTypeDetail { get; init; }

    [JsonPropertyName("additionalDetail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AdditionalDetail { get; init; }

    [JsonPropertyName("operatingSystem")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OperatingSystem { get; init; }

    [JsonPropertyName("language")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Language { get; init; }

    [JsonPropertyName("version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Version { get; init; }

    [JsonPropertyName("users")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Users { get; init; }

    [JsonPropertyName("metric")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Metric { get; init; }

    [JsonPropertyName("bridge")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Bridge { get; init; }

    [JsonPropertyName("upcEanCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UpcEanCode { get; init; }

    [JsonPropertyName("gtinCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GtinCode { get; init; }

    [JsonPropertyName("acdIndicator")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AcdIndicator { get; init; }

    [JsonPropertyName("acdEffectiveDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? AcdEffectiveDate { get; init; }

    [JsonPropertyName("acdDescription")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AcdDescription { get; init; }

    [JsonPropertyName("levelDetails")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LevelDetails { get; init; }

    [JsonPropertyName("firstOrderDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? FirstOrderDate { get; init; }

    [JsonPropertyName("lastOrderDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? LastOrderDate { get; init; }

    [JsonPropertyName("partnerPrice")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PartnerPrice { get; init; }

    [JsonPropertyName("estimatedStreetPrice")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EstimatedStreetPrice { get; init; }

    [JsonPropertyName("discountCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DiscountCode { get; init; }

    [JsonPropertyName("estimatedShipDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? EstimatedShipDate { get; init; }

    [JsonPropertyName("publicAnnounceDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? PublicAnnounceDate { get; init; }

    [JsonPropertyName("rmaRequestDeadline")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? RmaRequestDeadline { get; init; }

    [JsonPropertyName("pool")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Pool { get; init; }

    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Duration { get; init; }

}
