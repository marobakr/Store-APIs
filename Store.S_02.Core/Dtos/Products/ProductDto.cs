namespace Store.S_02.Core.Dtos.Products;

public class ProductDto
{
    public int  Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public string BrandName { get; set; }
    public string TypeName { get; set; }
    public int? BrandId { get; set; } /* This is the foreign key By convention */
    public int? TypeId { get; set; } /* This is the foreign key By convention */
}