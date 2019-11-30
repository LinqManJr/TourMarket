using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Context
{
    interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Get(Func<TEntity, bool> predicate);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> FindById(int id);
        Task Remove(TEntity entity);
        Task Update(TEntity entity);
    }
}
