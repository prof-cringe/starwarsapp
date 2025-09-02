using System.Text.RegularExpressions;

namespace starwarsapp.Helpers
{
    public class SwapiImageHelper
    {
        public static int ExtractId(string url)
        {
            var m = Regex.Match(url, @"/(?<id>\\d+)/?$");
            if (!m.Success) return 0;
            return int.TryParse(m.Groups["id"].Value, out var id) ? id : 0;
        }

        public static string GetImageUrl(string type, int id)
        {
            var segment = type switch
            {
                "people" => "characters",
                "planets" => "planets",
                "species" => "species",
                "starships" => "starships",
                "vehicles" => "vehicles",
                _ => "characters"
            };

            return $"https://starwars-visualguide.com/assets/img/{segment}/{id}";
        }
    }
}
