using System.Linq.Expressions;
using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Specifications;

public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntities<Tkey>
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;

    public List<Expression<Func<TEntity, object>>> Include { get; set; } = new List<Expression<Func<TEntity, object>>>();

    public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
    {
        Criteria = expression;
    }

    public BaseSpecifications()
    {
        
    }

}