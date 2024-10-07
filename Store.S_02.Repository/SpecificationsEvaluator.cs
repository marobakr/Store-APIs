using Microsoft.EntityFrameworkCore;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Specifications;

namespace Store.S_02.Repository;

public class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntities<TKey>
{
    public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specification)
    {
        var query = inputQuery;
        
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy is not null)
        {
            query =  query.OrderBy(specification.OrderBy);
        }
        if (specification.OrderByDesc is not null) 
        {
            query =  query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.IsPaginationsInable)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        query = specification.Include.Aggregate(query, (currentQuery, includeQuery) => currentQuery.Include(includeQuery));

        return query;
    }
}