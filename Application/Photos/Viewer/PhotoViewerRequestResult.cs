using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer;

public class PhotoViewerRequest
{
    public int Id { get; set; }
}

public class PhotoViewerResult : ResultBase
{
    public PhotoEntry Photo { get; set; }
}