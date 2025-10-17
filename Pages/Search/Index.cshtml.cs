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

        public record Card(
            string Type,
            int id,
            string Title,
            string Subtitle,
            string Img
        );

        public List<Card> People { get; set; } = new();
        public List<Card> Planets { get; set; } = new();
        public List<Card> Species { get; set; } = new();
        public List<Card> Others { get; set; } = new(); //Straships e Vehicles


        public async Task OnGetAsync()
        {
            var p1 = await _api.GetPeopleAsync( 1 );
            foreach (var p in p1.Results.Take(8))
            {
                var id = SwapiImageHelper.ExtractId(p.Url);
                var img = await _images.GetImageUrlAsync("people", p.Name) ?? "/img/placeholder.png";
                People.Add(new(
                    "people",
                    id,
                    p.Name,
                    $"{p.BirthYear} - {p.Gender}",
                    img));
            }

            var pl1 = await _api.GetPlanetAsync(1);
            foreach (var p in pl1.Results.Take(8))
            {
                var id = SwapiImageHelper.ExtractId(p.Url);
                var img = await _images.GetImageUrlAsync("planets", p.Name) ?? "/img/placeholder.png";
                Planets.Add(new(
                    "planets",
                    id,
                    p.Name,
                    $"{p.Climate} - {p.Population}",
                    img));
            }

            var s1 = await _api.GetSpeciesAsync(1);
            foreach (var p in s1.Results.Take(8))
            {
                var id = SwapiImageHelper.ExtractId(p.Url);
                var img = await _images.GetImageUrlAsync("species", p.Name) ?? "/img/placeholder.png";
                Species.Add(new(
                    "species",
                    id,
                    p.Name,
                    $"{p.Classification} - {p.Language}",
                    img));
            }

            var st1 = await _api.GetStarshipAsync(1);
            foreach (var p in st1.Results.Take(4))
            {
                var id = SwapiImageHelper.ExtractId(p.Url);
                var img = await _images.GetImageUrlAsync("starships", p.Name) ?? "/img/placeholder.png";
                Others.Add(new(
                    "starships",
                    id,
                    p.Name,
                    $"{p.Model} - {p.Manufacturer}",
                    img));
            }

            var v1 = await _api.GetVehicleAsync(1);
            foreach (var p in v1.Results.Take(4))
            {
                var id = SwapiImageHelper.ExtractId(p.Url);
                var img = await _images.GetImageUrlAsync("vehicles", p.Name) ?? "/img/placeholder.png";
                Others.Add(new(
                    "vehicles",
                    id,
                    p.Name,
                    $"{p.Model} - {p.Manufacturer}",
                    img));
            }
        }
    }
}
