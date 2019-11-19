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

        public void AddOrder(OrderDto orderDto)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try 
                { 
                    var order = new Order { Date = orderDto.Date };
                    context.Orders.Add(order);

                    if (orderDto.Tourist.Id == 0)
                        context.Tourists.Add(orderDto.Tourist);
                    order.TouristId = orderDto.Tourist.Id;

                    if (orderDto.Tour.Id == 0)
                        context.Tours.Add(orderDto.Tour);
                    order.TourId = orderDto.Tour.Id;

                    order.ManagerId = orderDto.Manager.Id;
                                        
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
            }               
            
        }

        public void DeleteOrder(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }
    }
}
