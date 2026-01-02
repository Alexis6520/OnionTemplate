using System.Net;
using System.Text.Json.Serialization;

namespace Application.ResultPattern;

public class Result<T>(T? value, Error? error, HttpStatusCode statusCode)
{
    public T? Value { get; } = value;
    public Error? Error { get; } = error;

    [JsonIgnore]
    public bool Succeeded => Error is null;

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public record Error(string Code, string Message);

public readonly struct Unit
{
    public static readonly Unit Value = new();
}

public static class Result
{
    public static Result<T> Success<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (statusCode >= HttpStatusCode.BadRequest)
        {
            throw new ArgumentException(
                "Success status code must be less than 400.",
                nameof(statusCode)
            );
        }

        return new(value, null, statusCode);
    }

    public static Result<Unit> Success(HttpStatusCode statusCode = HttpStatusCode.NoContent)
    {
        return Success(Unit.Value, statusCode);
    }

    public static Result<T> Failure<T>(Error error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        if (statusCode < HttpStatusCode.BadRequest)
        {
            throw new ArgumentException(
                "Failure status code must be 400 or greater.",
                nameof(statusCode)
            );
        }
        return new(default, error, statusCode);
    }
}
