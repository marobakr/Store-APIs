using Store.S_02.Core.Dtos.Auth;

namespace Store.S_02.Core.Services.Contract;

public interface IUserService
{
    Task<UserDto>  LoginAsync (LoginDto loginDto);
    Task<UserDto>  RegisterAsync (RegisterDto registerDto);
    
    Task<bool> CheckEmailExistAsync(string email);

}