using Microsoft.AspNetCore.Identity;
using Store.S_02.Core.Entities.Identity;

namespace Store.S_02.Repository.Identity;

public class StoreIdentityDbContextSeed
{
    public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
    {
        if (_userManager.Users.Count() == 0)
        {
            var user = new AppUser()
            {
                Email = "dev@marobakr.com",
                DisplayName = "marwan",
                PhoneNumber = "0111111111",
                Address = new Address()
                {
                    FirstName = "maro",
                    LastName = "dev",
                    Street = "dev street",
                    Country = "Egypt"
                }
            };
           await  _userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}