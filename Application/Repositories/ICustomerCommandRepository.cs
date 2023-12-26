using Domain.Customer;
namespace Application.Repositories;
public interface ICustomerCommandRepository
{
    Task AddAsync(CustomerAggregate customerAggregate);
    Task<int> GenerateNewCustomerCodeAsync();
}
