using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer.RequestsResults;

public class PhotoViewerCollectionRequest : IRequest
{
    public int AlbumId { get; set; }

    public List<(string Key, string ErrorMessage)> Validate()
    {
        var errors = new List<(string Key, string ErrorMessage)>();

        if(AlbumId <= 0)
            errors.Add((Key: "INVALID_ALBUM_ID", ErrorMessage: "AlbumId is Invalid."));

        return errors;
    }
}