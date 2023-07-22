using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer.RequestsResults;

public class PhotoViewerCollectionResult : ResultBase
{
    public List<PhotoEntry> Photos { get; set; } = new List<PhotoEntry>();
}