using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
namespace OctaApi.Application.Features.CustomerFeatures.GetCustomers;

public sealed class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    private ICustomerRepository _customerRepository;

    public GetCustomersHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        var data = (await _customerRepository.GetAllAsync()).Select((a, i) => new GetCustomersResponse_DTO
        {
            Code = a.Code.ToString(),
            FirstName = a.FirstName,
            LastName = a.LastName,
            RowNumber = i + 1,
            CustomerPhoneNumber = a.PhoneNumber
        }).ToList();
        var response = new GetCustomersResponse
        {
            Count = data.Count(),
            Data = data
        };
        return response;
    }
}
