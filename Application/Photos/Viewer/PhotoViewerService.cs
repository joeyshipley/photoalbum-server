using Application.Infrastructure.External;

namespace Application.Photos.Viewer;

public interface IPhotoViewerService
{
    Task<PhotoViewerCollectionResult> ViewAll();
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
    
    public async Task<PhotoViewerCollectionResult> ViewAll()
    {
        var result = new PhotoViewerCollectionResult();
        var url = _urlProvider.PhotosUrl();
        var response = await _apiCaller.GetAsync<List<PhotoEntry>>(url);

        if(!response.WasSuccessful())
        {
            result.AddErrors(response.Errors.Select(e => (Key: "API_FAILURE", Text: e)).ToList());
            return result;
        }

        result.Photos = response.Model;
        return result;
    }

    public async Task<PhotoViewerResult> View(PhotoViewerRequest request)
    {
        var result = new PhotoViewerResult();
        var url = _urlProvider.PhotoUrl(request.Id);
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