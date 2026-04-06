using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Reseller;

public sealed class Resellers
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    [JsonPropertyName("offset")]
    public int OffSet { get; set; }
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("accounts")]
    public IEnumerable<Account> Accounts { get; set; } = Enumerable.Empty<Account>();
}
