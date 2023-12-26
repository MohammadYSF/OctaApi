using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.Inventory.GetServices;
public sealed class GetServicesHandler : IRequestHandler<GetServicesRequest, GetServicesResponse>
{
    private readonly IServiceQueryRepository _serviceQueryRepository;
    public GetServicesHandler(IServiceQueryRepository serviceQueryRepository)
    {
        _serviceQueryRepository = serviceQueryRepository;
    }
    public async Task<GetServicesResponse> Handle(GetServicesRequest request, CancellationToken cancellationToken)
    {
        var data = await _serviceQueryRepository.GetAsync();
        var response = new GetServicesResponse(data);
        return response;
    }
}
