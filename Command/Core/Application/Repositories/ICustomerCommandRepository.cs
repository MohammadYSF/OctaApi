using Command.Core.Domain.Customer;

namespace Command.Core.Application.Repositories;
public interface ICustomerCommandRepository
{
    Task AddAsync(CustomerAggregate customerAggregate);
    Task<int> GenerateNewCustomerCodeAsync();
}
