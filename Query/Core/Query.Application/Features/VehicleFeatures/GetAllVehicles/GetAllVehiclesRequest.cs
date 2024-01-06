using MediatR;
namespace Query.Application.Features.VehicleFeatures.GetAllVehicles;

public record GetAllVehiclesRequest : IRequest<GetAllVehiclesResponse>
{
}
