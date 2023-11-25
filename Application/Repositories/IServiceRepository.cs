using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Repositories
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync();
        Task AddAsync(Service entity);
        void Update(Service entity);
        void Delete(Service entity);
        Task<int> GetNewCodeAsync();
        Task<Service?> GetByCode(int code);
        Task<Service?> GetByIdAsync(Guid id);
    }
}
