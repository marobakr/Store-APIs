using System.Linq.Expressions;
using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Specifications;

public interface ISpecifications<TEntity,Tkey> where TEntity : BaseEntities<Tkey>

{
    public Expression<Func<TEntity, bool>> Criteria { get; set; }
    public List<Expression <Func <TEntity, object>> > Include { get; set; }
    public Expression<Func<TEntity, object>>  OrderBy { get; set; }

    public Expression<Func<TEntity, object>>  OrderByDesc { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool IsPaginationsInable { get; set; }

}