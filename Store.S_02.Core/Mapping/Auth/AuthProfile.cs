using AutoMapper;
using Store.S_02.Core.Dtos.Auth;
using Store.S_02.Core.Entities.Identity;

namespace Store.S_02.Core.Mapping.Auth;

public class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}