using Store.S_02.Core.Entities;
using Store.S_02.Core.Specifications;

namespace Store.S_02.Core.Services.Contract;

public interface IGenericsRepository<TEntity,TKey> where TEntity : BaseEntities<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(int id);
    Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity , TKey> specification);
    Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity , TKey> specification);
    Task AddAsync(TEntity entity);
    Task<int> GetCountAsync(ISpecifications<TEntity, TKey> specification); 
    void UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    
    
}