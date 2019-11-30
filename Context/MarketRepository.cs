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
        public Task<TEntity> Create(TEntity entity)
        {
            _dbSet.AddAsync(entity);
            _context.SaveChangesAsync();
            return Task.FromResult(entity);
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
            return await _dbSet.AsNoTracking().Where(predicate).AsQueryable().ToListAsync();
        }

        public Task Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        Task<TEntity> IRepository<TEntity>.Create(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
