using FlexERP.Customers.Models;
using FlexERP.Shared.Models;

namespace FlexERP.Customers.Services.Abstractions;

public interface ICustomerService
{
    Task<ServiceResult<int>> CreateCustomerAsync(string name);
    Task<ServiceResult<Customer>> GetCustomerAsync(int id);
}