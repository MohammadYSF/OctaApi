using Application.Common;
using Domain.Customer.Events;
using Domain.Vehicle;
using OctaApi.Application.Repositories;
namespace Application.EventHandlers.Vehicle;

public class VehicleAggregateEventHandler : IEventHandler<VehicleAddedToCustomerEvent>
{
    private readonly IVehicleCommandRepository _vehicleRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    public VehicleAggregateEventHandler(IVehicleCommandRepository vehicleRepository, ICommandUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(VehicleAddedToCustomerEvent @event, CancellationToken cancellationToken)
    {
        int code = await _vehicleRepository.GenerateNewVehicleCodeAsync();
        VehicleAggregate vehicleAggregate = VehicleAggregate.Create(@event.VehicleId, code, @event.VehicleName, @event.VehiclePlate, @event.VehicleColor);
        await _vehicleRepository.AddAsync(vehicleAggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}
