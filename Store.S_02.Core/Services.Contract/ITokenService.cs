using Microsoft.AspNetCore.Identity;
using Store.S_02.Core.Entities.Identity;

namespace Store.S_02.Core.Services.Contract;

public interface ITokenService
{
  Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
}