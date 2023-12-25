using Application.Repositories;
using Application.Repositories.Command;
using AutoMapper;
using Domain.Customer;
using Domain.Vehicle;
using MediatR;
using OctaApi.Application.DomainModels;
using OctaApi.Application.Repositories;
using OctaApi.Domain.InventoryItem.ValueObjects;
using OctaApi.Domain.Models;
namespace OctaApi.Application.Features.CustomerFeatures.AddCustomer;
public class AddCustomerHandler : IRequestHandler<AddCustomerRequest, AddCustomerResponse>
{
    private readonly ICustomerCommandRepository _customerRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public AddCustomerHandler(ICustomerCommandRepository customerRepository, ICommandUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<AddCustomerResponse> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        //var customer = _mapper.Map<Customer>(request);
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
            await _eventBus.PublishAsync(item);
        }
        var response = new AddCustomerResponse();
        return response;
    }
}
