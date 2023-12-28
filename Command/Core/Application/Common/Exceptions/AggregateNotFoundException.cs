using Command.Core.Domain.Core;

namespace Command.Core.Application.Common.Exceptions;

public class AggregateNotFoundException<T> : Exception where T : AggregateRoot
{
    public AggregateNotFoundException(string message) : base(message)
    {

    }
    public AggregateNotFoundException(string[] errors) : base("Multiple errors occurred. See error details.")
    {
        Errors = errors;
    }

    public string[] Errors { get; set; }
}
public class EntityNotFoundException<T> : Exception where T : Entity
{
    public EntityNotFoundException(string message) : base(message)
    {

    }
    public EntityNotFoundException(string[] errors) : base("Multiple errors occurred. See error details.")
    {
        Errors = errors;
    }

    public string[] Errors { get; set; }
}
