namespace Store.S_02.APIs.Error;

public class APiErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public APiErrorResponse(int statusCode , string? message =  null)
    {
        StatusCode = statusCode;
        Message = message ??  GetDefaultMessageForStatusCode(statusCode);
    }
    
    private string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
            _ => null
        };
    }
}