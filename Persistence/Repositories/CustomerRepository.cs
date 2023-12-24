using Application.Repositories.Command;
using Microsoft.EntityFrameworkCore;
using OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;
using OctaApi.Domain.Models;
using OctaApi.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Persistence.Repositories
{
    public class CustomerRepository : ICustomerCommandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Customer entity)
        {
            await _dbContext.Customers.AddAsync(entity);
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<GetCustomersMinimal_DTO>> Get()
        //{
        //    var data = await _dbContext.Customers.Select(a => new GetCustomersMinimal_DTO(a.Id, a.Code, a.FirstName + " " + a.LastName)).ToListAsync();
        //    return data;
        //}

        public async Task<List<Customer>>GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> GenerateNewCustomerCodeAsync()
        {
            if (_dbContext.Customers.Count() == 0)
            {
                return 1;
            }
            return await _dbContext.Customers.Select(a => a.Code).MaxAsync() + 1;
        }

        public async Task<int> GetNewVehicleCode()
        {
            if (_dbContext.Vehicles.Count() == 0)
            {
                return 1;
            }
            return await _dbContext.Vehicles.Select(a => a.Code).MaxAsync() + 1;

        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
