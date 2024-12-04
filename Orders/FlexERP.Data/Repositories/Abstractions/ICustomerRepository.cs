using FlexERP.Shared.Abstractions;

namespace FlexERP.Data.Repositories.Abstractions;

public interface ICustomerRepository : IRepository
{
    Task<int> CreateCustomerAsync(string name);
    Task<Customer> GetCustomerAsync(int id);
}