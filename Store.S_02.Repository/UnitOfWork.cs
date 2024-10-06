using System.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.S_02.Core;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Services.Contract;
using Store.S_02.Repository.Data.Contexts;
using Store.S_02.Repository.Repositories;

namespace Store.S_02.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(StoreDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IGenericsRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntities<TKey>
    {
        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repository = new GenericsRepository<TEntity, TKey>(_context);
            _repositories.Add(type, repository);
        }
        return _repositories[type] as IGenericsRepository<TEntity, TKey>;
        
    }
}