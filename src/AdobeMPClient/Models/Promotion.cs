using System.Text.Json.Serialization;

namespace AdobeMPClient.Models;

public class Promotion
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("result")]
    public string? Result { get; set; }
}
