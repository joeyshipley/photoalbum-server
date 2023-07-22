namespace Application.Photos.Viewer;

public interface IPhotoViewerService
{
    PhotoViewerResult View(PhotoViewerRequest request);
}

public class PhotoViewerService : IPhotoViewerService
{
    public PhotoViewerResult View(PhotoViewerRequest request)
    {
        return new PhotoViewerResult
        {
            Photo = new PhotoEntry { Id = 1001, AlbumId = 101, Title = "Nope", ThumbnailUrl = "nope.nope", Url = "nope.nope" }
        };
    }
}