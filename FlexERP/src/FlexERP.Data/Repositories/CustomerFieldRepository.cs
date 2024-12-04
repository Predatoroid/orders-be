using System.Data;
using Dapper;
using FlexERP.Data.DAOs;
using FlexERP.Data.Repositories.Abstractions;
using FlexERP.Shared.Enums;
using FlexERP.WebApi.Enums;

namespace FlexERP.Data.Repositories;

public class CustomerFieldRepository : ICustomerFieldRepository
{
    private readonly IDbConnection _dbConnection;

    public CustomerFieldRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<int> CreateCustomerFieldAsync(int customerId, FieldTypeEnum fieldTypeId, string description)
    {
        const string query = "INSERT INTO CustomerFields (CustomerId, FieldTypeId, Description) VALUES (@CustomerId, @FieldTypeId, @Description) RETURNING Id";
        return await _dbConnection.ExecuteScalarAsync<int>(query, new { CustomerId = customerId, FieldTypeId = fieldTypeId, Description = description });
    }
    
    public async Task<CustomerFieldDao> GetCustomerFieldAsync(int id)
    {
        const string query = "SELECT * FROM CustomerFields WHERE Id = @Id";
        return await _dbConnection.QuerySingleAsync<CustomerFieldDao>(query, new { Id = id });
    }
    
    public async Task<IEnumerable<CustomerFieldHistoryDao>> GetCustomerFieldHistoryAsync(int id)
    {
        const string query = "SELECT * FROM CustomerFieldHistory WHERE Id = @Id";
        return await _dbConnection.QueryAsync<CustomerFieldHistoryDao>(query, new { Id = id });
    }
    
    public async Task<int> CreateCustomerFieldOptionAsync(int customerFieldId, string optionValue)
    {
        const string query = "INSERT INTO FieldOptions (CustomerFieldId, OptionValue) VALUES (@CustomerFieldId, @OptionValue) RETURNING Id";
        return await _dbConnection.ExecuteScalarAsync<int>(query, new { CustomerFieldId = customerFieldId, OptionValue = optionValue });
    }
    
    public async Task<int> CreateCustomerFieldValueAsync(int customerFieldId, int fieldOptionId)
    {
        const string query = "INSERT INTO CustomerFieldValues (CustomerFieldId, FieldOptionId) VALUES (@CustomerFieldId, @FieldOptionId) RETURNING Id";
        return await _dbConnection.ExecuteScalarAsync<int>(query, new { CustomerFieldId = customerFieldId, FieldOptionId = fieldOptionId });
    }

    public async Task UpdateCustomerFieldAsync(int customerFieldId, string description)
    {
        const string query = "UPDATE CustomerFields SET description = @Description WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = customerFieldId, Description = description });
    }

    public Task UpdateCustomerFieldOptionAsync(int fieldOptionId, string optionValue)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateCustomerFieldHistoryAsync(int entityId, EntityTypeEnum entityTypeId, string oldValue, string newValue)
    {
        const string query = "INSERT INTO CustomerFieldHistory (EntityId, EntityTypeId, OldValue, NewValue) VALUES (@EntityId, @EntityTypeId, @OldValue, @NewValue) RETURNING Id";
        return await _dbConnection.ExecuteScalarAsync<int>(query, new { EntityId = entityId, EntityTypeId = entityTypeId, OldValue = oldValue, NewValue = newValue });
    }

    // public async Task<int> CreateCustomFieldAsync(int customerId, string fieldType, string fieldName)
    // {
    //     var query = "INSERT INTO CustomerCustomFields (CustomerId, FieldName, FieldType) VALUES (@CustomerId, @FieldName, @FieldType) RETURNING Id";
    //     return await _dbConnection.ExecuteScalarAsync<int>(query, new { CustomerId = customerId, FieldName = fieldName, FieldType = fieldType });
    // }

    // public async Task CreateCustomFieldValueAsync(int fieldId, string fieldValue, int? optionId = null)
    // {
    //     var field = await _dbConnection.QueryFirstOrDefaultAsync<CustomerCustomField>("SELECT * FROM CustomerCustomFields WHERE Id = @FieldId", new { FieldId = fieldId });
    //     
    //     if (field == null) throw new Exception("Field not found.");
    //
    //     // Αν είναι dropdown, ο πελάτης πρέπει να επιλέξει από τα διαθέσιμα options
    //     if (field.FieldType == "dropdown" && optionId.HasValue)
    //     {
    //         var option = await _dbConnection.QueryFirstOrDefaultAsync<DropdownOption>("SELECT * FROM DropdownOptions WHERE Id = @OptionId", new { OptionId = optionId });
    //
    //         if (option == null) throw new Exception("Option not found.");
    //
    //         fieldValue = option.OptionValue; // Αποθηκεύουμε την επιλογή του πελάτη
    //     }
    //     
    //     var query = "INSERT INTO CustomerCustomFieldValues (CustomFieldId, FieldValue) VALUES (@CustomFieldId, @FieldValue)";
    //     await _dbConnection.ExecuteAsync(query, new { CustomFieldId = fieldId, FieldValue = fieldValue });
    // }
    //
    // public async Task<IEnumerable<CustomerCustomField>> GetCustomFieldsForCustomerAsync(int customerId)
    // {
    //     var query = "SELECT * FROM CustomerCustomFields WHERE CustomerId = @CustomerId";
    //     return await _dbConnection.QueryAsync<CustomerCustomField>(query, new { CustomerId = customerId });
    // }
    //
    // public async Task<IEnumerable<DropdownOption>> GetDropdownOptionsAsync(int fieldId)
    // {
    //     var query = "SELECT * FROM DropdownOptions WHERE CustomFieldId = @CustomFieldId";
    //     return await _dbConnection.QueryAsync<DropdownOption>(query, new { CustomFieldId = fieldId });
    // }
}