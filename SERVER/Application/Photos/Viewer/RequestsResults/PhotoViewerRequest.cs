using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Viewer.RequestsResults;

public class PhotoViewerRequest : IRequest
{
    public int PhotoId { get; set; }

    public List<(string Key, string ErrorMessage)> Validate()
    {
        var errors = new List<(string Key, string ErrorMessage)>();

        if(PhotoId <= 0)
            errors.Add((Key: "INVALID_PHOTO_ID", ErrorMessage: "PhotoId is Invalid."));

        return errors;
    }

}