using API.Infrastructure.Controllers;
using Application.Albums.Viewer;
using Application.Albums.Viewer.RequestsResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumViewerService _photoViewerService;

    public AlbumsController(IAlbumViewerService photoViewerService)
    {
        _photoViewerService = photoViewerService;
    }

    [HttpGet, Route("")]
    public async Task<JsonResult> ViewAll()
    {
        var request = new AlbumViewerCollectionRequest();
        var result = await _photoViewerService.ViewAll(request);
        return ResponseHelper.Respond(result);
    }
    
    [HttpGet, Route("{id}")]
    public async Task<JsonResult> View(int id)
    {
        var request = new AlbumViewerRequest { Id = id };
        var result = await _photoViewerService.View(request);
        return ResponseHelper.Respond(result);
    }
}