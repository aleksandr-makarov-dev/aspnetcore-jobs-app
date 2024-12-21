using FluentResults;
using Jobs.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.API.Helpers
{
    public class ExtendedControllerBase:ControllerBase
    {
        public IActionResult HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.ValueOrDefault);
            }

            if (result.HasError<BadRequestError>(out var badRequestErrors))
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "400 Bad Request",
                    Detail = badRequestErrors.FirstOrDefault()?.Message
                });
            }

            if (result.HasError<NotFoundError>(out var notFoundErrors))
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "404 Not Found",
                    Detail = notFoundErrors.FirstOrDefault()?.Message
                });
            }

            if (result.HasError<UnauthorizedError>(out var unauthorizedErrors))
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "401 Unauthorized",
                    Detail = unauthorizedErrors.FirstOrDefault()?.Message
                });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "500 Internal Server Error",
                Detail = "An unexpected error occurred"
            });
        }
    }
}
