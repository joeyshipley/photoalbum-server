using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer;

public class PhotoViewerCollectionRequest
{
    
}

public class PhotoViewerCollectionResponse : ResultBase
{
    public List<PhotoEntry> Photos { get; set; } = new List<PhotoEntry>();
}