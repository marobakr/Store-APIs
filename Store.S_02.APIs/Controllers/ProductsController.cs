using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.S_02.Core.Services.Contract;

namespace Store.S_02.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController( IProductService productService)
        {
            _productService = productService;
        }

        /* === === === === === === Get: BaseUrl/api/Products === === === === === ===  */
        [HttpGet] 
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProdcutsAsync();
            return Ok(products); //* return 200 status codes
        }

        /* === === === === === === Get: BaseUrl/api/Products/brand === === === === === ===  */
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }
        
        /* === === === === === === Get: BaseUrl/api/Products/types === === === === === ===  */
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes() {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }
        
        /* === === === === === === Get: BaseUrl/api/Products:id === === === === === ===  */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null) return BadRequest("Id is null");
            var product = await _productService.GetProductById(id.Value);
            return Ok(product);
        }

        


    }
}
