using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.S_02.Core.Dtos.Products;

namespace Store.S_02.Core.Mapping.Products;

public class PictureUrlResolver: IValueResolver<Entities.Products,ProductDto,string>
{
    private readonly IConfiguration _configuration;

    public PictureUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
    public string Resolve(Entities.Products source, ProductDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return $"{_configuration["BASEURL"]}{source.PictureUrl}";
        }
        else return string.Empty;
    }

    
}