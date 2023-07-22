using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer.RequestsResults;

public class PhotoViewerResult : ResultBase
{
    public PhotoEntry Photo { get; set; }
}