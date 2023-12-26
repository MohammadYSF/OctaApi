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

    public CustomerEventHandler(ICustomerQueryRepository customerQueryRepository, IQueryUnitOfWork queryUnitOfWork)
    {
        _customerQueryRepository = customerQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
    }

    public async Task HandleAsync(CustomerCreatedEvent @event, CancellationToken cancellationToken)
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
        await _queryUnitOfWork.SaveAsync(cancellationToken);

    }

    public async Task HandleAsync(VehicleCreatedEvent @event, CancellationToken cancellationToken)
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
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }
}
