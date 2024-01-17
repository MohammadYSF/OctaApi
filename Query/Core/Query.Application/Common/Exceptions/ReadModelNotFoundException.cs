namespace Query.Application.Common.Exceptions;

public class ReadModelNotFoundException<T> : Exception
{
    public ReadModelNotFoundException(string message) : base(message)
    {

    }
    public ReadModelNotFoundException(string[] errors) : base("Multiple errors occurred. See error details.")
    {
        Errors = errors;
    }

    public ReadModelNotFoundException()
    {
        Errors.Append($"{nameof(T)} not found");
    }

    public string[] Errors { get; set; } = { };
}

