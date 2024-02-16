using Command.Core.Application.Repositories;
using Command.Core.Domain.Vehicle;
using Command.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Command.Infrastructure.Persistence.Repositories;
public class VehicleCommandRepository : IVehicleCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public VehicleCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task AddAsync(List<VehicleAggregate> vehicleAggregates)
    {
        await _writeDbContext.Vehicles.AddRangeAsync(vehicleAggregates);
    }

    public async Task AddAsync(VehicleAggregate vehicleAggregate)
    {
        await _writeDbContext.Vehicles.AddAsync(vehicleAggregate);
    }

    public async Task<int> GenerateNewVehicleCodeAsync()
    {
        List<int> usedCodes = await _writeDbContext.Vehicles.Select(a => a.Code.Value).ToListAsync();
        if (usedCodes.Count == 0) return 1;
        int min = usedCodes.Min();
        int max = usedCodes.Max();
        List<int> unUsedCodes = Enumerable.Range(min, max).Where(a => !usedCodes.Contains(a)).ToList();
        if (unUsedCodes.Count > 0) return unUsedCodes[0];
        return max + 1;
    }

    public async Task<List<int>> GenerateNewVehicleCodesAsync(int count)
    {
        List<int> answer = new();
        List<int> usedCodes = await _writeDbContext.Vehicles.Select(a => a.Code.Value).ToListAsync();
        int min = usedCodes.Min();
        int max = usedCodes.Max();
        List<int> unUsedCodes = Enumerable.Range(min, max).Where(a => !usedCodes.Contains(a)).ToList();
        int i = 0;
        foreach (var unUsedCode in unUsedCodes)
        {
            if (i < count)
            {
                answer.Add(unUsedCode);
                i++;
            }
        }
        int j = count - i;
        int temp = max;
        for (; j > 0; j--)
        {
            temp++;
            answer.Add(temp);
        }
        return answer;
    }

    public async Task<VehicleAggregate?> GetByIdAsync(Guid id)
    {
        return await _writeDbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
    }
}
