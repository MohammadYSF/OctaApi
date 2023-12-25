using MediatR;

namespace OctaApi.Application.Features.CustomerFeatures.GetCustomers;

public record GetCustomersRequest:IRequest<GetCustomersResponse>
{
}
