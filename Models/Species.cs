using System.Text.Json.Serialization;

namespace starwarsapp.Models
{
    public class Species
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

        [JsonPropertyName("classification")] public string? Classification { get; set; }

        [JsonPropertyName("language")] public string? Language { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
    }
}
