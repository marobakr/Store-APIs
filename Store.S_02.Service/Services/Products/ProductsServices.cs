using AutoMapper;
using Store.S_02.Core;
using Store.S_02.Core.Dtos.Products;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Services.Contract;
using Store.S_02.Core.Specifications;
using Store.S_02.Core.Specifications.Products;


namespace Store.S_02.Service.Services.Products;

public class ProductsServices:IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsServices( IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductDto>> GetAllProdcutsAsync()
    {
        var spec = new productsSpecifications();
        var products = await _unitOfWork.Repository<Core.Entities.Products, int>().GetAllWithSpecAsync(spec);
        var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);
        
        return mappedProducts;
    }

    public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
    {
     return  _mapper.Map <IEnumerable <TypeBrandDto>>( await _unitOfWork.Repository<ProductType, int>().GetAllAsync());
    }


    public async Task<ProductDto> GetProductById(int id)
    {
        var spec = new productsSpecifications();
      var product = await  _unitOfWork.Repository<Core.Entities.Products,int>().GetWithSpecAsync(spec);
      var mapperProduct = _mapper.Map<ProductDto>(product);
      return mapperProduct;
    }
    
    
    public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
    {

       var brands = await _unitOfWork.Repository<ProductBrand,int>().GetAllAsync();
        var mappedBrands = _mapper.Map<IEnumerable<TypeBrandDto>>(brands);
        return mappedBrands;
    }

  
}