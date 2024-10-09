using Microsoft.AspNetCore.Mvc;
using Store.S_02.APIs.Error;

namespace Store.S_02.APIs.Controllers;

[Route("error/[code]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]

public class ErrorsController : ControllerBase
{
    public IActionResult Error(int code)
    {
        return NotFound(new APiErrorResponse(StatusCodes.Status404NotFound, "Resource not found"));
    }
}