using Store.S_02.Core.Entities;
using Store.S_02.Core.Services.Contract;

namespace Store.S_02.Core;

public interface IUnitOfWork
{
  public Task<int> CompleteAsync();
  
  /* Create Repository <T> And Return them */
  public IGenericsRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntities<TKey>;
}