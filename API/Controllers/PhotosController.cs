using API.Infrastructure.Controllers;
using Application.Infrastructure.RequestResponse;
using Application.Photos;
using Application.Photos.Persistence;
using Application.Photos.Viewer;
using Application.Photos.Viewer.RequestsResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoViewerService _photoViewerService;
    private readonly IPhotoRepository _photoRepository;

    public PhotosController(
        IPhotoViewerService photoViewerService,
        IPhotoRepository photoRepository
    )
    {
        _photoViewerService = photoViewerService;
        _photoRepository = photoRepository;
    }
    
    [HttpGet, Route("{photoId}")]
    public async Task<JsonResult> View(int photoId)
    {
        var request = new PhotoViewerRequest { PhotoId = photoId };
        var result = await _photoViewerService.View(request);
        return ResponseHelper.Respond(result);
    }

    [HttpGet, Route("album/{albumId}")]
    public async Task<JsonResult> ViewAll(int albumId)
    {
        var request = new PhotoViewerCollectionRequest { AlbumId = albumId };
        var result = await _photoViewerService.ViewForAlbum(request);
        return ResponseHelper.Respond(result);
    }

    [HttpPost, Route("{photoId}/like")]
    public async Task<JsonResult> Like(int photoId)
    {
        // TODO: PoC successful, implement in service.
        var entity = await _photoRepository.Upsert(new PhotoDetailsEntity { PhotoId = photoId, Likes = 1 });
        await _photoRepository.SaveChangesAsync();

        var photo = await _photoRepository.Find(photoId);

        return ResponseHelper.Respond(new ResultBase {});
    }
}