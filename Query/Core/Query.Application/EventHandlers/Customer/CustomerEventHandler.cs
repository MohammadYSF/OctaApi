using Query.Application.Core;
using Query.Application.Events.Customer;
using Query.Application.Events.Vehicles;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.EventHandlers.Customer;
public class CustomerEventHandler :
    IEventHandler<CustomerCreatedEvent>,
    IEventHandler<VehicleCreatedEvent>
{
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly IQueryUnitOfWork _queryUnitOfWork;
    private readonly IDistributedCacheService<CustomerRM> _customerRMCacheService;

    public CustomerEventHandler(ICustomerQueryRepository customerQueryRepository, IQueryUnitOfWork queryUnitOfWork, IDistributedCacheService<CustomerRM> customerRMCacheService)
    {
        _customerQueryRepository = customerQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
        _customerRMCacheService = customerRMCacheService;
    }

    public async Task HandleAsync(CustomerCreatedEvent @event)
    {
        var customerRM = new CustomerRM
        {
            Id = Guid.NewGuid(),
            CustomerCode = @event.CustomerId.ToString(),
            CustomerId = @event.CustomerId,
            CustomerPhoneNumber = @event.PhoneNumber,
            CustomerFirstName = @event.FirstName,
            CustomerLastName = @event.LastName,
        };
        await _customerQueryRepository.AddAsync(customerRM);
        await _queryUnitOfWork.SaveAsync(default);
        _customerRMCacheService.Dirty();

    }

    public async Task HandleAsync(VehicleCreatedEvent @event)
    {
        var customerVehicleRM = new CustomerVehicleRM
        {
            CustomerId = @event.CustomerId,
            VehicleId = @event.VehicleId,
            Id = Guid.NewGuid(),
            VehicleCode = @event.Code.ToString(),
            VehicleName = @event.Name,
            VehiclePlate = @event.Plate
        };
        await _customerQueryRepository.AddAsync(customerVehicleRM);
        await _queryUnitOfWork.SaveAsync(default);
    }
}
