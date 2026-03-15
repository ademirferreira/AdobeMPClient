using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Reseller;

public sealed class Resellers
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    public int OffSet { get; set; }
    public int Limimt { get; set; }

    [JsonPropertyName("accounts")]
    public IEnumerable<Account> Accounts { get; set; } = Enumerable.Empty<Account>();
}
