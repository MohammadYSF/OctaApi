using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.VehicleFeatures.GetVehiclesMinimal;
public sealed class GetVehiclesMinimalHandler : IRequestHandler<GetVehiclesMinimalRequest, GetVehiclesMinimalResponse>
{
    private readonly IVehicleQueryRepository _vehicleQueryRepository;
    private readonly IDistributedCacheService<VehicleRM> _vehicleCacheService;

    public GetVehiclesMinimalHandler(IVehicleQueryRepository vehicleQueryRepository, IDistributedCacheService<VehicleRM> vehicleCacheService)
    {
        _vehicleQueryRepository = vehicleQueryRepository;
        _vehicleCacheService = vehicleCacheService;
    }

    public async Task<GetVehiclesMinimalResponse> Handle(GetVehiclesMinimalRequest request, CancellationToken cancellationToken)
    {
        await _vehicleQueryRepository.CheckCacheAsync();
        var data = _vehicleCacheService.GetAll().ToList();
        //var data = await _vehicleQueryRepository.GetAsync();
        var response = new GetVehiclesMinimalResponse(data);
        return response;
    }
}
