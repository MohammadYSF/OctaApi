
using Query.Application.ReadModels;

namespace Query.Application.Repositories;
public interface IVehicleQueryRepository
{
    Task<List<VehicleRM>> GetAsync();
    Task<VehicleRM?> GetByVehicleIdAsync(Guid vehicleId);
    Task<VehicleRM?> GetByVehicleCodeAsync(string code);
}
