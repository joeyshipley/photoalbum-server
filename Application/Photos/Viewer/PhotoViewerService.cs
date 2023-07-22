using Application.Infrastructure.External;
using Application.Photos.Viewer.RequestsResults;

namespace Application.Photos.Viewer;

public interface IPhotoViewerService
{
    Task<PhotoViewerResult> View(PhotoViewerRequest request);
}

public class PhotoViewerService : IPhotoViewerService
{
    private readonly IUrlProvider _urlProvider;
    private readonly IApiCaller _apiCaller;

    public PhotoViewerService(
        IUrlProvider urlProvider,
        IApiCaller apiCaller
    )
    {
        _urlProvider = urlProvider;
        _apiCaller = apiCaller;
    }
    
    public async Task<PhotoViewerResult> View(PhotoViewerRequest request)
    {
        var result = new PhotoViewerResult();
        var url = _urlProvider.PhotoSingleUrl(request.Id);
        var response = await _apiCaller.GetAsync<PhotoEntry>(url);

        if(!response.WasSuccessful())
        {
            result.AddErrors(response.Errors.Select(e => (Key: "API_FAILURE", Text: e)).ToList());
            return result;
        }

        result.Photo = response.Model;
        return result;
    }
}