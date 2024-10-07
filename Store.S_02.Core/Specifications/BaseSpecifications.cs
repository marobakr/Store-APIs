using System.Linq.Expressions;
using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Specifications;

public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntities<Tkey>
{
    public BaseSpecifications()
    {
    }

    public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;

    public List<Expression<Func<TEntity, object>>> Include { get; set; } =
        new List<Expression<Func<TEntity, object>>>();

    public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;

    public Expression<Func<TEntity, object>> OrderByDesc { get; set; } = null;

    public int Skip { get; set; }

    public int Take { get; set; }

    public bool IsPaginationsInable { get; set; }

    public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
    {
        Criteria = expression;
    }
    
    public void AddOrderBy(Expression<Func<TEntity, object>> expression)
    {
        OrderBy = expression;
    }

    public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
    {
        OrderByDesc = expression;
    }
    public void ApplyPaginations(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPaginationsInable = true;
    }
}