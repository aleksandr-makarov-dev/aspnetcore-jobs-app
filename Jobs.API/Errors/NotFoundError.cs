using FluentResults;

namespace Jobs.API.Errors
{
    public class NotFoundError(string message) : Error(message);
}
