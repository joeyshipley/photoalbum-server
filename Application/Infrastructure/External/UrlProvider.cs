namespace Application.Infrastructure.External;

public interface IUrlProvider
{
    string PhotosUrl();
    string PhotoUrl(int id);
}

public class UrlProvider : IUrlProvider
{
    private const string PHOTO_API_BASE_URL = "https://jsonplaceholder.typicode.com";

    public string PhotosUrl() => $"{PHOTO_API_BASE_URL}/photos";
    public string PhotoUrl(int id) => $"{PHOTO_API_BASE_URL}/photos/{id}";
}