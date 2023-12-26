using MediatR;
using Query.Application.Repositories;
namespace Query.Application.Features.VehicleFeatures.GetAllVehicles;
public sealed record GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesRequest, GetAllVehiclesResponse>
{
    private readonly IVehicleQueryRepository _vehicleQueryRepository;
    public GetAllVehiclesHandler(IVehicleQueryRepository vehicleQueryRepository)
    {
        _vehicleQueryRepository = vehicleQueryRepository;
    }
    public async Task<GetAllVehiclesResponse> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
    {
        var data = await _vehicleQueryRepository.GetAsync();
        var response = new GetAllVehiclesResponse()
        {
            Count = data.Count,
            Data = data
        };
        return response;
    }
}
