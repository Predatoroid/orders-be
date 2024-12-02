using Orders.Models;

namespace Orders.Abstractions;

public interface ICustomerService : IService
{
    Task<ServiceResult<int>> CreateCustomerAsync(string name);
    Task<ServiceResult<Customer>> GetCustomerAsync(int id);
}