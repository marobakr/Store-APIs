using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Dtos.Basket;

public class CustomerBasketDto
{
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; }
}