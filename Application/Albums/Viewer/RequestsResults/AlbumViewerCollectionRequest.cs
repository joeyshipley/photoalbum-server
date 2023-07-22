using Application.Infrastructure.RequestResponse;

namespace Application.Albums.Viewer.RequestsResults;

public class AlbumViewerCollectionRequest : IRequest
{
    // NOTE: Currently Hard-coding to userId 1. This would normally come in via auth architecture, outside scope of weekend/tiny project.
    public int UserId { get; set; } = 1;

    public List<(string Key, string ErrorMessage)> Validate()
    {
        var errors = new List<(string Key, string ErrorMessage)>();

        if(UserId <= 0)
            errors.Add((Key: "INVALID_USER_ID", ErrorMessage: "UserId is Invalid."));

        return errors;
    }

}