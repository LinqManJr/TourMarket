using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMarket.Dto;
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

        public IEnumerable<OrderDto> GetOrdersByManagerId(int id)
        {   
            //var result = context.Orders.Include(x => x.OrderManagers).ThenInclude(x => x.Manager);
            
            var result2 = context.Orders.Select(x => new OrderDto
            {
                Id = x.Id,
                Date = x.Date,
                Tour = x.Tour,                
                Manager = x.OrderManagers.Where(m => m.ManagerId == id).Select(y => y.Manager).First(),                
                Tourist = x.OrderTourists.Where(o => o.OrderId == x.Id).Select(o => o.Tourist).First()
            }).ToList();
            return result2;
        }

        internal void AddOrder(OrderDto orderDto)
        {
            context.Orders.Add(new Order { });
            throw new NotImplementedException();
        }
    }
}
