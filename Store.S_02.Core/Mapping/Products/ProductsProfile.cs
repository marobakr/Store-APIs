using AutoMapper;
using Store.S_02.Core.Dtos.Products;
using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Mapping.Products;

public class ProductsProfile: Profile
{
    public ProductsProfile()
    {
        CreateMap<Entities.Products, ProductDto>()
            .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Type.Name));

        CreateMap<ProductBrand, TypeBrandDto>();
        CreateMap<ProductType, TypeBrandDto>();
    }
}