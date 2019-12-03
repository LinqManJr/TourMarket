using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Context
{
    interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        Task<IEnumerable<TEntity>> GetAsync();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate);
        TEntity Create(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity FindById(int id);
        Task<TEntity> FindByIdAsync(int id);
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
