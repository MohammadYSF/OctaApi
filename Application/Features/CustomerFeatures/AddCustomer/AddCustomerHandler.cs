using Application.Repositories;
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
    private readonly ICustomerRepository _customerRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public AddCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus)
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
        List<VehicleAggregate> vehicleAggregates = new();
        List<int> newlyGeneratedVehicleCodes = await _vehicleRepository.GenerateNewVehicleCodesAsync(request.VehicleDTOs.Count);
        for (int k = 0; k < newlyGeneratedVehicleCodes.Count; k++)
        {
            var vehicleId = Guid.NewGuid();
            vehicleAggregates.Add(VehicleAggregate.Create(vehicleId, newlyGeneratedVehicleCodes[k], request.VehicleDTOs[k].Name, request.VehicleDTOs[k].Plate, request.VehicleDTOs[k].Color));
            customerAggregate.AddVehicle(vehicleId);
        }
        await _customerRepository.AddAsync(customerAggregate);
        await _vehicleRepository.AddAsync(vehicleAggregates);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in customerAggregate.GetDomainEvents())
        {
            await _eventBus.PublishAsync(item);
        }
        var response = new AddCustomerResponse();
        return response;
    }
}
