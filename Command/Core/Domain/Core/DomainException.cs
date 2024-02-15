using Command.Core.Domain.Core;

namespace Command.Domain.Core;

public class DomainException<T> : Exception where T : AggregateRoot
{
    public DomainException(string message) : base(message)
    {

    }
    public DomainException(string[] errors) : base("Multiple errors occurred. See error details.")
    {
        Errors = errors;
    }

    public string[] Errors { get; set; }
}