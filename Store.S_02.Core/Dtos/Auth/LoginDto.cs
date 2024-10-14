using System.ComponentModel.DataAnnotations;

namespace Store.S_02.Core.Dtos.Auth;

public class LoginDto
{
    [Required (ErrorMessage = "Email is Required")]
    [EmailAddress]
    public string Email { get; set; }
    [Required (ErrorMessage = "Email is Required")]
    public string Password { get; set; }
}