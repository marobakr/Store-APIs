using AutoMapper;
using Store.S_02.Core.Dtos.Basket;
using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Mapping.Basket;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
    }
}