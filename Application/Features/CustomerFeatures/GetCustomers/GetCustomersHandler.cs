using Application.Repositories.Command;
using Application.Repositories.Query;
using MediatR;
using OctaApi.Domain.Models;
namespace OctaApi.Application.Features.CustomerFeatures.GetCustomers;

public sealed class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    //private ICustomerCommandRepository _customerRepository;

    private ICustomerQueryRepository _customerQueryRepository;
    public GetCustomersHandler(ICustomerQueryRepository customerQueryRepository)
    {
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        var data = await _customerQueryRepository.GetAsync();
        var response = new GetCustomersResponse
        {
            Count = data.Count(),
            Data = data
        };
        return response;
    }
}
