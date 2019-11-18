using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMarket.Models;

namespace TourMarket.Context
{
    public class OrderRepository
    {
        private readonly MarketContext context;

        public OrderRepository(MarketContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetOrderByManagerId(int id)
        {
            var result = context.Orders.Include(x => x.OrderManagers).ThenInclude(x => x.Manager);
            //another one
            var result2 = context.Orders.Select(x => new
            {
                Order = x,
                Managers = x.OrderManagers.Where(m => m.ManagerId == id).ToList()
            }).ToList();
            throw new NotImplementedException();
        }
    }
}
