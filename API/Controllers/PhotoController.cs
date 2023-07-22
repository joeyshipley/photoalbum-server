using API.Infrastructure.Controllers;
using Application.Photos.Viewer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IPhotoViewerService _photoViewerService;

    public PhotoController(IPhotoViewerService photoViewerService)
    {
        _photoViewerService = photoViewerService;
    }

    // TODO: Remove this once albums are in place. Should never just return all photos. This was just exploring if the infrastructure was baked.
    [HttpGet, Route("")]
    public async Task<JsonResult> ViewAll()
    {
        var result = await _photoViewerService.ViewAll();
        return ResponseHelper.Respond(result);
    }
    
    [HttpGet, Route("{id}")]
    public async Task<JsonResult> View(int id)
    {
        var request = new PhotoViewerRequest { Id = id };
        var result = await _photoViewerService.View(request);
        return ResponseHelper.Respond(result);
    }
}