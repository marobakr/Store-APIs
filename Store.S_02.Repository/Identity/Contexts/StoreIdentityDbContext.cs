using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.S_02.Core.Entities.Identity;

namespace Store.S_02.Repository.Identity.Contexts;

public class StoreIdentityDbContext:IdentityDbContext<AppUser>
{
    public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> option ) : base(option)
    {
        
    }
}