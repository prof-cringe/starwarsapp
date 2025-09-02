using System.Text.Json.Serialization;

namespace starwarsapp.Models
{
    public class Starship
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

        [JsonPropertyName("model")] public string? Model { get; set; }

        [JsonPropertyName("manufacturer")] public string? Manufacturer { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
    }
}
