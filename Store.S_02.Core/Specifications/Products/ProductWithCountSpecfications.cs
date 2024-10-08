namespace Store.S_02.Core.Specifications.Products;

public class ProductWithCountSpecfications : BaseSpecifications<Entities.Products, int>
{
    public ProductWithCountSpecfications(ProductSpecParams productSpecParams) : base(P =>
        (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))
        &&
        (!productSpecParams.BrandId.HasValue || productSpecParams.BrandId == P.BrandId)
        &&
        (!productSpecParams.TypeId.HasValue || productSpecParams.TypeId == P.TypeId))
    {
    }
}