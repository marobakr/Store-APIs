namespace Store.S_02.Core.Specifications.Products;

public class ProductSpecParams
{
    public string? sort { get; set; }
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public int PageSize { get; set; } = 5;
    public int PageIndex { get; set; } = 1;

    private string? search;

    public string? Search
    {
        get => search;
        set => search = value?.ToLower();
    }
}