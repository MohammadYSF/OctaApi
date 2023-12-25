using Application.Repositories.Command;
using MediatR;
namespace OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;

public sealed class GetCustomersMinimalHandler : IRequestHandler<GetCustomersMinimalRequest, GetCustomersMinimalResponse>
{
    private readonly ICustomerCommandRepository _customerRepository;

    public GetCustomersMinimalHandler(ICustomerCommandRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomersMinimalResponse> Handle(GetCustomersMinimalRequest request, CancellationToken cancellationToken)
    {
        var data = (await _customerRepository.GetAllAsync()).Select(a => new GetCustomersMinimal_DTO(a.Id, a.Code, a.FirstName + " " + a.LastName)).ToList();
        var response = new GetCustomersMinimalResponse(data);
        return response;
    }
}
