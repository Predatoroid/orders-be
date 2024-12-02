using System.Data;
using Dapper;
using Orders.Abstractions;
using Orders.Enums;

namespace Orders.Repositories;

public class CustomerFieldRepository : ICustomerFieldRepository
{
    private readonly IDbConnection _dbConnection;

    public CustomerFieldRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<int> CreateCustomerFieldAsync(FieldTypeEnum fieldTypeId, string description)
    {
        const string query = "INSERT INTO CustomerFields (FieldType, Description) VALUES (@FieldType, @Description) RETURNING Id";
        return await _dbConnection.ExecuteScalarAsync<int>(query, new { FieldType = fieldTypeId, Description = description });
    }
    
    // public async Task<Customer> GetCustomerAsync(int id)
    // {
    //     const string query = "SELECT * FROM Customers WHERE Id = @Id";
    //     return await _dbConnection.QuerySingleAsync<Customer>(query, new { Id = id });
    // }

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