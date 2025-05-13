namespace Shared.Exceptions;
  
    
public class ApiErrorResponse
{
    public ApiErrorResponse(string title,
        int status,
        string detail,
        string instance,
        ErrorCode errorCode,
        Dictionary<string, string[]> anyValue = null)
    {
        Title = title;
        Status = status;
        Detail = detail;
        ErrorCode = errorCode;
        AnyValue = anyValue;
        Instance = instance;
    }

    public string Title { get; set; }
        
    public int Status { get; set; } 
        
    public string Detail { get; set; }
        
    public string Instance { get; set; }
        
    public ErrorCode ErrorCode { get; set; }
        
    public IDictionary<string, string[]> AnyValue { get; set; }
        
        
}