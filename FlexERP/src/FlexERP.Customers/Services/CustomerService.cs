using FlexERP.Customers.Models;
using FlexERP.Customers.Services.Abstractions;
using FlexERP.Data.Repositories.Abstractions;
using FlexERP.Shared.Models;
using FlexERP.WebApi.Enums;
using Serilog;

namespace FlexERP.Customers.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    
    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<ServiceResult<int>> CreateCustomerAsync(string name)
    {
        Log.Information("Creating customer with name {Name}", name);

        int customerId;
        try
        {
            customerId = await _customerRepository.CreateCustomerAsync(name);
        }
        catch (Exception)
        {
            Log.Error("Couldn't create customer with {Name}", name);
            return new ServiceResult<int>(ServiceErrorCode.GenericError);
        }

        Log.Information("Created customer with id {CustomerId}", customerId);

        return new ServiceResult<int>(customerId);
    }
    
    public async Task<ServiceResult<Customer>> GetCustomerAsync(int id)
    {
        Log.Information("Getting customer with id {Id}", id);

        Customer customer;
        try
        {
            var customerDao = await _customerRepository.GetCustomerAsync(id);
            customer = Customer.FromDao(customerDao);
        }
        catch (Exception)
        {
            Log.Warning("Couldn't get customer with {Id}", id);
            return new ServiceResult<Customer>(ServiceErrorCode.GenericError);
        }

        return new ServiceResult<Customer>(customer);
    }
}