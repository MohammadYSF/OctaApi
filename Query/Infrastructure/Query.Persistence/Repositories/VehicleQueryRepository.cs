using Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Query.Application.Repositories;
using Query.Persistence.Contexts;

namespace Query.Persistence.Repositories;
public class VehicleQueryRepository : IVehicleQueryRepository
{
    private readonly QueryDbContext _queryDbContext;
    public VehicleQueryRepository(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
    }

    public async Task<List<VehicleRM>> GetAsync()
    {
        return await _queryDbContext.VehicleRMs.ToListAsync();
    }

    public async Task<VehicleRM?> GetByVehicleIdAsync(Guid vehicleId)
    {
        return await _queryDbContext.VehicleRMs.FirstOrDefaultAsync(a => a.VehicleId == vehicleId);
    }
}
