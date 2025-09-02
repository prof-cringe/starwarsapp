using System.Text.Json.Serialization;

namespace starwarsapp.Models
{
    public class People
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

        [JsonPropertyName("gender")] public string? Gender { get; set; }

        [JsonPropertyName("birth_year")] public string? BirthYear { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
    }
}
