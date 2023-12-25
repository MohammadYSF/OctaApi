using Application.ReadModels;

namespace Application.Repositories.Query;
public interface IVehicleQueryRepository
{
    Task<List<VehicleRM>> GetAsync();
    Task<VehicleRM?> GetByVehicleIdAsync(Guid vehicleId);
}
