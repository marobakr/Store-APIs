namespace Store.S_02.Core.Helper;

public class PaginationsResponse<TEntity>
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int Count { get; set; }
    public IEnumerable<TEntity> Date { get; set; }
    
    public PaginationsResponse(int pageSize, int pageIndex, int count, IEnumerable<TEntity> date)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        Count = count;
        Date = date;
    }
    
}