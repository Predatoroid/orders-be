using System.Data;
using FlexERP.Data.DAOs;
using FlexERP.Data.Repositories.Abstractions;
using FlexERP.Shared.Enums;

namespace FlexERP.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnection _dbConnection;

    public OrderRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<OrderDao> GetOrderAsync(int id)
    {
        //For project purposes, we return directly a dummy OrderDao
        return Task.FromResult(new OrderDao
        {
            Id = 1,
            Currency = 1,
            Value = 340m
        });
    }
}