using API.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    [HttpGet, Route("")]
    public JsonResult Get()
    {
        var result = Enumerable.Range(1, 5).Select(i => $"Photo #{ i }");

        return ResponseHelper.Success(result);
    }
}