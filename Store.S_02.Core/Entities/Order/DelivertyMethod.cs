namespace Store.S_02.Core.Entities.Order;

public class DelivertyMethod:BaseEntities<int>
{
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
}