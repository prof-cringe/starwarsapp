namespace starwarsapp.Services
{
    public interface IImageService
    {
        Task<string?> GetImageUrlAsync(string type, string name);
    }
}
