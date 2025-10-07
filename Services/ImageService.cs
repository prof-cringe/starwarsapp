
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Nodes;
using System.Web;

namespace starwarsapp.Services
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        private const string DataBankBase = "https://starwars-databank-server.vercel.app/api/v1";

        public ImageService(HttpClient httpClient, IMemoryCache cache)
        {
            _cache = cache;
            _httpClient = httpClient;
        }

        public async Task<string?> GetImageUrlAsync(string type, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var key = $"img::{type}::{name}".ToLowerInvariant();

            if(_cache.TryGetValue(key, out string? cached)) return cached;

            var route = type.ToLowerInvariant() switch
            {
                "people" => "characters",
                "planets" => "locations",
                "species" => "species",
                "starships" => "vehicles",
                "vehicles" => "vehicles",
                _ => "characters"
            };

            var encoded = HttpUtility.UrlEncode(name);
            var byName = $"{DataBankBase}/{route}/name/{encoded}";

            try
            {
                using var response = await _httpClient.GetAsync(byName);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<dynamic>();
                    string? img = null;
                    
                    if (json is JsonArray arr && arr.Count > 0)
                    {
                        img = (string?)arr[0]?["image"];
                    }
                    else if(json is JsonObject obj)
                    {
                        img = (string?)obj["image"];
                    }

                    if (!string.IsNullOrWhiteSpace(img))
                    {
                        _cache.Set(key, img, TimeSpan.FromHours(12));
                        return img;
                    }
                }
            }
            catch (Exception)
            {
                //Silencioso
            }
            //Fallback
            _cache.Set(key, null as string, TimeSpan.FromMinutes(30));
            return null;
        }
    }
}
