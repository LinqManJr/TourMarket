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

        public IEnumerable<OrderDto> GetOrdersByManagerId(int id)
        {
            //var result = context.Orders.Include(x => x.OrderManagers).ThenInclude(x => x.Manager);

            /*var result2 = context.Orders.Select(x => new OrderDto
            {
                Id = x.Id,
                Date = x.Date,
                Tour = x.Tour,                
                Manager = x.OrderManagers.Where(m => m.ManagerId == id).Select(y => y.Manager).First(),                
                Tourist = x.OrderTourists.Where(o => o.OrderId == x.Id).Select(o => o.Tourist).First()
            }).ToList();
            return result2;*/
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetOrders(int id)
        {        
            return context.Orders;
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
