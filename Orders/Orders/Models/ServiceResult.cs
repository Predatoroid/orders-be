using Orders.Enums;

namespace Orders.Models;

public class ServiceResult<TValue>
{
    /// <summary>
    /// The actual value of the result when it's successful
    /// </summary>
    public TValue? Value { get; set;  }

    /// <summary>
    /// The error code when the service result has failed
    /// </summary>
    private ServiceErrorCode? Error { get; set; }
    
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