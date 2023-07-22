using Application.Infrastructure.RequestResponse;

namespace Application.Albums.Viewer.RequestsResults;

public class AlbumViewerCollectionResult : ResultBase
{
    public List<AlbumEntry> Albums { get; set; } = new List<AlbumEntry>();
}