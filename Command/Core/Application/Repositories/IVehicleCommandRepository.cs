using Command.Core.Domain.Vehicle;

namespace Command.Core.Application.Repositories;
public interface IVehicleCommandRepository
{
    Task AddAsync(List<VehicleAggregate> vehicleAggregates);
    Task AddAsync(VehicleAggregate vehicleAggregate);
    Task<VehicleAggregate?> GetByIdAsync(Guid id);
    Task<int> GenerateNewVehicleCodeAsync();
    Task<List<int>> GenerateNewVehicleCodesAsync(int count);
}
