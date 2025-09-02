using System.Text.Json.Serialization;

namespace starwarsapp.Models
{
    public class Planet
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

        [JsonPropertyName("climate")] public string? Climate { get; set; }

        [JsonPropertyName("population")] public string? Population { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
    }
}
