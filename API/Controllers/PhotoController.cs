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

    [HttpGet, Route("")]
    public JsonResult Get()
    {
        var request = new PhotoViewerRequest { Id = 1001 };
        var result = _photoViewerService.View(request);

        return ResponseHelper.Success(result);
    }
}