using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;

public sealed class GetCustomersMinimalHandler : IRequestHandler<GetCustomersMinimalRequest, GetCustomersMinimalResponse>
{
    private readonly ICustomerQueryRepository _customerRepository;

    public GetCustomersMinimalHandler(ICustomerQueryRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomersMinimalResponse> Handle(GetCustomersMinimalRequest request, CancellationToken cancellationToken)
    {
        var data = (await _customerRepository.GetAsync()).Select(a => new GetCustomersMinimal_DTO(a.Id, int.Parse(a.CustomerCode), a.CustomerFirstName + " " + a.CustomerLastName)).ToList();
        var response = new GetCustomersMinimalResponse(data);
        return response;
    }
}
