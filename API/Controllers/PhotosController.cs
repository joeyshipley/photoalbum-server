using API.Infrastructure.Controllers;
using Application.Photos.Mutators;
using Application.Photos.Mutators.RequestsResults;
using Application.Photos.Viewer;
using Application.Photos.Viewer.RequestsResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoViewerService _photoViewerService;
    private readonly ILikePhotos _photosLikeService;

    public PhotosController(
        IPhotoViewerService photoViewerService,
        ILikePhotos photosLikeService
    )
    {
        _photoViewerService = photoViewerService;
        _photosLikeService = photosLikeService;
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
        var request = new LikeRequest { PhotoId = photoId };
        var result = await _photosLikeService.Like(request);
        return ResponseHelper.Respond(result);
    }
}