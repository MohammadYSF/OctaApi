﻿using Application.ReadModels;

namespace Query.Application.Repositories;
public interface ICustomerQueryRepository
{
    Task AddAsync(CustomerVehicleRM customerVehicleRM);
    Task AddAsync(CustomerRM customerRM);
    Task UpdateAsync(CustomerRM customerRM);
    Task<List<CustomerRM>> GetAsync();
    Task<CustomerRM?> GetByCustomerIdAsync(Guid customerId);
}