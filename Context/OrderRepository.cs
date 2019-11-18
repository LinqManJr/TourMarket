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
                TourName = x.Tour.Name,
                TourPrice = x.Tour.Price,
                ManagerName = x.OrderManagers.Where(m => m.ManagerId == id).Select(y => y.Manager.Name).First(),                
                TouristName = x.OrderTourists.Where(o => o.OrderId == x.Id).Select(o => o.Tourist.Fio).First()
            }).ToList();
            return result2;
        }
    }
}
