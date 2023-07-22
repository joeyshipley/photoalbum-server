using Application.Infrastructure.External;
using Application.Photos.External;
using Application.Photos.Persistence;
using Application.Photos.Viewer.RequestsResults;

namespace Application.Photos.Viewer;

public interface IPhotoViewerService
{
    Task<PhotoViewerCollectionResult> ViewForAlbum(PhotoViewerCollectionRequest request);
    Task<PhotoViewerResult> View(PhotoViewerRequest request);
}

public class PhotoViewerService : IPhotoViewerService
{
    private readonly IUrlProvider _urlProvider;
    private readonly IApiCaller _apiCaller;
    private readonly IPhotoRepository _photoRepository;

    public PhotoViewerService(
        IUrlProvider urlProvider,
        IApiCaller apiCaller,
        IPhotoRepository photoRepository
    )
    {
        _urlProvider = urlProvider;
        _apiCaller = apiCaller;
        _photoRepository = photoRepository;
    }

    public async Task<PhotoViewerCollectionResult> ViewForAlbum(PhotoViewerCollectionRequest request)
    {
        var result = new PhotoViewerCollectionResult();
        
        var requestErrors = request.Validate();
        if(requestErrors.Any())
        {
            result.AddErrors(requestErrors);
            return result;
        }
        
        var url = _urlProvider.AlbumManyPhotosUrl(request.AlbumId);
        var response = await _apiCaller.GetAsync<List<PhotoExternalSourceDto>>(url);

        if(!response.WasSuccessful())
        {
            result.AddErrors(response.Errors.Select(e => (Key: "API_FAILURE", Text: e)).ToList());
            return result;
        }

        result.Photos = response.Model
            .Select(PhotoEntryDto.From)
            .ToList();
        return result;
    }

    public async Task<PhotoViewerResult> View(PhotoViewerRequest request)
    {
        var result = new PhotoViewerResult();
        
        var requestErrors = request.Validate();
        if(requestErrors.Any())
        {
            result.AddErrors(requestErrors);
            return result;
        }

        var url = _urlProvider.PhotoSingleUrl(request.PhotoId);
        var response = await _apiCaller.GetAsync<PhotoExternalSourceDto>(url);
        if(!response.WasSuccessful())
        {
            result.AddErrors(response.Errors.Select(e => (Key: "API_FAILURE", Text: e)).ToList());
            return result;
        }

        var photoDetailsDto = PhotoDetailsDto.From(response.Model);
        var photoEntity = await _photoRepository.Find(request.PhotoId);
        if(photoEntity != null)
            photoDetailsDto.Likes = photoEntity.Likes;
        
        result.Photo = photoDetailsDto;
        return result;
    }
}