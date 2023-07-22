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
    
    [HttpGet, Route("{id}")]
    public async Task<JsonResult> View(int id)
    {
        var request = new PhotoViewerRequest { Id = id };
        var result = await _photoViewerService.View(request);
        return ResponseHelper.Respond(result);
    }
}