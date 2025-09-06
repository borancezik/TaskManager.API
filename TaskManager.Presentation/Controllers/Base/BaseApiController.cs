using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Presentation.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected virtual async Task<IActionResult> ApiResponse<T>(T result) where T : Result
    {
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else if (result.Error == Error.NotFound || result.Error == Error.NotFoundByQuery)
        {
            return NotFound(result.Error.Value);
        }
        else
        {
            var errorList = result.Errors.Any() ? result.Errors : new List<Error> { result.Error.Value };

            return BadRequest(errorList);
        }
    }
}
