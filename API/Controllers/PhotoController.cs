using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return Enumerable.Range(1, 5).Select(i => $"Photo #{ i }");
    }
}