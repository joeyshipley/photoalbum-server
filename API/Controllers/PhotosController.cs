using API.Infrastructure.Controllers;
using Application.Photos.Viewer;
using Application.Photos.Viewer.RequestsResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoViewerService _photoViewerService;

    public PhotosController(IPhotoViewerService photoViewerService)
    {
        _photoViewerService = photoViewerService;
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
}