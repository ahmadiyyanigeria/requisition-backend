namespace Application.Exceptions;

public class ApplicationException : Exception
{
    public int HttpStatusCode { get; init; }
    public object? Error { get; init; }
    public string ErrorCode { get; init; } = default!;

    public ApplicationException(string message, string errorCode, int statusCode, object? error = null) : base(message)
    {
        HttpStatusCode = statusCode;
        Error = error;
        ErrorCode = errorCode;
    }

    public ApplicationException(string message, Exception innerException) : base(message, innerException)
    { }

    protected ApplicationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}