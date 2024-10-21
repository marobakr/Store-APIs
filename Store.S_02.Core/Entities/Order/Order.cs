namespace Store.S_02.Core.Entities.Order;

public class Order : BaseEntities<int>
{
    public string BuyuerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Address ShipToAddress { get; set; }
    public int DeliveryMethodId { get; set; }
    public DelivertyMethod DeliveryMethod { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public decimal Subtotal { get; set; }
    public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;
    public string PaymentIntentId { get; set; }
    
}