namespace Domain.Exceptions;

public class DomainException : Exception
{
    public object? Error { get; init; }

    public DomainException(string message, object? error = null) : base(message)
    {
        Error = error;
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    { }

    protected DomainException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}