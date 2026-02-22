using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Subscriptions;

public class AutoRenewal
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("renewalQuantity")]
    public int RenewalQuantity { get; set; }

    [JsonPropertyName("flexDiscountCodes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? FlexDiscountCodes { get; set; }
}
