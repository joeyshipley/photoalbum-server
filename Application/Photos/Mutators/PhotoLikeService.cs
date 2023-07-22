using Application.Infrastructure.External;
using Application.Photos.External;
using Application.Photos.Mutators.RequestsResults;
using Application.Photos.Persistence;

namespace Application.Photos.Mutators;

public interface ILikePhotos
{
    Task<LikeResult> Like(LikeRequest request);
}

public class PhotoLikeService : ILikePhotos
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IUrlProvider _urlProvider;
    private readonly IApiCaller _apiCaller;

    public PhotoLikeService(
        IPhotoRepository photoRepository,
        IUrlProvider urlProvider,
        IApiCaller apiCaller
    )
    {
        _photoRepository = photoRepository;
        _urlProvider = urlProvider;
        _apiCaller = apiCaller;
    }

    public async Task<LikeResult> Like(LikeRequest request)
    {
        var result = new LikeResult();
        var errors = request.Validate();

        if (errors.Any())
        {
            result.Errors = errors;
            return result;
        }

        // NOTE: Refactor opportunity.
        // Now that this logic is shared across both this and the PhotoViewerService,
        // an abstraction can be created around the external API calls itself.
        var url = _urlProvider.PhotoSingleUrl(request.PhotoId);
        var response = await _apiCaller.GetAsync<PhotoExternalSourceDto>(url);
        if(!response.WasSuccessful())
        {
            result.AddErrors(response.Errors.Select(e => (Key: "PHOTO_DOES_NOT_EXIST", Text: e)).ToList());
            return result;
        }

        var entity = await _photoRepository.Find(request.PhotoId) ?? new PhotoDetailsEntity
        {
            PhotoId = request.PhotoId,
            Likes = 0,
        };
        entity.Likes += 1;
        entity = await _photoRepository.Upsert(entity);

        result.PhotoLikeDetails = new LikeDto
        {
            PhotoId = entity.PhotoId,
            Likes = entity.Likes,
        };
        return result;
    }
}