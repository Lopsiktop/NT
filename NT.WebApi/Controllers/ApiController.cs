using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using NT.WebApi.Common;

namespace NT.WebApi.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected ObjectResult Problem(List<Error> errors)
    {
        var statusCode = errors.First().Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Failure => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        var result = new ObjectResult(new JsonProblem(
            errors.First().Code,
            errors.First().Description,
            statusCode,
            errors.Select(x => new JsonError(x.Code, x.Description)).ToArray()));

        result.StatusCode = statusCode;
        return result;
    }

    protected string GetJwtToken() =>
        Request.Headers.Authorization.ToString().Split(' ').Last();
}
