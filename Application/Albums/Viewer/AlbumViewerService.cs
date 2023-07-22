using Application.Albums.Viewer.RequestsResults;
using Application.Infrastructure.External;

namespace Application.Albums.Viewer;

public interface IAlbumViewerService
{
    Task<AlbumViewerCollectionResult> ViewAll(AlbumViewerCollectionRequest request);
    Task<AlbumViewerResult> View(AlbumViewerRequest request);
}

public class AlbumViewerService : IAlbumViewerService
{
private readonly IUrlProvider _urlProvider;
    private readonly IApiCaller _apiCaller;

    public AlbumViewerService(
        IUrlProvider urlProvider,
        IApiCaller apiCaller
    )
    {
        _urlProvider = urlProvider;
        _apiCaller = apiCaller;
    }
    
    public async Task<AlbumViewerCollectionResult> ViewAll(AlbumViewerCollectionRequest request)
    {
        var result = new AlbumViewerCollectionResult();

        var requestErrors = request.Validate();
        if(requestErrors.Any())
        {
            result.AddErrors(requestErrors);
            return result;
        }

        var url = _urlProvider.UserManyAlbumsUrl(request.UserId);
        var response = await _apiCaller.GetAsync<List<AlbumEntry>>(url);

        if(!response.WasSuccessful())
        {
            result.AddErrors(response.Errors.Select(e => (Key: "API_FAILURE", Text: e)).ToList());
            return result;
        }

        result.Albums = response.Model;
        return result;
    }

    public async Task<AlbumViewerResult> View(AlbumViewerRequest request)
    {
        var result = new AlbumViewerResult();
        
        var requestErrors = request.Validate();
        if(requestErrors.Any())
        {
            result.AddErrors(requestErrors);
            return result;
        }
        
        var url = _urlProvider.AlbumSingleUrl(request.AlbumId);
        var response = await _apiCaller.GetAsync<AlbumEntry>(url);

        if(!response.WasSuccessful())
        {
            result.AddErrors(response.Errors.Select(e => (Key: "API_FAILURE", Text: e)).ToList());
            return result;
        }

        result.Album = response.Model;
        return result;
    }
}