using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer.RequestsResults;

public class PhotoViewerResult : ResultBase
{
    public PhotoDetailsDto Photo { get; set; }
}