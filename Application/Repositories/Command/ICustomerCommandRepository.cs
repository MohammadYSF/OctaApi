using Domain.Customer;
using OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Command
{
    public interface ICustomerCommandRepository
    {
        //Task<List<GetCustomersMinimal_DTO>> Get();
        Task AddAsync(CustomerAggregate customerAggregate);
        Task<CustomerAggregate?> GetByIdAsync(Guid id);

        //Task<Customer?> GetByIdAsync(Guid id);
        //Task AddAsync(Customer entity);
        //Task<List<Customer>> GetAllAsync();
        Task DeleteAsync(CustomerAggregate customerAggregate);
        //void Delete(Customer entity);
        Task UpdateAsync(CustomerAggregate customerAggregate);
        //void Update(Customer entity);
        //Task<int> GetNewVehicleCode();
        Task<int> GenerateNewCustomerCodeAsync();
    }
}
