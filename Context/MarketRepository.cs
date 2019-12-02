using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Context
{
    public class MarketRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public MarketRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public Task<TEntity> FindById(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Get(Func<TEntity, bool> predicate)
        {
            return await Task.FromResult(_dbSet.AsNoTracking().Where(predicate));
        }

        public async Task Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }        
    }
}
