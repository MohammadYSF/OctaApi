using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.VehicleFeatures.GetVehiclesMinimal;
public sealed class GetVehiclesMinimalHandler : IRequestHandler<GetVehiclesMinimalRequest, GetVehiclesMinimalResponse>
{
    private readonly IVehicleQueryRepository _vehicleQueryRepository;
    public GetVehiclesMinimalHandler(IVehicleQueryRepository vehicleQueryRepository)
    {
        _vehicleQueryRepository = vehicleQueryRepository;
    }

    public async Task<GetVehiclesMinimalResponse> Handle(GetVehiclesMinimalRequest request, CancellationToken cancellationToken)
    {
        var data = await _vehicleQueryRepository.GetAsync();
        var response = new GetVehiclesMinimalResponse(data);
        return response;
    }
}
