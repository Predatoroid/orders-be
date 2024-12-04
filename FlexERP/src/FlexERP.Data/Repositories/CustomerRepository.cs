using System.Data;
using Dapper;
using FlexERP.Data.DAOs;
using FlexERP.Data.Repositories.Abstractions;

namespace FlexERP.WebApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnection _dbConnection;

    public CustomerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<int> CreateCustomerAsync(string name)
    {
        const string query = "INSERT INTO Customers (Name) VALUES (@Name) RETURNING Id";
        return await _dbConnection.ExecuteScalarAsync<int>(query, new { Name = name });
    }
    
    public async Task<CustomerDao> GetCustomerAsync(int id)
    {
        const string query = "SELECT * FROM Customers WHERE Id = @Id";
        return await _dbConnection.QuerySingleAsync<CustomerDao>(query, new { Id = id });
    }
}