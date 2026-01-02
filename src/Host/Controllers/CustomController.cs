using Application.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class CustomController : ControllerBase
{
    protected ObjectResult BuildResponse<T>(Result<T> result)
    {
        return StatusCode((int)result.StatusCode, result);
    }

    protected ObjectResult BuildResponse(Result<Unit> result)
    {
        var statusCode = (int)result.StatusCode;
        if (result.Succeeded) return StatusCode(statusCode, null);
        return StatusCode(statusCode, result);
    }

    protected OkObjectResult BuildResponse<T>(T value)
    {
        return Ok(value);
    }
}
