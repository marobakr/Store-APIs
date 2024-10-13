using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.S_02.APIs.Attributes;
using Store.S_02.APIs.Error;
using Store.S_02.Core.Dtos.Products;
using Store.S_02.Core.Helper;
using Store.S_02.Core.Services.Contract;
using Store.S_02.Core.Specifications.Products;

namespace Store.S_02.APIs.Controllers
{
 
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController( IProductService productService)
        {
            _productService = productService;
        }

        /* === === === === === === Get: BaseUrl/api/Products === === === === === ===  */
        [ProducesResponseType(typeof(PaginationsResponse<ProductDto>),StatusCodes.Status200OK)]
        [HttpGet] 
        [Cached(100)]
        public async Task<ActionResult <PaginationsResponse<ProductDto>> > GetAllProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var products = await _productService.GetAllProdcutsAsync(productSpecParams);
            return Ok(products); //* return 200 status codes
        }

        /* === === === === === === Get: BaseUrl/api/Products/brand === === === === === ===  */
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>),StatusCodes.Status200OK)]
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }
        
        /* === === === === === === Get: BaseUrl/api/Products/types === === === === === ===  */
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>),StatusCodes.Status200OK)]
        [HttpGet("types")]
        public async Task<ActionResult <IEnumerable<ProductDto>>> GetAllTypes() {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }
        
        /* === === === === === === Get: BaseUrl/api/Products:id === === === === === ===  */
        [ProducesResponseType(typeof(TypeBrandDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APiErrorResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APiErrorResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null) return BadRequest(new APiErrorResponse(400));
            var product = await _productService.GetProductById(id.Value);
            
            if(product is null) return NotFound(new APiErrorResponse(404));
            return Ok(product);
        }

        


    }
}
