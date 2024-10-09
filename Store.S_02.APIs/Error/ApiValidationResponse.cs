namespace Store.S_02.APIs.Error;

public class ApiValidationResponse: APiErrorResponse
{
    public IEnumerable<string> Erros { get; set; } = new List<string>();
    
    public ApiValidationResponse() : base(400)
    {
            
    }
}