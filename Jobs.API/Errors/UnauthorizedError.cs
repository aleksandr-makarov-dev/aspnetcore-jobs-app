using FluentResults;

namespace Jobs.API.Errors;

public class UnauthorizedError(string message) : Error(message);