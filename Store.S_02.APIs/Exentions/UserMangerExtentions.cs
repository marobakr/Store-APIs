using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.S_02.Core.Entities.Identity;

namespace Store.S_02.APIs.Exentions;

public static class UserMangerExtentions
{
    public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
    {
        var userEmail = user.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return null;

        var userResult = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(U => U.Email == userEmail);
        if (userResult is null) return null;

        return userResult;
    }
}