using Application.Infrastructure.RequestResponse;

namespace Application.Albums.Viewer.RequestsResults;

public class AlbumViewerResult : ResultBase
{
    public AlbumEntry Album { get; set; }
}