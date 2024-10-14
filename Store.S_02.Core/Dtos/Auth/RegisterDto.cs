using System.ComponentModel.DataAnnotations;

namespace Store.S_02.Core.Dtos.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "DisplayName is Required")]

    public string DisplayName { get; set; }

    [Required(ErrorMessage = "PhoneNumber is Required")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; }
}