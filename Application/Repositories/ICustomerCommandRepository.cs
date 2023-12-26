using Domain.Customer;
using OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;
namespace Application.Repositories;
public interface ICustomerCommandRepository
{
    Task AddAsync(CustomerAggregate customerAggregate);
    Task<int> GenerateNewCustomerCodeAsync();
}
