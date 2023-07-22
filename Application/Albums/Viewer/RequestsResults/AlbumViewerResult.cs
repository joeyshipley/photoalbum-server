using Application.Albums.External;
using Application.Infrastructure.RequestResponse;

namespace Application.Albums.Viewer.RequestsResults;

public class AlbumViewerResult : ResultBase
{
    public AlbumExternalSourceDto Album { get; set; }
}