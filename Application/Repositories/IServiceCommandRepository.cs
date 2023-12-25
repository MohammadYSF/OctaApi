using Domain.Service;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Repositories
{
    public interface IServiceCommandRepository
    {
        Task<List<Service>> GetAllAsync();
        //Task AddAsync(Service entity);
        Task AddAsync(ServiceAggregate serviceAggregate);
        Task UpdateAsync(ServiceAggregate serviceAggregate);
        //void Update(Service entity);
        void Delete(Service entity);
        Task<int> GenerateNewCodeAsync();
        //Task<int> GetNewCodeAsync();
        //Task<Service?> GetByCode(int code);
        Task<ServiceAggregate?> GetByCodeAsync(int code);
        //Task<Service?> GetByIdAsync(Guid id);
        Task<ServiceAggregate?> GetByIdAsync(Guid id);
    }
}
