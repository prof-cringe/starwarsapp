using starwarsapp.Models;

namespace starwarsapp.Services
{
    public class SwapiClient : ISwapiClient
    {
        private readonly HttpClient _http;
        public SwapiClient(HttpClient http)
        {
            _http = http;
        }

        private static string BuildPath(string resource, int page, string? search)
        {
            return string.IsNullOrWhiteSpace(search)
                ? $"{resource}/?page={page}"
                : $"{resource}/?search={Uri.EscapeDataString(search)}&page={page}";
        }

        public Task<PagedResponse<People>> GetPeopleAsync(int page = 1, string? search = null)
        {
            return GetAsync<PagedResponse<People>>(BuildPath("people", page, search))!;
        }

        public Task<People?> GetPersonByIdAsync(int id)
        {
            return GetAsync<People>($"people/{id}/")!;
        }

        public Task<PagedResponse<Planet>> GetPlanetAsync(int page = 1, string? search = null)
        {
            return GetAsync<PagedResponse<Planet>>(BuildPath("planets", page, search))!;
        }

        public Task<Planet?> GetPlanetByIdAsync(int id)
        {
            return GetAsync<Planet>($"planets/{id}/")!;
        }

        public Task<PagedResponse<Species>> GetSpeciesAsync(int page = 1, string? search = null)
        {
            return GetAsync<PagedResponse<Species>>(BuildPath("species", page, search))!;
        }

        public Task<Species?> GetSpeciesByIdAsync(int id)
        {
            return GetAsync<Species>($"species/{id}/")!;
        }

        public Task<PagedResponse<Starship>> GetStarshipAsync(int page = 1, string? search = null)
        {
            return GetAsync<PagedResponse<Starship>>(BuildPath("starships", page, search))!;
        }

        public Task<Starship?> GetStarshipByIdAsync(int id)
        {
            return GetAsync<Starship>($"starships/{id}/")!;
        }

        public Task<PagedResponse<Vehicle>> GetVehicleAsync(int page = 1, string? search = null)
        {
            return GetAsync<PagedResponse<Vehicle>>(BuildPath("vehicles", page, search))!;
        }

        public Task<Vehicle?> GetVehicleByIdAsync(int id)
        {
            return GetAsync<Vehicle>($"vehicles/{id}/")!;
        }

        private async Task<T?> GetAsync<T>(string path)
        {
            return await _http.GetFromJsonAsync<T>(path);
        }
    }
}
