using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace OctaApi.Application.Features.CustomerFeatures.GetCustomers;

public sealed class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    //private ICustomerCommandRepository _customerRepository;
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly IDistributedCacheService<CustomerRM> _customerRMCache;

    public GetCustomersHandler(ICustomerQueryRepository customerQueryRepository, IDistributedCacheService<CustomerRM> customerRMCache)
    {
        _customerQueryRepository = customerQueryRepository;
        _customerRMCache = customerRMCache;
    }

    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        await _customerQueryRepository.CheckCacheAsync();
        var data = _customerRMCache.GetAll().ToList();
        //var data = await _customerQueryRepository.GetAsync();
        var response = new GetCustomersResponse
        {
            Count = data.Count(),
            Data = data
        };
        return response;
    }
}
