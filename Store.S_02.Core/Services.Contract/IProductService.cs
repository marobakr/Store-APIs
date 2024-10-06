using Store.S_02.Core.Dtos.Products;
using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Services.Contract;

public interface IProductService
{
   Task<IEnumerable<ProductDto>> GetAllProdcutsAsync();
   Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
   Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
   Task<ProductDto> GetProductById(int id);


}