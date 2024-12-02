using Orders.Enums;

namespace Orders.Models;

public class ServiceResult<TValue>
{
    /// <summary>
    /// The actual value of the result when it's successful
    /// </summary>
    public TValue? Value { get; }

    /// <summary>
    /// The error code when the service result has failed
    /// </summary>
    public ServiceErrorCode? Error { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool Success => Error == null;
    
    public ServiceResult(TValue value)
    {
        Value = value;
        Error = null;
    }
    
    public ServiceResult(ServiceErrorCode errorCode)
    {
        Error  = errorCode;
    }
}