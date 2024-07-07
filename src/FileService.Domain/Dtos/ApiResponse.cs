namespace FileService.Domain.DTOs;

public class ApiResponse<T>
{
    public bool Succeed { get; set; } = true;
    public int ResponseCode { get; set; } = 1000;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public ApiResponse()
    {
        
    }
    /// <summary>
    /// Incase of succeesfull request
    /// </summary>
    /// <param name="data"></param>
    public ApiResponse(T data)
    {
        Data = data;
    }
    /// <summary>
    /// Incase of unsuccessfull request
    /// </summary>
    public ApiResponse(int responseCode, string message)
    {
        Succeed = false;
        ResponseCode = responseCode;
        Message = message;
    }
    public ApiResponse(int responseCode, string message, T? data = default(T?))
    {
        Succeed = false;
        ResponseCode = responseCode;
        Message = message;
        Data = data;
    }
}
