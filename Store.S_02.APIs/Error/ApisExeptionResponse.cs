namespace Store.S_02.APIs.Error;

public class ApisExeptionResponse:APiErrorResponse
{
    public string? Details { get; set; }


    public ApisExeptionResponse(int statusCode , string? message = null , string? details= null):base(statusCode , message)
    {
        Details = details;
    }

}