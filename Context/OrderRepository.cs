using Microsoft.EntityFrameworkCore;
using System.Linq;
using TourMarket.Models;

namespace TourMarket.Context
{
    public class OrderRepository : MarketRepository<Order>
    {       
        
        public OrderRepository(DbContext context) : base(context) { }

        public IQueryable<Order> GetOrdersByManagerId(int id)
        {            
           return _dbSet.Where(x => x.ManagerId == id).Include(x => x.Manager).Include(x => x.Tourist);            
        }
        public IQueryable<Order> GetOrders()
        {
            return _dbSet.Include(x => x.Manager).Include(x => x.Tourist);
        }

        public bool IfExist(Order order)
        {
            return _dbSet.Find(order.Id) != null;            
        }
    }
}
