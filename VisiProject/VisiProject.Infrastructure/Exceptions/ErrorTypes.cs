namespace VisiProject.Infrastructure.Exceptions;

public enum ErrorTypes
{
    ClientError,
    ServerError,
    AuthenticationRequired,
    ResourceNotFound,
    ResourceAlreadyExists,
    AccessForbidden,
    NotAllowed,
    ValidationError,
    Conflict,
    TooManyRequests,
    FailedDependency
}