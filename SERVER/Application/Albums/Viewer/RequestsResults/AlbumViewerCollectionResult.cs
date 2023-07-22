using Application.Albums.External;
using Application.Infrastructure.RequestResponse;

namespace Application.Albums.Viewer.RequestsResults;

public class AlbumViewerCollectionResult : ResultBase
{
    public List<AlbumExternalSourceDto> Albums { get; set; } = new List<AlbumExternalSourceDto>();
}