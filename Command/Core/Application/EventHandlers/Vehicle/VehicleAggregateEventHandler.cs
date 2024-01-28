using Command.Core.Application.Repositories;
using Command.Core.Domain.Vehicle;
using OctaShared.Contracts;
using OctaShared.Events;
namespace Command.Core.Application.EventHandlers.Vehicle;

public class VehicleAggregateEventHandler : IEventHandler<VehicleAddedToCustomerEvent>
{
    private readonly IVehicleCommandRepository _vehicleRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public VehicleAggregateEventHandler(IVehicleCommandRepository vehicleRepository, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }


    public async Task HandleAsync(VehicleAddedToCustomerEvent @event)
    {
        int code = await _vehicleRepository.GenerateNewVehicleCodeAsync();
        VehicleAggregate vehicleAggregate = VehicleAggregate.Create(@event.VehicleId, code, @event.VehicleName, @event.VehiclePlate, @event.VehicleColor, @event.CustomerId);
        await _vehicleRepository.AddAsync(vehicleAggregate);
        await _unitOfWork.SaveAsync(default);
        foreach (var item in vehicleAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
    }
}
    