namespace Store.S_02.Core.Specifications.Products;

public class productsSpecifications : BaseSpecifications<Entities.Products, int>
{
    public productsSpecifications(int id) : base(P => P.Id == id)
    {
        ApplyIncludes();
    }

    public productsSpecifications(ProductSpecParams productSpecParams) : base(P =>
        (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))
        &&
        (! productSpecParams.BrandId.HasValue || productSpecParams.BrandId == P.BrandId)
        && 
        (! productSpecParams.TypeId.HasValue || productSpecParams.TypeId == P.TypeId))
    {
        /* Name, PriceAsc, PriceDesc */
        if (!string.IsNullOrEmpty(productSpecParams.sort))
        {
            switch (productSpecParams.sort)
            {
                case "priceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P .Name);
                    break;
            }
        }
        else if (productSpecParams.sort == "priceDesc")
        {
            AddOrderBy(P => P.Name);
        }

        ApplyIncludes();
        
        ApplyPaginations(productSpecParams.PageSize * (productSpecParams.PageIndex - 1) , productSpecParams.PageSize);
    }

    private void ApplyIncludes()
    {
        Include.Add(P => P.Brand);
        Include.Add(P => P.Type);
    }
}