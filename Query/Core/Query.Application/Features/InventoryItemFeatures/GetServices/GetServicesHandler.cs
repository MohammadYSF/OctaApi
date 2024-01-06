using MediatR;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.Inventory.GetServices;
public sealed class GetServicesHandler : IRequestHandler<GetServicesRequest, GetServicesResponse>
{
    private readonly IServiceQueryRepository _serviceQueryRepository;
    private readonly IDistributedCacheService<ServiceRM> _serviceCacheService;

    public GetServicesHandler(IServiceQueryRepository serviceQueryRepository, IDistributedCacheService<ServiceRM> serviceCacheService)
    {
        _serviceQueryRepository = serviceQueryRepository;
        _serviceCacheService = serviceCacheService;
    }
    public async Task<GetServicesResponse> Handle(GetServicesRequest request, CancellationToken cancellationToken)
    {
        await _serviceQueryRepository.CheckCacheAsync();
        var data = _serviceCacheService.GetAll().ToList();
        //var data = await _serviceQueryRepository.GetAsync();
        var response = new GetServicesResponse(data);
        return response;
    }
}
