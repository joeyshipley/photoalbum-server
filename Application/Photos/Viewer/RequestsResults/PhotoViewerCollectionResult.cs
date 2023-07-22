using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer.RequestsResults;

public class PhotoViewerCollectionResult : ResultBase
{
    public List<PhotoEntryDto> Photos { get; set; } = new List<PhotoEntryDto>();
}