using MediatR;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.Features.VehicleFeatures.GetAllVehicles;
public sealed record GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesRequest, GetAllVehiclesResponse>
{
    private readonly IVehicleQueryRepository _vehicleQueryRepository;
    private readonly IDistributedCacheService<VehicleRM> _vehicleCacheService;

    public GetAllVehiclesHandler(IVehicleQueryRepository vehicleQueryRepository, IDistributedCacheService<VehicleRM> vehicleCacheService)
    {
        _vehicleQueryRepository = vehicleQueryRepository;
        _vehicleCacheService = vehicleCacheService;
    }
    public async Task<GetAllVehiclesResponse> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
    {
        await _vehicleQueryRepository.CheckCacheAsync();
        var data = _vehicleCacheService.GetAll().ToList();
        //var data = await _vehicleQueryRepository.GetAsync();
        var response = new GetAllVehiclesResponse()
        {
            Count = data.Count,
            Data = data
        };
        return response;
    }
}
