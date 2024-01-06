using Command.Core.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Command.Core.Application.Common.Behavior;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        //var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(x => x.ErrorMessage)
            .Distinct()
            .ToArray();

        if (errors.Any())
            throw new BadRequestException(errors);

        return await next();
    }

    //private static TResult CreateValidationResult<TResult>(Error[] errors)
    //{
    //    object validationResult = typeof(ValidationResult<>)
    //        .GetGenericTypeDefinition()
    //        .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
    //        .GetMethod(nameof(ValidationResult.MemberNames))
    //        .Invoke(null, new object?[] { errors });
    //}
}