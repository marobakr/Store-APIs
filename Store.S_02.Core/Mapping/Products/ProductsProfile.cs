using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.S_02.Core.Dtos.Products;
using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Mapping.Products;

public class ProductsProfile: Profile
{
    public ProductsProfile(IConfiguration configuration)
    {
        CreateMap<Entities.Products, ProductDto>()
            .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Type.Name))
            
            /* This is the old way asd*/ 
            
            // .ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"{configuration["BASEURL"]}{s.PictureUrl}")); 
            
            /* This is the new way */
            .ForMember(d => d.PictureUrl, o => o.MapFrom(new PictureUrlResolver (configuration)));

        CreateMap<ProductBrand, TypeBrandDto>();
        CreateMap<ProductType, TypeBrandDto>();
    }
}