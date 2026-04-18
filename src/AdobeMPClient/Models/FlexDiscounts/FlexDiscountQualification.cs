using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.FlexDiscounts;

public class FlexDiscountQualification
{
    [JsonPropertyName("baseOfferIds")]
    public List<string> BaseOfferIds { get; init; } = [];
}
