namespace Application.Albums.Viewer.RequestsResults;

public class AlbumViewerCollectionRequest
{
    // NOTE: Currently Hard-coding to userId 1. This would normally come in via auth architecture, outside scope of weekend/tiny project.
    public int UserId => 1;
}