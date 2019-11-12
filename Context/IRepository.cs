using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMarket.Context
{
    interface IRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        TEntity Create(TEntity entity);
        TEntity FindById(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
