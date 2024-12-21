using FluentResults;

namespace Jobs.API.Errors
{
    public class BadRequestError(string message) : Error(message);
}
