using Command.Core.Application.Repositories;
using Command.Core.Common;
using Command.Core.Domain.Customer.Events;
using Command.Core.Domain.Vehicle;
namespace Command.Core.Application.EventHandlers.Vehicle;

public class VehicleAggregateEventHandler : IEventHandler<VehicleAddedToCustomerEvent>
{
    private readonly IVehicleCommandRepository _vehicleRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    public VehicleAggregateEventHandler(IVehicleCommandRepository vehicleRepository, ICommandUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task HandleAsync(VehicleAddedToCustomerEvent @event)
    {
        int code = await _vehicleRepository.GenerateNewVehicleCodeAsync();
        VehicleAggregate vehicleAggregate = VehicleAggregate.Create(@event.VehicleId, code, @event.VehicleName, @event.VehiclePlate, @event.VehicleColor);
        await _vehicleRepository.AddAsync(vehicleAggregate);
        await _unitOfWork.SaveAsync(default);
    }
}
