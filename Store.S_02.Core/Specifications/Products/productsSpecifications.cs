namespace Store.S_02.Core.Specifications.Products;

public class productsSpecifications:BaseSpecifications<Entities.Products, int>
{
    public productsSpecifications(int id):base(P => P.Id == id)
    {
        ApplyIncludes();
    }
    public productsSpecifications()
    {
        ApplyIncludes();    
    }

    private void ApplyIncludes()
    {
        Include.Add(P => P.Brand);
        Include.Add(P => P.Type);
    }
}