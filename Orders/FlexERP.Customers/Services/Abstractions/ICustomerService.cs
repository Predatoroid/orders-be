using FlexERP.Customers.Models;
using FlexERP.Shared.Abstractions;
using FlexERP.Shared.Models;

namespace FlexERP.Customers.Services.Abstractions;

public interface ICustomerService : IService
{
    Task<ServiceResult<int>> CreateCustomerAsync(string name);
    Task<ServiceResult<Customer>> GetCustomerAsync(int id);
}