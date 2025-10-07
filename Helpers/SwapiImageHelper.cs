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

    }
}
