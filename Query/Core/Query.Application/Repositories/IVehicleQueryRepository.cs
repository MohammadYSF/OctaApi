﻿using OctaShared.ReadModels;

namespace Query.Application.Repositories;
public interface IVehicleQueryRepository
{
    Task AddAsync(VehicleRM vehicleRM);
    Task<List<VehicleRM>> GetAsync();
    Task<VehicleRM?> GetByVehicleIdAsync(Guid vehicleId);
    Task<VehicleRM?> GetByVehicleCodeAsync(string code);
    Task CheckCacheAsync();

}
