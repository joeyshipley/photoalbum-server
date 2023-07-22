namespace Application.Infrastructure.External;

public interface IUrlProvider
{
    string AlbumSingleUrl(int id);
    string AlbumManyPhotosUrl(int albumId);
    string PhotoSingleUrl(int id);
    string UserManyAlbumsUrl(int userId);
}

public class UrlProvider : IUrlProvider
{
    private const string PHOTO_API_BASE_URL = "https://jsonplaceholder.typicode.com";

    public string AlbumSingleUrl(int id) => $"{ PHOTO_API_BASE_URL }/albums/{ id }";
    public string AlbumManyPhotosUrl(int albumId) => $"{ PHOTO_API_BASE_URL }/albums/{ albumId }/photos";
    public string PhotoSingleUrl(int id) => $"{ PHOTO_API_BASE_URL }/photos/{ id }";
    public string UserManyAlbumsUrl(int userId) => $"{ PHOTO_API_BASE_URL }/users/{ userId }/albums";
}