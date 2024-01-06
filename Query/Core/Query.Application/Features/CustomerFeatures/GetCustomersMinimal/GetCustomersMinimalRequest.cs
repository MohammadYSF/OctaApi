using MediatR;
namespace OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;

public sealed record GetCustomersMinimalRequest() : IRequest<GetCustomersMinimalResponse>;

