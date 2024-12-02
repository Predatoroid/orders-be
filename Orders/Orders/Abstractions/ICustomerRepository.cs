using Orders.Models;

namespace Orders.Abstractions;

public interface ICustomerRepository
{
    Task<int> CreateCustomerAsync(string name);
    Task<Customer> GetCustomerAsync(int id);
}