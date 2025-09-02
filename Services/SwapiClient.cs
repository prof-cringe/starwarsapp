namespace starwarsapp.Services
{
    public class SwapiClient : ISwapiClient
    {
        private readonly HttpClient _http;
        public SwapiClient(HttpClient http)
        {
            _http = http;
        }

        private async Task<T?> GetAsync<T>(string path)
        {
            return await _http.GetFromJsonAsync<T>(path);
        }
    }
}
