﻿using Domain.Service;
using Microsoft.EntityFrameworkCore;
using OctaApi.Application.Repositories;
using OctaApi.Persistence.Contexts;
namespace Persistence.Repositories;

public class ServiceCommandRepository : IServiceCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public ServiceCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    public async Task AddAsync(ServiceAggregate serviceAggregate)
    {
        await _writeDbContext.Services.AddAsync(serviceAggregate);

    }

    public async Task<int> GenerateNewCodeAsync()
    {
        List<int> usedCodes = await _writeDbContext.Services.Select(a => a.Code.Value).ToListAsync();
        int min = usedCodes.Min();
        int max = usedCodes.Max();
        List<int> unUsedCodes = Enumerable.Range(min, max).Where(a => !usedCodes.Contains(a)).ToList();
        if (unUsedCodes.Count > 0) return unUsedCodes[0];
        return max + 1;
    }

    public async Task<ServiceAggregate?> GetByCodeAsync(int code)
    {
        return await _writeDbContext.Services.FirstOrDefaultAsync(a => a.Code.Value == code);
    }

    public async Task<ServiceAggregate?> GetByIdAsync(Guid id)
    {
        return await _writeDbContext.Services.FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task UpdateAsync(ServiceAggregate serviceAggregate)
    {
        _writeDbContext.Services.Update(serviceAggregate);
        return Task.CompletedTask;
    }
}
