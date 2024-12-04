using FlexERP.Data.DAOs;

namespace FlexERP.Data.Repositories.Abstractions;

public interface ICustomerRepository
{
    Task<int> CreateCustomerAsync(string name);
    Task<CustomerDao> GetCustomerAsync(int id);
}