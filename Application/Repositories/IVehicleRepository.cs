using OctaApi.Application.Features.VehicleFeatures.GetVehiclesMinimal;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Repositories
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync();
        //Task<List<GetVehiclesMinimal_DTO>> Get();
        Task<Vehicle?> GetByIdAsync(Guid id);
    }
}
