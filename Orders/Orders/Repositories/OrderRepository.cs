using System.Data;
using Dapper;
using Orders.Abstractions;
using Orders.Enums;
using Orders.Models;

namespace Orders.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnection _dbConnection;

    public OrderRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<Order> GetOrderAsync(int id)
    {
        //For project purposes, we return directly a dummy Order
        return Task.FromResult(new Order(1, new Money(CurrencyEnum.EUR, 340m)));
    }
}