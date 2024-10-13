using Microsoft.AspNetCore.Identity;

namespace Store.S_02.Core.Entities.Identity;

public class AppUser:IdentityUser
{
    public string DisplayName { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string Country { get; set; }
    public string AppuserId { get; set; } // FK
    public AppUser AppUser { get; set; }
}
