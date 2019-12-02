using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMarket.Dto;
using TourMarket.Helpers;
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

        public IQueryable<Order> GetOrdersByManagerId(int id)
        {            
           return context.Orders.Where(x => x.ManagerId == id).Include(x => x.Manager).Include(x => x.Tourist);            
        }        
        
        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public void Update(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
        }

        public bool IfExist(Order order)
        {
            return context.Orders.Find(order.Id) != null;            
        }
    }
}
