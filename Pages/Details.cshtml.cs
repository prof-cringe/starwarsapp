using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using starwarsapp.Models;
using starwarsapp.Services;

namespace starwarsapp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ISwapiClient _api;
        private readonly IImageService _images;

        public DetailsModel(ISwapiClient api, IImageService images)
        {
            _api = api;
            _images = images;
        }

        [BindProperty(SupportsGet = true)] public string Type { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)] public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Dictionary<string, string> Fields { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string type, int id)
        {
            Type = type.ToLowerInvariant();
            Id = id;

            switch (Type)
            {
                case "people":
                    var p = _api.GetPersonByIdAsync(id);
                    var response = p.Result ?? null;
                    if (response == null)
                    {
                        return NotFound();
                    }

                    Title = response.Name;
                    ImageUrl = await _images.GetImageUrlAsync("people", response.Name) ?? "/img/placeholder.png";
                    Fields = new()
                    {
                        ["Nome"] = response.Name,
                        ["Nascimento"] = response.BirthYear ?? "-",
                        ["Gênero"] = response.Gender ?? "-"
                    };
                    break;

                case "planets":
                    var p1 = _api.GetPlanetByIdAsync(id);
                    var response1 = p1.Result ?? null;
                    if (response1 == null)
                    {
                        return NotFound();
                    }

                    Title = response1.Name;
                    ImageUrl = await _images.GetImageUrlAsync("planets", response1.Name) ?? "/img/placeholder.png";
                    Fields = new()
                    {
                        ["Nome"] = response1.Name,
                        ["Clima"] = response1.Climate ?? "-",
                        ["População"] = response1.Population ?? "-"
                    };
                    break;

                case "species":
                    var s = _api.GetSpeciesByIdAsync(id);
                    var response2 = s.Result ?? null;
                    if (response2 == null)
                    {
                        return NotFound();
                    }

                    Title = response2.Name;
                    ImageUrl = await _images.GetImageUrlAsync("people", response2.Name) ?? "/img/placeholder.png";
                    Fields = new()
                    {
                        ["Nome"] = response2.Name,
                        ["Classificação"] = response2.Classification ?? "-",
                        ["Idioma"] = response2.Language ?? "-"
                    };
                    break;

                case "starships":
                    var st = _api.GetStarshipByIdAsync(id);
                    var response3 = st.Result ?? null;
                    if (response3 == null)
                    {
                        return NotFound();
                    }

                    Title = response3.Name;
                    ImageUrl = await _images.GetImageUrlAsync("starships", response3.Name) ?? "/img/placeholder.png";
                    Fields = new()
                    {
                        ["Nome"] = response3.Name,
                        ["Modelo"] = response3.Model ?? "-",
                        ["Fabricante"] = response3.Manufacturer ?? "-"
                    };
                    break;

                case "vehicles":
                    var v = _api.GetStarshipByIdAsync(id);
                    var response4 = v.Result ?? null;
                    if (response4 == null)
                    {
                        return NotFound();
                    }

                    Title = response4.Name;
                    ImageUrl = await _images.GetImageUrlAsync("vehicles", response4.Name) ?? "/img/placeholder.png";
                    Fields = new()
                    {
                        ["Nome"] = response4.Name,
                        ["Modelo"] = response4.Model ?? "-",
                        ["Fabricante"] = response4.Manufacturer ?? "-"
                    };
                    break;
                default:
                    return NotFound();
            }
            return Page();
        }
    }
}
