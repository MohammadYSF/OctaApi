using Command.Core.Application.Repositories;
using Command.Core.Domain.Customer;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.CustomerFeatures.AddCustomer;
public class AddCustomerHandler : IRequestHandler<AddCustomerRequest, AddCustomerResponse>
{
    private readonly ICustomerCommandRepository _customerRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public AddCustomerHandler(ICustomerCommandRepository customerRepository, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<AddCustomerResponse> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var customerId = Guid.NewGuid();
        int customerCode = await _customerRepository.GenerateNewCustomerCodeAsync();
        var customerAggregate = CustomerAggregate.Create(customerId, customerCode, request.FirstName, request.LastName, request.phoneNumber);
        for (int k = 0; k < request.VehicleDTOs.Count; k++)
        {
            var vehicleId = Guid.NewGuid();
            customerAggregate.AddVehicle(vehicleId, request.VehicleDTOs[k].Name, request.VehicleDTOs[k].Plate, request.VehicleDTOs[k].Color);
        }
        await _customerRepository.AddAsync(customerAggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in customerAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new AddCustomerResponse();
        return response;
    }
}
