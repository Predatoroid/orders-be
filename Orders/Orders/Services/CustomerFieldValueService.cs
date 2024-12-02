namespace Orders.Services;

public class CustomFieldValueService
{
    private readonly string _connectionString;

    public CustomFieldValueService(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    // public async Task UpdateFieldValueAsync(int customFieldValueId, string newValue)
    // {
    //     using var connection = new SQLiteConnection(_connectionString);
    //
    //     // Fetch the current value
    //     var fieldValue = await connection.QueryFirstOrDefaultAsync<CustomFieldValue>(
    //         "SELECT * FROM CustomFieldValues WHERE Id = @Id", new { Id = customFieldValueId });
    //
    //     if (fieldValue == null)
    //         throw new Exception("Custom field value not found");
    //
    //     // Insert history
    //     var history = new CustomFieldHistory
    //     {
    //         CustomFieldValueId = fieldValue.Id,
    //         OldValue = fieldValue.Value,
    //         NewValue = newValue,
    //         Timestamp = DateTime.UtcNow
    //     };
    //
    //     await connection.ExecuteAsync(
    //         "INSERT INTO CustomFieldHistories (CustomFieldValueId, OldValue, NewValue, Timestamp) VALUES (@CustomFieldValueId, @OldValue, @NewValue, @Timestamp)",
    //         history);
    //
    //     // Update value
    //     await connection.ExecuteAsync(
    //         "UPDATE CustomFieldValues SET Value = @Value WHERE Id = @Id",
    //         new { Value = newValue, Id = customFieldValueId });
    // }
}
