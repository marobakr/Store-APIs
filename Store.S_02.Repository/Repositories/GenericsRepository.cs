using Microsoft.EntityFrameworkCore;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Services.Contract;
using Store.S_02.Core.Specifications;
using Store.S_02.Repository.Data.Contexts;

namespace Store.S_02.Repository.Repositories;

public class GenericsRepository<TEntity, TKey> : IGenericsRepository<TEntity, TKey> where TEntity : BaseEntities<TKey>
{
    private readonly StoreDbContext _context;
    //
    public GenericsRepository(StoreDbContext context)
    {
        _context = context;
    }
    
    /* === === === === === ===  GetAllAsync  === === === === === === */
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        if (typeof(TEntity) == typeof(Products))
        {
            return (IEnumerable<TEntity>)await _context.Products.OrderBy(P => P.Name).Include(P => P.Brand).Include(P => P.Type)
                .ToListAsync();
        }

        return await _context.Set<TEntity>().ToListAsync();
    }

    /* === === === === === ===  GetAsync  === === === === === === */
    public async Task<TEntity> GetAsync(int id)
    {
        if (typeof(TEntity) == typeof(Products))
        {
            return  _context.Products.Include(P => P.Brand).Include(P => P.Type)
                .FirstOrDefault(P => P.Id == id as int ?) as TEntity;
        }

        return  await _context.Set<TEntity>().FindAsync(id);
    }

    /* === === === === === ===  GetAllWithSpecificationAsync  === === === === === === */
    public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> specification)
    {
      return  await ApplySpecification(specification).ToListAsync();
    }
    /* === === === === === ===  GetWithSpecificationAsync  === === === === === === */

    public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> specification)
    {
        return  await ApplySpecification(specification).FirstOrDefaultAsync();

    }

    /* === === === === === ===  AddAsync  === === === === === === */
    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public Task<int> GetCountAsync(ISpecifications<TEntity, TKey> specification)
    {
        return ApplySpecification(specification).CountAsync();
    }

    /* === === === === === ===  UpdateAsync  === === === === === === */
    public void UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
    }
    
    /* === === === === === ===  DeleteAsync  === === === === === === */
    public void DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
    }

    /* === === === === === ===  ApplySpecification [Improve Code]  === === === === === === */

    private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> specification)
    {
        return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specification);
    }
}