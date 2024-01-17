using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;

public sealed class GetCustomersMinimalHandler : IRequestHandler<GetCustomersMinimalRequest, GetCustomersMinimalResponse>
{
    private readonly ICustomerQueryRepository _customerRepository;
    private readonly IDistributedCacheService<CustomerRM> _customerRMCache;

    public GetCustomersMinimalHandler(ICustomerQueryRepository customerRepository, IDistributedCacheService<CustomerRM> customerRMCache)
    {
        _customerRepository = customerRepository;
        _customerRMCache = customerRMCache;
    }

    public async Task<GetCustomersMinimalResponse> Handle(GetCustomersMinimalRequest request, CancellationToken cancellationToken)
    {
        await _customerRepository.CheckCacheAsync();

        var data = _customerRMCache.GetAll().Select(a => new GetCustomersMinimal_DTO(a.Id, int.Parse(a.CustomerCode), a.CustomerFirstName + " " + a.CustomerLastName)).ToList();
        var response = new GetCustomersMinimalResponse(data);
        return response;
    }
}
