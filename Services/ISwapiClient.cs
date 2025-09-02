using starwarsapp.Models;

namespace starwarsapp.Services
{
    public interface ISwapiClient
    {
        Task<PagedResponse<People>> GetPeopleAsync(int page = 1, string? search = null);
        Task<PagedResponse<Planet>> GetPlanetAsync(int page = 1, string? search = null);
        Task<PagedResponse<Species>> GetSpeciesAsync(int page = 1, string? search = null);
        Task<PagedResponse<Starship>> GetStarshipAsync(int page = 1, string? search = null);
        Task<PagedResponse<Vehicle>> GetVehicleAsync(int page = 1, string? search = null);

        Task<People?> GetPersonByIdAsync(int id);
        Task<Planet?> GetPlanetByIdAsync(int id);
        Task<Species?> GetSpeciesByIdAsync(int id);
        Task<Starship?> GetStarshipByIdAsync(int id);
        Task<Vehicle?> GetVehicleByIdAsync(int id);
    }
}
