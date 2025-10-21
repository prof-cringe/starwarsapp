using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using starwarsapp.Helpers;
using starwarsapp.Models;
using starwarsapp.Services;

namespace starwarsapp.Pages.Search
{

    public class IndexModel : PageModel
    {
        private readonly ISwapiClient _api;
        private readonly IImageService _images;

        public IndexModel(ISwapiClient api, IImageService images)
        {
            _api = api;
            _images = images;
        }

        [BindProperty(SupportsGet = true)] public string? Q { get; set; }
        [BindProperty(SupportsGet = true)] public string Type { get; set; } = "all";

        public record Card(
            string Type,
            int id,
            string Title,
            string Subtitle,
            string Img
        );

        public List<Card> Results { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(Q)) return;

            async Task addPeople()
            {
                var r = await _api.GetPeopleAsync(1, Q);
                foreach (var p in r.Results)
                {
                    var id = SwapiImageHelper.ExtractId(p.Url);
                    var img = await _images.GetImageUrlAsync("people", p.Name) ?? "/img/placeholder.png";
                    Results.Add(new("people", id, p.Name, $"{p.BirthYear} - {p.Gender}", img));
                }
            }

            async Task addPlanets()
            {
                var r = await _api.GetPlanetAsync(1, Q);
                foreach (var p in r.Results)
                {
                    var id = SwapiImageHelper.ExtractId(p.Url);
                    var img = await _images.GetImageUrlAsync("planets", p.Name) ?? "/img/placeholder.png";
                    Results.Add(new("planets", id, p.Name, $"{p.Climate} - {p.Population}", img));
                }
            }

            async Task addSpecies()
            {
                var r = await _api.GetSpeciesAsync(1, Q);
                foreach (var p in r.Results)
                {
                    var id = SwapiImageHelper.ExtractId(p.Url);
                    var img = await _images.GetImageUrlAsync("species", p.Name) ?? "/img/placeholder.png";
                    Results.Add(new("species", id, p.Name, $"{p.Classification} - {p.Language}", img));
                }
            }

            async Task addStarships()
            {
                var r = await _api.GetStarshipAsync(1, Q);
                foreach (var p in r.Results)
                {
                    var id = SwapiImageHelper.ExtractId(p.Url);
                    var img = await _images.GetImageUrlAsync("starships", p.Name) ?? "/img/placeholder.png";
                    Results.Add(new("starships", id, p.Name, $"{p.Model} - {p.Manufacturer}", img));
                }
            }

            async Task addVehicles()
            {
                var r = await _api.GetVehicleAsync(1, Q);
                foreach (var p in r.Results)
                {
                    var id = SwapiImageHelper.ExtractId(p.Url);
                    var img = await _images.GetImageUrlAsync("vehicles", p.Name) ?? "/img/placeholder.png";
                    Results.Add(new("vehicles", id, p.Name, $"{p.Model} - {p.Manufacturer}", img));
                }
            }

            switch (Type.ToLowerInvariant())
            {
                case "people":
                    await addPeople();
                    break;
                case "planets":
                    await addPlanets();
                    break;
                case "species":
                    await addSpecies();
                    break;
                case "starships":
                    await addStarships();
                    break;
                case "vehicles":
                    await addVehicles();
                    break;
                default:
                    await Task.WhenAll(addPeople(),
                                        addPlanets(),
                                        addSpecies(),
                                        addStarships(),
                                        addVehicles());
                    break;
            }
        }
    }
}
