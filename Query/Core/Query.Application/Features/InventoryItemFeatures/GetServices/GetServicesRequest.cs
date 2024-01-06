using MediatR;

namespace OctaApi.Application.Features.Inventory.GetServices
{
    public record GetServicesRequest : IRequest<GetServicesResponse>
    {

    }
}
