namespace Store.S_02.Core.Entities;

public class BaseEntities<TKey>
{
    public TKey Id { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
}