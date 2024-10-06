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

        query = specification.Include.Aggregate(query, (currentQuery, includeQuery) => currentQuery.Include(includeQuery));

        return query;
    }
}