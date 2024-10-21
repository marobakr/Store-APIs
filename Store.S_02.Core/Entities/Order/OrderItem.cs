namespace Store.S_02.Core.Entities.Order;

public class OrderItem:BaseEntities<int>
{
    public OrderItem()
    {
        
    } 
    
    public OrderItem(ProductItemOrder product, decimal price, int quantity)
    {
        Product = product;
        Price = price;
        Quantity = quantity;
    }

   
    public ProductItemOrder Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}