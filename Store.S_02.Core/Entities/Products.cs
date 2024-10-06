namespace Store.S_02.Core.Entities;

public class Products: BaseEntities<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public ProductBrand Brand { get; set; }
    public ProductType Type { get; set; }
    public int? BrandId { get; set; } /* This is the foreign key By convention */
    public int? TypeId { get; set; } /* This is the foreign key By convention */
    
    
}