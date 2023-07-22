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

    [HttpGet, Route("{id}")]
    public async Task<JsonResult> View(int id)
    {
        var request = new PhotoViewerRequest { Id = id };
        var result = await _photoViewerService.View(request);
        if(result.Errors.Any())
            return ResponseHelper.Fail(result.Errors);

        return ResponseHelper.Success(result);
    }
}