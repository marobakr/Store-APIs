using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Services.Contract;

public interface IGenericsRepository<TEntity,TKey> where TEntity : BaseEntities<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(int id);
    Task AddAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    
}