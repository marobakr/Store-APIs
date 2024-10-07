using Store.S_02.Core.Dtos.Products;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Helper;
using Store.S_02.Core.Specifications.Products;

namespace Store.S_02.Core.Services.Contract;

public interface IProductService
{
   Task<PaginationsResponse<ProductDto>> GetAllProdcutsAsync(ProductSpecParams productSpecParams);
   Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
   Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync(); 
   Task<ProductDto> GetProductById(int id);


}