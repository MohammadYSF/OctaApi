using MediatR;

namespace OctaApi.Application.Features.VehicleFeatures.GetVehiclesMinimal
{
    public sealed record GetVehiclesMinimalRequest() : IRequest<GetVehiclesMinimalResponse>;

}
