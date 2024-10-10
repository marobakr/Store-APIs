namespace Store.S_02.Core.Entities;

public class BasketItem
{
    public int Id { get; set; }
    public string nameProduct { get; set; }
    public string Picture { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}