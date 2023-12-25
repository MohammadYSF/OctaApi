using Domain.Vehicle;
using OctaApi.Application.Features.VehicleFeatures.GetVehiclesMinimal;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Repositories
{
    public interface IVehicleCommandRepository
    {
        //Task<List<Vehicle>> GetAllAsync();
        //Task<List<GetVehiclesMinimal_DTO>> Get();
        Task AddAsync(List<VehicleAggregate> vehicleAggregates);
        Task AddAsync(VehicleAggregate vehicleAggregate);
        Task<VehicleAggregate> GetByIdAsync(Guid id);
        Task<int> GenerateNewVehicleCodeAsync();
        Task<List<int>> GenerateNewVehicleCodesAsync(int count);

    }
}
